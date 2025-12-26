# Effect Plugin System

ChaosModV now supports dynamic loading of effects through a plugin system. This allows you to create custom effects without modifying or recompiling the main mod.

## Overview

The plugin system supports two types of dynamic effects:

1. **Lua Scripts** - Simple, interpreted scripts (already existed)
2. **C++ DLL Plugins** - Compiled native code plugins (new)

## Plugin Types

### Lua Scripts

Place `.lua` files in:
- `chaosmod/scripts/`
- `chaosmod/workshop/`
- `chaosmod/custom_scripts/`

See the [Lua Scripting Wiki](https://github.com/gta-chaos-mod/ChaosModV/wiki/Lua-Scripting) for more information.

### C++ DLL Plugins

Place `.dll` files in:
- `chaosmod/plugins/`

## Creating a C++ Plugin

### 1. Setup Your Project

Copy the `SampleEffectPlugin` directory as a starting point:

```bash
cp -r SampleEffectPlugin MyCustomPlugin
cd MyCustomPlugin
```

### 2. Include the Plugin API

Include the plugin API header in your source file:

```cpp
#define CHAOS_PLUGIN_EXPORTS
#include "../ChaosMod/Effects/EffectPluginAPI.h"
```

### 3. Implement Plugin Metadata

Define your plugin information:

```cpp
static PluginInitData g_PluginData = {
    CHAOS_PLUGIN_API_VERSION,  // API version (must match)
    "My Custom Plugin",         // Plugin name
    "1.0.0",                   // Plugin version
    1                          // Number of effects
};
```

### 4. Define Your Effects

For each effect, provide metadata and callbacks:

```cpp
// Effect metadata
static EffectPluginInfo g_EffectInfo = {
    "My Effect Name",           // Display name
    "my_custom_effect_id",      // Unique ID
    "Effect description",       // Description
    "Your Name",               // Author
    true,                      // Is timed effect?
    false,                     // Is short duration?
    1                          // Effect version
};

// Effect callbacks
static void MyEffect_OnStart() {
    // Called when effect starts
}

static void MyEffect_OnStop() {
    // Called when effect ends
}

static void MyEffect_OnTick() {
    // Called every frame while active
}

static EffectPluginCallbacks g_EffectCallbacks = {
    MyEffect_OnStart,
    MyEffect_OnStop,
    MyEffect_OnTick
};
```

### 5. Export Required Functions

Implement the required plugin exports:

```cpp
extern "C" {
    CHAOS_PLUGIN_API PluginInitData* GetPluginInfo() {
        return &g_PluginData;
    }

    CHAOS_PLUGIN_API EffectPluginInfo* GetEffectInfo(uint32_t effectIndex) {
        if (effectIndex == 0)
            return &g_EffectInfo;
        return nullptr;
    }

    CHAOS_PLUGIN_API EffectPluginCallbacks* GetEffectCallbacks(uint32_t effectIndex) {
        if (effectIndex == 0)
            return &g_EffectCallbacks;
        return nullptr;
    }

    CHAOS_PLUGIN_API void OnPluginLoad() {
        // Optional: Initialize plugin resources
    }

    CHAOS_PLUGIN_API void OnPluginUnload() {
        // Optional: Clean up plugin resources
    }
}
```

### 6. Build Your Plugin

Using CMake:

```bash
mkdir build
cd build
cmake -GNinja -DCMAKE_BUILD_TYPE=Release ..
ninja
```

Or use Visual Studio to open the CMakeLists.txt file.

### 7. Install Your Plugin

Copy the compiled DLL to the plugins directory:

```
GTA V Directory/
  └── chaosmod/
      └── plugins/
          └── MyCustomPlugin.dll
```

### 8. Enable Your Effect

Add your effect to the effects configuration file (`chaosmod/configs/effects.json` or `effects.ini`):

```json
{
  "my_custom_effect_id": {
    "enabled": true,
    "weight": 5
  }
}
```

## Plugin API Reference

### Data Structures

#### PluginInitData
```cpp
struct PluginInitData {
    uint32_t ApiVersion;      // Must be CHAOS_PLUGIN_API_VERSION
    const char* PluginName;   // Plugin display name
    const char* PluginVersion;// Plugin version string
    uint32_t EffectCount;     // Number of effects in plugin
};
```

#### EffectPluginInfo
```cpp
struct EffectPluginInfo {
    const char* Name;         // Effect display name
    const char* Id;           // Unique effect identifier
    const char* Description;  // Effect description
    const char* Author;       // Effect author
    bool IsTimed;            // Is this a timed effect?
    bool IsShortDuration;    // Is this a short duration effect?
    uint32_t Version;        // Effect version
};
```

#### EffectPluginCallbacks
```cpp
struct EffectPluginCallbacks {
    EffectStartFunc OnStart; // Called when effect starts
    EffectStopFunc OnStop;   // Called when effect stops
    EffectTickFunc OnTick;   // Called every frame
};
```

### Required Exports

- `PluginInitData* GetPluginInfo()` - Returns plugin metadata
- `EffectPluginInfo* GetEffectInfo(uint32_t index)` - Returns effect metadata
- `EffectPluginCallbacks* GetEffectCallbacks(uint32_t index)` - Returns effect callbacks

### Optional Exports

- `void OnPluginLoad()` - Called when plugin is loaded
- `void OnPluginUnload()` - Called before plugin is unloaded

## Best Practices

1. **Error Handling**: Always handle exceptions in your callbacks - crashes will affect the entire mod
2. **Thread Safety**: Effects may be called from different threads - use appropriate synchronization
3. **Unique IDs**: Use descriptive, unique effect IDs to avoid conflicts with other plugins
4. **Cleanup**: Always clean up resources in OnStop and OnPluginUnload
5. **Testing**: Test your plugin thoroughly before distribution
6. **Version**: Keep your plugin version updated when making changes
7. **Documentation**: Document your effects and any special requirements

## Troubleshooting

### Plugin Not Loading

1. Check that the DLL is in `chaosmod/plugins/`
2. Verify API version matches (check logs)
3. Ensure all required exports are present
4. Check mod logs for error messages

### Effect Not Running

1. Verify effect is enabled in configuration
2. Check effect ID matches exactly
3. Look for errors in mod logs
4. Ensure callbacks are not null

### Crashes

1. Add exception handling in all callbacks
2. Validate pointers before use
3. Don't call game functions outside the main thread
4. Check for memory leaks

## Examples

See the `SampleEffectPlugin` directory for a complete working example.

## Advantages Over Built-in Effects

- **No Recompilation**: Add/update effects without rebuilding the mod
- **Easy Distribution**: Share plugins as simple DLL files
- **Modularity**: Enable/disable specific plugin collections
- **Rapid Development**: Faster iteration without full mod builds
- **Independence**: Plugins can be updated separately from the main mod

## Comparison: Lua vs C++ Plugins

| Feature | Lua Scripts | C++ Plugins |
|---------|------------|-------------|
| Performance | Slower (interpreted) | Fast (native code) |
| Development Speed | Fast | Slower |
| Complexity | Simple syntax | Full C++ features |
| Hot Reload | Yes | No (requires restart) |
| Memory Access | Limited | Full access |
| Best For | Simple effects | Complex/performance-critical effects |

## Future Enhancements

Potential future improvements:
- Hot reloading for C++ plugins
- Plugin dependency system
- Extended API for more game features
- Plugin configuration UI
- Auto-update mechanism
