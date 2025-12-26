#pragma once

#include "Components/Component.h"

#include <filesystem>
#include <string>
#include <unordered_map>
#include <vector>
#include <windows.h>

// Forward declarations
struct EffectPluginInfo;
struct EffectPluginCallbacks;

class EffectPluginLoader : public Component
{
  public:
	struct LoadedPlugin
	{
		HMODULE Handle;
		std::string FilePath;
		std::vector<std::string> RegisteredEffectIds;
	};

  private:
	std::unordered_map<std::string, LoadedPlugin> m_LoadedPlugins;
	std::vector<std::string> m_PluginDirectories;

  public:
	EffectPluginLoader();
	virtual ~EffectPluginLoader();

	virtual void OnModPauseCleanup(PauseCleanupFlags cleanupFlags = {}) override;

	void LoadPluginsFromDirectories();
	bool LoadPlugin(const std::filesystem::path &pluginPath);
	void UnloadPlugin(const std::string &pluginName);
	void UnloadAllPlugins();

	const std::unordered_map<std::string, LoadedPlugin> &GetLoadedPlugins() const
	{
		return m_LoadedPlugins;
	}

  private:
	void ScanDirectory(const std::string &directory);
};
