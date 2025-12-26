#include <stdafx.h>

#include "EffectPluginLoader.h"

#include "Effects/EffectPluginAPI.h"
#include "Effects/Register/RegisteredEffects.h"
#include "Effects/Register/RegisteredEffectsMetadata.h"
#include "Util/File.h"

#include <filesystem>

static const std::vector<std::string> ms_PluginDirs { "chaosmod\\plugins" };

EffectPluginLoader::EffectPluginLoader()
{
	m_PluginDirectories = ms_PluginDirs;

	LOG("EffectPluginLoader initialized");
}

EffectPluginLoader::~EffectPluginLoader()
{
	UnloadAllPlugins();
}

void EffectPluginLoader::OnModPauseCleanup(PauseCleanupFlags cleanupFlags)
{
	// Don't unload plugins on pause - they should remain loaded
}

void EffectPluginLoader::LoadPluginsFromDirectories()
{
	LOG("Scanning for effect plugins...");

	for (const auto &dir : m_PluginDirectories)
	{
		ScanDirectory(dir);
	}

	LOG("Effect plugin loading complete. Loaded " << m_LoadedPlugins.size() << " plugin(s)");
}

void EffectPluginLoader::ScanDirectory(const std::string &directory)
{
	std::filesystem::path dirPath(directory);

	if (!std::filesystem::exists(dirPath))
	{
		DEBUG_LOG("Plugin directory does not exist: " << directory);
		return;
	}

	try
	{
		for (const auto &entry : std::filesystem::directory_iterator(dirPath))
		{
			if (!entry.is_regular_file())
				continue;

			const auto &path = entry.path();
			if (path.extension() == ".dll")
			{
				LoadPlugin(path);
			}
		}
	}
	catch (const std::exception &e)
	{
		LOG("Error scanning plugin directory " << directory << ": " << e.what());
	}
}

bool EffectPluginLoader::LoadPlugin(const std::filesystem::path &pluginPath)
{
	std::string pluginName = pluginPath.stem().string();

	if (m_LoadedPlugins.contains(pluginName))
	{
		LOG("Plugin already loaded: " << pluginName);
		return false;
	}

	LOG("Loading plugin: " << pluginPath.string());

	HMODULE hModule = LoadLibraryA(pluginPath.string().c_str());
	if (!hModule)
	{
		DWORD error = GetLastError();
		LOG("Failed to load plugin " << pluginName << " (Error code: " << error << ")");
		return false;
	}

	// Get plugin info function
	using GetPluginInfoFunc = PluginInitData *(*)();
	auto getPluginInfo      = reinterpret_cast<GetPluginInfoFunc>(GetProcAddress(hModule, "GetPluginInfo"));

	if (!getPluginInfo)
	{
		LOG("Plugin " << pluginName << " does not export GetPluginInfo - not a valid effect plugin");
		FreeLibrary(hModule);
		return false;
	}

	PluginInitData *pluginData = getPluginInfo();
	if (!pluginData)
	{
		LOG("Plugin " << pluginName << " returned null plugin info");
		FreeLibrary(hModule);
		return false;
	}

	// Check API version
	if (pluginData->ApiVersion != CHAOS_PLUGIN_API_VERSION)
	{
		LOG("Plugin " << pluginName << " has incompatible API version " << pluginData->ApiVersion << " (expected "
		             << CHAOS_PLUGIN_API_VERSION << ")");
		FreeLibrary(hModule);
		return false;
	}

	LOG("Plugin info: " << pluginData->PluginName << " v" << pluginData->PluginVersion << " with "
	                    << pluginData->EffectCount << " effect(s)");

	// Get effect functions
	using GetEffectInfoFunc      = EffectPluginInfo *(*)(uint32_t);
	using GetEffectCallbacksFunc = EffectPluginCallbacks *(*)(uint32_t);

	auto getEffectInfo      = reinterpret_cast<GetEffectInfoFunc>(GetProcAddress(hModule, "GetEffectInfo"));
	auto getEffectCallbacks = reinterpret_cast<GetEffectCallbacksFunc>(GetProcAddress(hModule, "GetEffectCallbacks"));

	if (!getEffectInfo || !getEffectCallbacks)
	{
		LOG("Plugin " << pluginName << " is missing required effect exports");
		FreeLibrary(hModule);
		return false;
	}

	// Call OnPluginLoad if it exists
	using OnPluginLoadFunc = void (*)();
	auto onPluginLoad      = reinterpret_cast<OnPluginLoadFunc>(GetProcAddress(hModule, "OnPluginLoad"));
	if (onPluginLoad)
	{
		try
		{
			onPluginLoad();
		}
		catch (...)
		{
			LOG("Exception in OnPluginLoad for plugin " << pluginName);
		}
	}

	// Register each effect from the plugin
	LoadedPlugin loadedPlugin;
	loadedPlugin.Handle   = hModule;
	loadedPlugin.FilePath = pluginPath.string();

	for (uint32_t i = 0; i < pluginData->EffectCount; i++)
	{
		EffectPluginInfo *effectInfo = getEffectInfo(i);
		if (!effectInfo)
		{
			LOG("Plugin " << pluginName << " returned null info for effect " << i);
			continue;
		}

		EffectPluginCallbacks *effectCallbacks = getEffectCallbacks(i);
		if (!effectCallbacks)
		{
			LOG("Plugin " << pluginName << " returned null callbacks for effect " << i);
			continue;
		}

		LOG("  - Registering effect: " << effectInfo->Name << " (" << effectInfo->Id << ")");

		// Create lambda wrappers for the callbacks
		std::function<void()> startFunc = nullptr;
		std::function<void()> stopFunc  = nullptr;
		std::function<void()> tickFunc  = nullptr;

		if (effectCallbacks->OnStart)
		{
			startFunc = [callback = effectCallbacks->OnStart]()
			{
				try
				{
					callback();
				}
				catch (...)
				{
					LOG("Exception in plugin effect OnStart");
				}
			};
		}

		if (effectCallbacks->OnStop)
		{
			stopFunc = [callback = effectCallbacks->OnStop]()
			{
				try
				{
					callback();
				}
				catch (...)
				{
					LOG("Exception in plugin effect OnStop");
				}
			};
		}

		if (effectCallbacks->OnTick)
		{
			tickFunc = [callback = effectCallbacks->OnTick]()
			{
				try
				{
					callback();
				}
				catch (...)
				{
					LOG("Exception in plugin effect OnTick");
				}
			};
		}

		// Create a unique identifier for the plugin effect
		std::string effectId = std::string(effectInfo->Id);

		// Register the effect
		g_RegisteredEffects.emplace_back(effectId, startFunc, stopFunc, tickFunc);

		// Register metadata
		RegisteredEffectMetadata metadata;
		metadata.Name            = effectInfo->Name;
		metadata.Id              = effectInfo->Id;
		metadata.IsTimed         = effectInfo->IsTimed;
		metadata.IsShortDuration = effectInfo->IsShortDuration;

		g_RegisteredEffectsMetadata[effectInfo->Id] = metadata;

		loadedPlugin.RegisteredEffectIds.push_back(effectId);
	}

	m_LoadedPlugins[pluginName] = loadedPlugin;

	LOG("Successfully loaded plugin: " << pluginName);
	return true;
}

void EffectPluginLoader::UnloadPlugin(const std::string &pluginName)
{
	auto it = m_LoadedPlugins.find(pluginName);
	if (it == m_LoadedPlugins.end())
		return;

	LOG("Unloading plugin: " << pluginName);

	LoadedPlugin &plugin = it->second;

	// Call OnPluginUnload if it exists
	using OnPluginUnloadFunc = void (*)();
	auto onPluginUnload      = reinterpret_cast<OnPluginUnloadFunc>(GetProcAddress(plugin.Handle, "OnPluginUnload"));
	if (onPluginUnload)
	{
		try
		{
			onPluginUnload();
		}
		catch (...)
		{
			LOG("Exception in OnPluginUnload for plugin " << pluginName);
		}
	}

	// Remove registered effects
	for (const auto &effectId : plugin.RegisteredEffectIds)
	{
		EffectIdentifier identifier(effectId);
		auto effectIt = std::find(g_RegisteredEffects.begin(), g_RegisteredEffects.end(), identifier);
		if (effectIt != g_RegisteredEffects.end())
		{
			g_RegisteredEffects.erase(effectIt);
		}

		// Remove from metadata
		auto metadataIt = g_RegisteredEffectsMetadata.find(effectId);
		if (metadataIt != g_RegisteredEffectsMetadata.end())
		{
			g_RegisteredEffectsMetadata.erase(metadataIt);
		}
	}

	// Unload the DLL
	FreeLibrary(plugin.Handle);

	m_LoadedPlugins.erase(it);
	LOG("Plugin unloaded: " << pluginName);
}

void EffectPluginLoader::UnloadAllPlugins()
{
	LOG("Unloading all plugins...");

	// Copy plugin names to avoid iterator invalidation
	std::vector<std::string> pluginNames;
	pluginNames.reserve(m_LoadedPlugins.size());
	for (const auto &[name, plugin] : m_LoadedPlugins)
	{
		pluginNames.push_back(name);
	}

	for (const auto &name : pluginNames)
	{
		UnloadPlugin(name);
	}

	LOG("All plugins unloaded");
}
