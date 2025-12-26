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

// Helper macro for defining plugin effects (for C++ plugins)
#ifdef __cplusplus

#define DEFINE_CHAOS_PLUGIN(name, version)                   \
	extern "C"                                               \
	{                                                        \
		CHAOS_PLUGIN_API PluginInitData *GetPluginInfo()    \
		{                                                    \
			static PluginInitData data = {                   \
				CHAOS_PLUGIN_API_VERSION,                    \
				name,                                        \
				version,                                     \
				GetPluginEffectCount()                       \
			};                                               \
			return &data;                                    \
		}                                                    \
		CHAOS_PLUGIN_API void OnPluginLoad() {}              \
		CHAOS_PLUGIN_API void OnPluginUnload() {}            \
	}                                                        \
	static uint32_t GetPluginEffectCount();

#define BEGIN_EFFECT_LIST() \
	static uint32_t GetPluginEffectCount() { return g_PluginEffectCount; } \
	static constexpr uint32_t g_PluginEffectCount =

#define REGISTER_PLUGIN_EFFECT(index, name, id, isTimed, startFunc, stopFunc, tickFunc) \
	namespace { \
		EffectPluginInfo g_EffectInfo_##index = { name, id, "", "", isTimed, false, 1 }; \
		EffectPluginCallbacks g_EffectCallbacks_##index = { startFunc, stopFunc, tickFunc }; \
	} \
	extern "C" { \
		CHAOS_PLUGIN_API EffectPluginInfo *GetEffectInfo(uint32_t effectIndex) { \
			if (effectIndex == index) return &g_EffectInfo_##index; \
			return nullptr; \
		} \
		CHAOS_PLUGIN_API EffectPluginCallbacks *GetEffectCallbacks(uint32_t effectIndex) { \
			if (effectIndex == index) return &g_EffectCallbacks_##index; \
			return nullptr; \
		} \
	}

#endif
