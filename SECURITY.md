# Security Considerations for Plugin System

## Overview

The ChaosModV plugin system allows loading and executing native code from DLL files. This document outlines the security considerations, protections, and best practices.

## Security Model

### Trust Levels

1. **Built-in Effects**: Fully trusted, part of signed mod
2. **Lua Scripts**: Sandboxed, limited API access
3. **C++ Plugins**: Fully trusted, native code execution

### Plugin Trust

C++ plugins have the same privileges as the main mod:
- Full memory access
- Full API access
- Can interact with game process
- Can load other libraries
- Can execute arbitrary code

**Conclusion**: Only load plugins from trusted sources.

## Security Protections

### 1. API Version Validation

```cpp
if (pluginData->ApiVersion != CHAOS_PLUGIN_API_VERSION)
{
    LOG("Plugin has incompatible API version");
    FreeLibrary(hModule);
    return false;
}
```

**Protects Against**:
- Loading incompatible plugins
- ABI mismatches
- Memory corruption from version mismatches

### 2. Null Pointer Checks

```cpp
if (!getPluginInfo) {
    FreeLibrary(hModule);
    return false;
}

PluginInitData *pluginData = getPluginInfo();
if (!pluginData) {
    FreeLibrary(hModule);
    return false;
}
```

**Protects Against**:
- Crashes from null function pointers
- Invalid plugin exports
- Memory corruption

### 3. Exception Handling

```cpp
startFunc = [callback = effectCallbacks->OnStart]()
{
    try {
        callback();
    }
    catch (...) {
        LOG("Exception in plugin effect OnStart");
    }
};
```

**Protects Against**:
- Plugin crashes affecting mod
- Unhandled exceptions
- Cascade failures

### 4. Resource Cleanup

```cpp
// On error:
FreeLibrary(hModule);
return false;

// On unload:
FreeLibrary(plugin.Handle);
```

**Protects Against**:
- Memory leaks
- Resource exhaustion
- DLL handle leaks

### 5. Bounds Checking

```cpp
if (effectIndex < pluginData->EffectCount)
    return &g_EffectInfos[effectIndex];
return nullptr;
```

**Protects Against**:
- Buffer overruns
- Out-of-bounds access
- Memory corruption

### 6. Directory Restriction

```cpp
static const std::vector<std::string> ms_PluginDirs { "chaosmod\\plugins" };
```

**Protects Against**:
- Loading DLLs from arbitrary locations
- DLL hijacking attacks
- Path traversal attacks

## Known Limitations

### What This CANNOT Protect Against

1. **Malicious Plugins**: A malicious DLL can do anything
2. **Memory Corruption**: Plugins can corrupt mod memory
3. **Game Crashes**: Plugins can crash the game
4. **Data Exfiltration**: Plugins can read game memory
5. **Code Injection**: Plugins can inject code into game
6. **Persistence**: Plugins can install hooks/modifications

## Best Practices for Users

### 1. Source Verification

✅ **DO**:
- Download from official sources
- Verify plugin author
- Check for source code
- Read user reviews
- Scan for malware

❌ **DON'T**:
- Download from unknown sites
- Use pirated plugins
- Trust unsigned plugins
- Skip malware scans

### 2. Testing

✅ **DO**:
- Test in isolated environment
- Back up game saves
- Monitor for unusual behavior
- Start with one plugin
- Read plugin documentation

❌ **DON'T**:
- Load many new plugins at once
- Skip testing phase
- Use on main save file
- Ignore crash reports

### 3. Updates

✅ **DO**:
- Keep plugins updated
- Read changelog
- Check compatibility
- Update main mod first

❌ **DON'T**:
- Use outdated plugins
- Mix plugin versions
- Skip update notes

### 4. Removal

✅ **DO**:
- Clean uninstall
- Remove from config
- Restart game
- Verify removal

❌ **DON'T**:
- Delete while running
- Leave in config
- Keep old versions

## Best Practices for Developers

### 1. Error Handling

```cpp
// Always wrap potentially failing code
try {
    // Your code
}
catch (const std::exception& e) {
    // Log error
    return;
}
catch (...) {
    // Log unknown error
    return;
}
```

### 2. Resource Management

```cpp
// Use RAII for automatic cleanup
class PluginResource {
    HANDLE handle;
public:
    PluginResource() : handle(CreateHandle()) {}
    ~PluginResource() { if (handle) CloseHandle(handle); }
};
```

### 3. Input Validation

```cpp
// Validate all inputs
if (!pointer) return;
if (index >= count) return;
if (value < min || value > max) return;
```

### 4. Thread Safety

```cpp
// Use synchronization for shared data
std::mutex g_DataMutex;

void SafeModify() {
    std::lock_guard<std::mutex> lock(g_DataMutex);
    // Modify data
}
```

### 5. Memory Safety

```cpp
// No raw pointers
std::unique_ptr<Data> data = std::make_unique<Data>();

// Bounds checking
if (index < vector.size()) {
    auto& element = vector[index];
}

// Safe casts
if (auto* derived = dynamic_cast<Derived*>(base)) {
    // Use derived
}
```

### 6. Logging

```cpp
// Log important events
void OnStart() {
    LOG("MyEffect starting");
    // Effect logic
    LOG("MyEffect started successfully");
}
```

## Security Checklist

### For Plugin Users

- [ ] Plugin from trusted source?
- [ ] Source code reviewed?
- [ ] Malware scan performed?
- [ ] User reviews checked?
- [ ] Compatible with mod version?
- [ ] Tested in safe environment?
- [ ] Backup created?
- [ ] Update available?

### For Plugin Developers

- [ ] All errors handled?
- [ ] Resources cleaned up?
- [ ] Input validated?
- [ ] Thread-safe?
- [ ] Memory-safe?
- [ ] Logging added?
- [ ] Documentation complete?
- [ ] Test coverage adequate?

## Reporting Security Issues

If you discover a security vulnerability:

1. **DO NOT** post publicly
2. Report to mod authors privately
3. Provide detailed information
4. Wait for fix before disclosure
5. Credit will be given

## Security Updates

The plugin API may be updated for security reasons:

1. API version will be incremented
2. Old plugins will be rejected
3. Security advisory will be published
4. Migration guide will be provided

## Conclusion

The plugin system is designed with security in mind, but native code plugins have inherent risks. Users should only load trusted plugins, and developers should follow security best practices.

**Remember**: With great power comes great responsibility!

## Additional Resources

- **PLUGIN_DEVELOPMENT.md**: Development guide
- **ARCHITECTURE.md**: System design
- **Discord**: Community support
- **GitHub Issues**: Bug reports
