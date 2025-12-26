cmake_minimum_required(VERSION 3.15)
project(SampleEffectPlugin)

# Set C++ standard
set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Source files
set(SOURCES
    SamplePlugin.cpp
)

# Create the plugin DLL
add_library(SampleEffectPlugin MODULE ${SOURCES})

# Set output name
set_target_properties(SampleEffectPlugin PROPERTIES
    OUTPUT_NAME "SampleEffectPlugin"
    SUFFIX ".dll"
)

# Platform-specific settings
if (CMAKE_CXX_COMPILER_ID MATCHES "MSVC")
    target_compile_options(SampleEffectPlugin PRIVATE /W3)
elseif(CMAKE_CXX_COMPILER_ID MATCHES "GNU|Clang")
    target_compile_options(SampleEffectPlugin PRIVATE -Wall -Wextra)
endif()

# Install target
install(TARGETS SampleEffectPlugin DESTINATION .)
