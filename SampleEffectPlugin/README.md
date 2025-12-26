# Sample Effect Plugin

This is a sample effect plugin for ChaosModV that demonstrates how to create custom effects as DLLs.

## Building

### Using CMake

```bash
mkdir build
cd build
cmake ..
cmake --build . --config Release
```

The output DLL should be placed in `chaosmod/plugins/` directory.

## Creating Your Own Plugin

1. Copy this directory as a template
2. Implement your effects in `SamplePlugin.cpp`
3. Update the plugin metadata (name, version, effects)
4. Build and place the DLL in `chaosmod/plugins/`
5. Add your effect to the effects configuration file

## Plugin API

The plugin exports the following functions:
- `GetPluginInfo()` - Returns plugin metadata
- `GetEffectInfo(index)` - Returns effect metadata for each effect
- `GetEffectCallbacks(index)` - Returns callbacks (OnStart, OnStop, OnTick) for each effect
- `OnPluginLoad()` - Called when plugin is loaded (optional)
- `OnPluginUnload()` - Called when plugin is unloaded (optional)

## Effect Callbacks

Each effect can implement:
- `OnStart` - Called when the effect starts
- `OnStop` - Called when the effect ends
- `OnTick` - Called every frame while the effect is active

## Important Notes

- Plugins are loaded at mod initialization
- Effects must be added to the effects configuration file to be enabled
- Use the same API version as the main mod
- Handle exceptions in your code - crashes will affect the entire mod
