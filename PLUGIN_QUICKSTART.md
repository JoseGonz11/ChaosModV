# Plugin Quick Start

A minimal guide to creating your first ChaosModV effect plugin.

## 5-Minute Plugin

### Step 1: Create Plugin File

Create `MyPlugin.cpp`:

```cpp
#define CHAOS_PLUGIN_EXPORTS
#include "../ChaosMod/Effects/EffectPluginAPI.h"

// Your effect logic
static void MyEffect_OnStart() {
    // Called when effect starts
}

static void MyEffect_OnStop() {
    // Called when effect ends
}

static void MyEffect_OnTick() {
    // Called every frame
}

// Plugin metadata
static PluginInitData g_PluginData = {
    CHAOS_PLUGIN_API_VERSION,
    "My First Plugin",
    "1.0.0",
    1  // Number of effects
};

// Effect metadata
static EffectPluginInfo g_EffectInfo = {
    "My Cool Effect",          // Display name
    "my_cool_effect",          // Unique ID
    "Does something cool",     // Description
    "Your Name",              // Author
    true,                     // Is timed?
    false,                    // Is short duration?
    1                         // Version
};

// Effect callbacks
static EffectPluginCallbacks g_Callbacks = {
    MyEffect_OnStart,
    MyEffect_OnStop,
    MyEffect_OnTick
};

// Required exports
extern "C" {
    CHAOS_PLUGIN_API PluginInitData* GetPluginInfo() {
        return &g_PluginData;
    }

    CHAOS_PLUGIN_API EffectPluginInfo* GetEffectInfo(uint32_t index) {
        return (index == 0) ? &g_EffectInfo : nullptr;
    }

    CHAOS_PLUGIN_API EffectPluginCallbacks* GetEffectCallbacks(uint32_t index) {
        return (index == 0) ? &g_Callbacks : nullptr;
    }

    CHAOS_PLUGIN_API void OnPluginLoad() {}
    CHAOS_PLUGIN_API void OnPluginUnload() {}
}
```

### Step 2: Create Build File

Create `CMakeLists.txt`:

```cmake
cmake_minimum_required(VERSION 3.15)
project(MyPlugin)

set(CMAKE_CXX_STANDARD 20)
add_library(MyPlugin MODULE MyPlugin.cpp)
set_target_properties(MyPlugin PROPERTIES OUTPUT_NAME "MyPlugin" SUFFIX ".dll")
```

### Step 3: Build

```bash
mkdir build
cd build
cmake -GNinja -DCMAKE_BUILD_TYPE=Release ..
ninja
```

### Step 4: Install

Copy `MyPlugin.dll` to `GTA V/chaosmod/plugins/`

### Step 5: Enable

Add to `chaosmod/configs/effects.json`:

```json
{
  "my_cool_effect": {
    "enabled": true,
    "weight": 5
  }
}
```

### Step 6: Play!

Start GTA V with ChaosModV and your effect will be available!

## Common Patterns

### Timed Effect

```cpp
static void OnStart() {
    // Initialize effect
}

static void OnTick() {
    // Run every frame until timer expires
}

static void OnStop() {
    // Cleanup
}

// In metadata:
.IsTimed = true,
.IsShortDuration = false  // true for effects < 30s
```

### One-Time Effect

```cpp
static void OnStart() {
    // Do something once
}

static void OnStop() {}
static void OnTick() {}

// In metadata:
.IsTimed = false
```

### Multiple Effects in One Plugin

```cpp
static PluginInitData g_PluginData = {
    CHAOS_PLUGIN_API_VERSION,
    "My Plugin Pack",
    "1.0.0",
    3  // Three effects
};

static EffectPluginInfo g_Effects[] = {
    { "Effect 1", "effect_1", "", "", true, false, 1 },
    { "Effect 2", "effect_2", "", "", true, false, 1 },
    { "Effect 3", "effect_3", "", "", false, false, 1 }
};

static EffectPluginCallbacks g_Callbacks[] = {
    { Effect1_Start, Effect1_Stop, Effect1_Tick },
    { Effect2_Start, Effect2_Stop, Effect2_Tick },
    { Effect3_Start, Effect3_Stop, Effect3_Tick }
};

extern "C" {
    CHAOS_PLUGIN_API EffectPluginInfo* GetEffectInfo(uint32_t index) {
        return (index < 3) ? &g_Effects[index] : nullptr;
    }

    CHAOS_PLUGIN_API EffectPluginCallbacks* GetEffectCallbacks(uint32_t index) {
        return (index < 3) ? &g_Callbacks[index] : nullptr;
    }
}
```

### Resource Management

```cpp
// Global resources
static HMODULE g_MyLibrary = nullptr;

extern "C" {
    CHAOS_PLUGIN_API void OnPluginLoad() {
        // Load resources
        g_MyLibrary = LoadLibrary("mylib.dll");
    }

    CHAOS_PLUGIN_API void OnPluginUnload() {
        // Clean up
        if (g_MyLibrary) {
            FreeLibrary(g_MyLibrary);
            g_MyLibrary = nullptr;
        }
    }
}
```

## Troubleshooting

### Plugin Not Loading

**Check**: File is in `chaosmod/plugins/`
**Check**: File has `.dll` extension
**Check**: API version matches
**Check**: All required exports present

### Effect Not Running

**Check**: Effect enabled in config
**Check**: Effect ID matches exactly
**Check**: Plugin loaded successfully (check logs)

### Crashes

**Add**: Exception handling in callbacks
**Check**: Null pointers before use
**Test**: With simple effect first

## Next Steps

- See **PLUGIN_DEVELOPMENT.md** for detailed guide
- See **SampleEffectPlugin/** for complete example
- See **ARCHITECTURE.md** for system internals

## Tips

1. Start simple - get a basic effect working first
2. Use logging to debug (will appear in mod logs)
3. Test each effect independently
4. Handle errors gracefully
5. Check examples in SampleEffectPlugin
6. Read full documentation for advanced features

## Getting Help

- Check documentation in this repository
- Join the Discord server (link in README)
- Check the Wiki
- Report issues on GitHub

Happy modding! 🎮
