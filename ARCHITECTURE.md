# Effect Loading Architecture

This document describes how effects are loaded and managed in ChaosModV after the plugin refactoring.

## Overview

ChaosModV now supports three methods for loading effects:

1. **Built-in Effects** - Compiled directly into ChaosMod.asi (existing)
2. **Lua Scripts** - Dynamically loaded at runtime from script files (existing)
3. **C++ Plugin DLLs** - Dynamically loaded at runtime from compiled DLLs (new)

## Architecture Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                        ChaosMod.asi                          │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │              Effect Registration System                 │ │
│  │                                                          │ │
│  │   g_RegisteredEffects: vector<RegisteredEffect>         │ │
│  │   g_RegisteredEffectsMetadata: map<Id, Metadata>        │ │
│  └────────────────────────────────────────────────────────┘ │
│                           ▲                                  │
│                           │                                  │
│         ┌─────────────────┼─────────────────┐              │
│         │                 │                 │              │
│         ▼                 ▼                 ▼              │
│  ┌──────────┐   ┌─────────────┐   ┌────────────────┐     │
│  │ Built-in │   │ LuaScripts  │   │ PluginLoader   │     │
│  │ Effects  │   │ Component   │   │ Component      │     │
│  │          │   │             │   │                │     │
│  │ REGISTER │   │ Loads .lua  │   │ Loads .dll     │     │
│  │ _EFFECT  │   │ files       │   │ files          │     │
│  └──────────┘   └─────────────┘   └────────────────┘     │
│         │                 │                 │              │
│         │                 │                 │              │
└─────────┼─────────────────┼─────────────────┼──────────────┘
          │                 │                 │
          │                 │                 │
    Compiled in      chaosmod/scripts/  chaosmod/plugins/
                     chaosmod/workshop/
                     chaosmod/custom_scripts/
```

## Component Interaction

### 1. Built-in Effects

**Location**: `ChaosMod/Effects/db/`

**Registration**: Compile-time using `REGISTER_EFFECT` macro

**Lifecycle**:
```cpp
// At compile time:
REGISTER_EFFECT(OnStart, OnStop, OnTick, { .Name = "...", .Id = "..." })
  ↓
RegisterEffect constructor called
  ↓
Effect added to g_RegisteredEffects
  ↓
Metadata added to g_RegisteredEffectsMetadata
```

**Advantages**:
- Maximum performance (inlined, optimized)
- Direct access to all internal APIs
- No loading overhead

**Disadvantages**:
- Requires recompilation to add/modify
- Cannot be distributed independently

### 2. Lua Scripts

**Location**: `chaosmod/scripts/`, `chaosmod/workshop/`, `chaosmod/custom_scripts/`

**Registration**: Runtime by LuaScripts component

**Lifecycle**:
```
Mod initialization
  ↓
LuaScripts::Init()
  ↓
Scan script directories
  ↓
For each .lua file:
  ↓
  Parse Lua script
  ↓
  Create RegisteredEffect with EffectIdentifier(id, isScript=true)
  ↓
  Add to g_RegisteredEffects
  ↓
  Store Lua state in m_RegisteredEffects map
  ↓
When effect executes:
  ↓
  LuaScripts::Execute(effectId, funcType)
```

**Advantages**:
- Easy to create and modify
- No compilation needed
- Hot-reloadable
- Cross-platform development

**Disadvantages**:
- Slower execution (interpreted)
- Limited API access
- Requires learning Lua

### 3. C++ Plugin DLLs

**Location**: `chaosmod/plugins/`

**Registration**: Runtime by EffectPluginLoader component

**Lifecycle**:
```
Mod initialization
  ↓
EffectPluginLoader::Init()
  ↓
EffectPluginLoader::LoadPluginsFromDirectories()
  ↓
Scan chaosmod/plugins/ directory
  ↓
For each .dll file:
  ↓
  LoadLibrary(dllPath)
  ↓
  GetProcAddress("GetPluginInfo")
  ↓
  Validate API version
  ↓
  For each effect in plugin:
    ↓
    GetEffectInfo(index)
    ↓
    GetEffectCallbacks(index)
    ↓
    Create lambda wrappers with exception handling
    ↓
    Create RegisteredEffect(id, startFunc, stopFunc, tickFunc)
    ↓
    Add to g_RegisteredEffects
    ↓
    Add metadata to g_RegisteredEffectsMetadata
  ↓
  Call OnPluginLoad()
```

**Advantages**:
- Native performance
- Can be distributed independently
- No recompilation of main mod
- Full C++ language features
- Can link to external libraries

**Disadvantages**:
- Requires C++ knowledge
- Requires compilation
- No hot-reload (needs restart)
- Must match API version

## Effect Execution Flow

Regardless of how an effect is loaded, execution follows the same path:

```
EffectDispatcher::DispatchEffect(effectId)
  ↓
Look up effect in g_EnabledEffects
  ↓
Find RegisteredEffect in g_RegisteredEffects
  ↓
RegisteredEffect::Start()
  ↓
  If IsScript() → LuaScripts::Execute(effectId, Start)
  Else → Call m_Start function pointer
  ↓
Each frame while active:
  ↓
  RegisteredEffect::Tick()
    ↓
    If IsScript() → LuaScripts::Execute(effectId, Tick)
    Else → Call m_Tick function pointer
  ↓
When effect ends:
  ↓
  RegisteredEffect::Stop()
    ↓
    If IsScript() → LuaScripts::Execute(effectId, Stop)
    Else → Call m_Stop function pointer
```

## Plugin API Design

The plugin API is designed to be:

1. **C-compatible**: Uses extern "C" for maximum compatibility
2. **Version-safe**: API version check prevents incompatible plugins
3. **Safe**: Exception handling wrappers prevent crashes
4. **Simple**: Minimal required exports
5. **Extensible**: Easy to add new features in future versions

### Required Exports

```cpp
extern "C" {
    PluginInitData* GetPluginInfo();
    EffectPluginInfo* GetEffectInfo(uint32_t effectIndex);
    EffectPluginCallbacks* GetEffectCallbacks(uint32_t effectIndex);
}
```

### Optional Exports

```cpp
extern "C" {
    void OnPluginLoad();    // Initialize resources
    void OnPluginUnload();  // Clean up resources
}
```

## Configuration Integration

All three effect types use the same configuration system:

**effects.json / effects.ini**:
```json
{
  "builtin_effect_id": {
    "enabled": true,
    "weight": 5
  },
  "lua_script_effect_id": {
    "enabled": true,
    "weight": 5
  },
  "plugin_effect_id": {
    "enabled": true,
    "weight": 5
  }
}
```

Effects are enabled/disabled and weighted identically regardless of source.

## Backwards Compatibility

The refactoring maintains full backwards compatibility:

1. **Built-in effects**: Continue to work exactly as before
2. **Lua scripts**: Continue to work exactly as before
3. **Effect configuration**: Same format and behavior
4. **Effect dispatcher**: No changes to execution logic
5. **Feature flag**: `disablebuiltineffects` flag still works

## Future Extensions

The plugin architecture could be extended to support:

1. **Plugin dependencies**: Plugins that depend on other plugins
2. **Plugin configuration**: Per-plugin config files
3. **Hot reload**: Reload plugins without restarting
4. **Plugin UI**: Custom UI elements from plugins
5. **Event system**: Plugins can subscribe to game events
6. **Shared utilities**: Common helper library for plugins
7. **Plugin marketplace**: Centralized plugin distribution

## Security Considerations

### Trust Model

- **Built-in effects**: Fully trusted (part of main mod)
- **Lua scripts**: Sandboxed (limited API access)
- **C++ plugins**: Fully trusted (native code execution)

### Plugin Safety

C++ plugins have full access to the game process, so:

1. Only load plugins from trusted sources
2. Plugins can crash the game if poorly written
3. Exception handling wrappers provide some protection
4. API version check prevents incompatible plugins

### Recommendations

1. Review plugin source code before using
2. Download plugins from trusted developers
3. Keep plugins updated
4. Report malicious plugins to mod authors
5. Test plugins in a safe environment first

## Performance Comparison

| Metric | Built-in | Lua | C++ Plugin |
|--------|----------|-----|------------|
| Load Time | 0ms (compiled in) | ~10-50ms per script | ~50-100ms per DLL |
| Execution Speed | Fastest (native) | Slower (interpreted) | Fast (native) |
| Memory Overhead | None | Lua VM per script | DLL in memory |
| Distribution Size | N/A (part of mod) | Small (.lua files) | Medium (.dll files) |

## Development Workflow

### Adding Built-in Effect

1. Create .cpp file in `ChaosMod/Effects/db/`
2. Use `REGISTER_EFFECT` macro
3. Rebuild entire mod
4. Test
5. Distribute entire mod update

### Adding Lua Effect

1. Create .lua file in `chaosmod/scripts/`
2. Define effect metadata and functions
3. Copy file to game directory
4. Restart mod (or use hot reload)
5. Test
6. Distribute .lua file

### Adding Plugin Effect

1. Create new plugin project
2. Include `EffectPluginAPI.h`
3. Implement effect logic
4. Build plugin DLL
5. Copy to `chaosmod/plugins/`
6. Add to effect config
7. Restart mod
8. Test
9. Distribute .dll file

## Troubleshooting

### Plugin Not Loading

Check logs for:
- File not found: Check path
- API version mismatch: Update plugin or mod
- Missing exports: Implement required functions
- Exception in OnPluginLoad: Fix plugin initialization

### Effect Not Appearing

1. Check effect is in configuration file
2. Verify effect ID matches exactly
3. Check plugin loaded successfully
4. Ensure effect is enabled in config
5. Look for errors in logs

### Crashes

1. Check plugin exception handling
2. Verify API usage is correct
3. Test with simple effect first
4. Use debugger to identify crash location
5. Report to plugin developer
