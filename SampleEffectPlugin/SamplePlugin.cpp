#define CHAOS_PLUGIN_EXPORTS
#include "../ChaosMod/Effects/EffectPluginAPI.h"

#include <windows.h>

// Sample effect 1: Simple message effect
static void SampleEffect1_OnStart()
{
	// This would show a message to the player
	// In a real plugin, you'd use the game's native functions
}

static void SampleEffect1_OnStop()
{
	// Cleanup for effect 1
}

static void SampleEffect1_OnTick()
{
	// Called every frame while effect 1 is active
}

// Sample effect 2: Another example effect
static void SampleEffect2_OnStart()
{
	// Effect 2 initialization
}

static void SampleEffect2_OnStop()
{
	// Effect 2 cleanup
}

static void SampleEffect2_OnTick()
{
	// Effect 2 per-frame logic
}

// Define the plugin metadata
static PluginInitData g_PluginData = {
	CHAOS_PLUGIN_API_VERSION,
	"Sample Plugin",
	"1.0.0",
	2 // Number of effects
};

// Define effect metadata
static EffectPluginInfo g_EffectInfos[] = {
	{
		"Sample Effect 1",           // Name
		"plugin_sample_effect_1",    // Id
		"A sample timed effect",     // Description
		"Sample Author",             // Author
		true,                        // IsTimed
		false,                       // IsShortDuration
		1                            // Version
	},
	{
		"Sample Effect 2",
		"plugin_sample_effect_2",
		"Another sample effect",
		"Sample Author",
		false,
		false,
		1
	}
};

// Define effect callbacks
static EffectPluginCallbacks g_EffectCallbacks[] = {
	{
		SampleEffect1_OnStart,
		SampleEffect1_OnStop,
		SampleEffect1_OnTick
	},
	{
		SampleEffect2_OnStart,
		SampleEffect2_OnStop,
		SampleEffect2_OnTick
	}
};

// Exported functions required by the plugin API
extern "C"
{
	CHAOS_PLUGIN_API PluginInitData *GetPluginInfo()
	{
		return &g_PluginData;
	}

	CHAOS_PLUGIN_API EffectPluginInfo *GetEffectInfo(uint32_t effectIndex)
	{
		if (effectIndex < 2)
			return &g_EffectInfos[effectIndex];
		return nullptr;
	}

	CHAOS_PLUGIN_API EffectPluginCallbacks *GetEffectCallbacks(uint32_t effectIndex)
	{
		if (effectIndex < 2)
			return &g_EffectCallbacks[effectIndex];
		return nullptr;
	}

	CHAOS_PLUGIN_API void OnPluginLoad()
	{
		// Called when the plugin is loaded
		// Initialize any resources here
	}

	CHAOS_PLUGIN_API void OnPluginUnload()
	{
		// Called when the plugin is unloaded
		// Clean up any resources here
	}
}

// DLL entry point
BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	switch (fdwReason)
	{
		case DLL_PROCESS_ATTACH:
			// DLL is being loaded
			break;
		case DLL_PROCESS_DETACH:
			// DLL is being unloaded
			break;
	}
	return TRUE;
}
