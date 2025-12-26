#pragma once

#include <cstdint>

// Plugin API version - increment when making breaking changes
#define CHAOS_PLUGIN_API_VERSION 1

// Export macros for plugin DLLs
#ifdef CHAOS_PLUGIN_EXPORTS
#define CHAOS_PLUGIN_API __declspec(dllexport)
#else
#define CHAOS_PLUGIN_API __declspec(dllimport)
#endif

// Plugin API is C-compatible for maximum compatibility
#ifdef __cplusplus
extern "C"
{
#endif

	// Effect metadata structure
	struct EffectPluginInfo
	{
		const char *Name;
		const char *Id;
		const char *Description;
		const char *Author;
		bool IsTimed;
		bool IsShortDuration;
		uint32_t Version;
	};

	// Effect callback function types
	typedef void (*EffectStartFunc)();
	typedef void (*EffectStopFunc)();
	typedef void (*EffectTickFunc)();

	// Effect callbacks structure
	struct EffectPluginCallbacks
	{
		EffectStartFunc OnStart;
		EffectStopFunc OnStop;
		EffectTickFunc OnTick;
	};

	// Plugin initialization structure
	struct PluginInitData
	{
		uint32_t ApiVersion;
		const char *PluginName;
		const char *PluginVersion;
		uint32_t EffectCount;
	};

	// Required plugin exports
	// Returns plugin initialization data
	CHAOS_PLUGIN_API PluginInitData *GetPluginInfo();

	// Returns effect metadata for a specific effect index
	CHAOS_PLUGIN_API EffectPluginInfo *GetEffectInfo(uint32_t effectIndex);

	// Returns effect callbacks for a specific effect index
	CHAOS_PLUGIN_API EffectPluginCallbacks *GetEffectCallbacks(uint32_t effectIndex);

	// Called when plugin is loaded
	CHAOS_PLUGIN_API void OnPluginLoad();

	// Called when plugin is unloaded
	CHAOS_PLUGIN_API void OnPluginUnload();

#ifdef __cplusplus
}
#endif

