# Disabling Built-in Effects

If you want to use only dynamic effects (Lua scripts and/or C++ plugins), you can disable all built-in effects.

## How to Disable Built-in Effects

Create a feature flag file in the game directory:

**File**: `chaosmod/.disablebuiltineffects`

**Content**: Empty file (just create the file, no content needed)

**Location**:
```
GTA V Directory/
  └── chaosmod/
      └── .disablebuiltineffects
```

## What This Does

When this feature flag is present:
- All built-in effects (from `ChaosMod/Effects/db/`) are NOT registered
- Only Lua scripts and C++ plugins are loaded
- Mod size and memory usage are reduced
- You have complete control over which effects are available

## Use Cases

1. **Custom Mod Packs**: Create a completely custom experience with only your effects
2. **Performance**: Reduce mod footprint by using only selected effects
3. **Development**: Test plugins without built-in effects interfering
4. **Specialized Mods**: Create themed mod packs (e.g., only vehicle effects)

## Re-enabling Built-in Effects

Simply delete the `.disablebuiltineffects` file from the `chaosmod` directory.

## Important Notes

- The mod will still load Lua scripts and plugins even when built-in effects are disabled
- If you disable built-in effects and don't have any plugins or scripts, the mod will have no effects to dispatch
- This feature has existed since before the plugin system was added
- Configuration files still work the same way

## Example Workflow

### Creating a Pure Plugin Mod

1. Create `.disablebuiltineffects` file
2. Place your custom plugins in `chaosmod/plugins/`
3. Configure your effects in `chaosmod/configs/effects.json`
4. Start the game

Now only your custom plugin effects will be available!

### Creating a Lua-only Mod

1. Create `.disablebuiltineffects` file
2. Place your Lua scripts in `chaosmod/scripts/`
3. Configure your effects in `chaosmod/configs/effects.json`
4. Start the game

Now only your Lua script effects will be available!

### Mixed Approach

You can also use a combination:
1. Enable built-in effects (no feature flag)
2. Add Lua scripts for simple custom effects
3. Add C++ plugins for complex custom effects
4. Use effect configuration to enable/disable specific effects

This gives you maximum flexibility!
