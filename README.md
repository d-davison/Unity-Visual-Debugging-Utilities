# Unity-Visual-Debbugging-Utilities

Unreal-style on-screen and world-space debug visualization for Unity. This package mimics Unreal Engine’s `GEngine->AddOnScreenDebugMessage` and `DrawDebugSphere` (plus a few more helpers) so you can quickly visualize gameplay logic, AI, physics, and more in editor or builds.

- Developed with Unity 6000.3.12f1
- Namespace: `Dandev.Unity_Visual_Debugging_Utilities`

---

![Roundabout Traffic gameplay](https://github.com/d-davison/Unity-Visual-Debugging-Utilities/blob/main/Media/random-world-debugging.gif)

## Contents

- [Overview](#overview)
- [Quick start](#quick-start)  
- [Unreal equivalents](#unreal-equivalents)  
- [Requirements and setup](#requirements-and-setup)  
- [Usage examples](#usage-examples)  
  - [On-screen messages](#on-screen-messages)  
  - [World-space shapes and text](#world-space-shapes-and-text)  
- [Always On Top](#always-on-top)  
- [License and contributions](#license-and-contributions)

---

## Overview

This utility provides:

- On-screen HUD messages with duration and color (Unreal-style `AddOnScreenDebugMessage`)
- World-space debug drawing: sphere, cube, text, and arrow (akin to Unreal’s `DrawDebug*` helpers)
- Zero scene setup: singletons auto-initialize on first use and persist across scenes
- Shapes render on top of the scene to remain visible even when occluded

---

## Quick start

To install the package in to your Unity project:
1. Head to Window -> Package Management -> Package Manager.
2. From there, click the + in the top left, and click "Add package from git url..."
3. Add this url https://github.com/d-davison/Unity-Visual-Debugging-Utilities.git?path=/Unity-Visual-Debugging-Utilities-Project/Assets/Plugins/Dandev
4. Click Install

To use the package, make sure to add the namespace:
```csharp
using Dandev.Unity_Visual_Debugging_Utilities;
```

Then call the static methods from anywhere (Update, coroutines, gameplay code, etc.). The first call auto-creates the required GameObjects and loads their controllers from Resources.

---

## Unreal equivalents

If you’ve used Unreal’s debug helpers, these should feel familiar:

- Unreal (C++):
  - `GEngine->AddOnScreenDebugMessage(...)`
  - `DrawDebugSphere(...)`
  - Related: `DrawDebugBox(...)`, `DrawDebugDirectionalArrow(...)`, `DrawDebugString(...)`

- Unity equivalents in this package:
  - `DebugScreen.Log(...)` → on-screen messages
  - `DebugDraw.Sphere(...)` → world-space sphere
  - `DebugDraw.Cube(...)` → world-space cube (Unreal’s `DrawDebugBox`)
  - `DebugDraw.Arrow(...)` → world-space arrow (Unreal’s `DrawDebugDirectionalArrow`)
  - `DebugDraw.Text(...)` → world-space text (Unreal’s `DrawDebugString`)

Example (Unreal C++):
```cpp
// On-screen message
if (GEngine)
{
    GEngine->AddOnScreenDebugMessage(-1, 2.f, FColor::Yellow, TEXT("Hello, Unreal!"));
}

// World-space sphere
DrawDebugSphere(GetWorld(), FVector(0,0,100), 50.f, 12, FColor::Green, false, 5.f);
```

Equivalent (Unity C# with this package):
```csharp
using Dandev.Unity_Visual_Debugging_Utilities;
using UnityEngine;

// On-screen message
DebugScreen.Log("Hello, Unity!", 2f, Color.yellow);

// World-space sphere (position, duration, size, color)
DebugDraw.Sphere(new Vector3(0, 1, 0), 5f, 1f, Color.green);
```

---

## Requirements and setup

- Ensure controller prefabs exist at the expected Resources paths:
  - `DebugScreen` loads `DebugScreenController` from `Resources/DebugScreenController`
  - `DebugDraw` loads `DebugDrawController` from `Resources/<DebugUtilities.DebugDrawControllerPath>`
    - Set `DebugUtilities.DebugDrawControllerPath` or place the prefab accordingly
- Rendering pipeline:
  - `DebugDraw` includes `using UnityEngine.Rendering.Universal;`
  - Works with URP out of the box if your `DebugDrawController` is configured for URP
  - For Built-in/HDRP, remove/replace the URP directive and ensure your controller supports your pipeline
- No scene setup is required. The utilities auto-initialize and persist via `DontDestroyOnLoad`.

---

## Usage examples

### On-screen messages
![Roundabout Traffic gameplay](https://github.com/d-davison/Unity-Visual-Debugging-Utilities/blob/main/Media/screen-debugger.gif)

```csharp
// Basic message for 2 seconds in yellow
DebugScreen.Log("Hello, world!", 2f, Color.yellow);

// Update a stat every frame (short lifetime so it refreshes smoothly)
void Update()
{
    DebugScreen.Log($"Speed: {currentSpeed:0.0}", 0.1f, Color.cyan);
}

// Conditional warning
if (!hasTarget)
{
    DebugScreen.Log("No target found", 1.5f, Color.red);
}
```

### World-space shapes and text
![Roundabout Traffic gameplay](https://github.com/d-davison/Unity-Visual-Debugging-Utilities/blob/main/Media/random-world-debugging.gif)

```csharp
// Sphere at a position for 5 seconds
DebugDraw.Sphere(transform.position, 5f, 1f, Color.green);

// Cube at a position (no rotation)
DebugDraw.Cube(new Vector3(0, 1, 0), 3f, 0.5f, Color.magenta);

// Rotated cube (rotation as Euler angles)
DebugDraw.Cube(new Vector3(0, 1, 0), new Vector3(0, 45f, 0), 4f, 1.2f, Color.blue);

// Text label in world space
DebugDraw.Text(target.position + Vector3.up * 2f, "Target", 3f, 1f, Color.white);

// Arrow indicating a direction
Vector3 dir = (goal.position - agent.position).normalized;
DebugDraw.Arrow(agent.position, dir, 2.5f, 1f, Color.red);
```

### DebugScreen
```csharp
public static void Log(string label, float time, Color color)
```
- label: The text to display.
- time: Lifetime in seconds.
- color: Message color.

### DebugDraw
```csharp
public static void Sphere(Vector3 position, float duration = 5f, float size = 1f, Color color = default)
public static void Cube(Vector3 position, float duration = 5f, float size = 1f, Color color = default)
public static void Cube(Vector3 position, Vector3 rotation, float duration = 5f, float size = 1f, Color color = default)
public static void Text(Vector3 position, string label, float duration = 5f, float size = 1f, Color color = default)
public static void Arrow(Vector3 position, Vector3 direction, float duration = 5f, float size = 1f, Color color = default)
```

Parameters (common):
- `position`: World position for the shape/text.
- `rotation`: Euler angles (degrees) for rotated cube overload.
- `direction`: Direction vector for the arrow.
- `duration`: Time in seconds before the item is removed.
- `size`: General size/scale of the shape or text.
- `color`: Optional; if omitted, the controller’s default color is used.

---

### Always on Top
Note that the shapes will always appear on top!
![Roundabout Traffic gameplay](https://github.com/d-davison/Unity-Visual-Debugging-Utilities/blob/main/Media/world-debugging-always-on-top.gif)

## License and contributions

- Feel free to use this in your projects; no attribution needed.
- PRs and issues welcome. When filing an issue, please include:
  - Unity version (e.g., 6000.3.12f1)
  - Render pipeline (URP/Built-in/HDRP)
  - Repro steps and screenshots/GIFs where possible.
