# Shahidi.VR XR Application

**Version:** 0.1.0 (MVP)
**Unity Requirement:** Unity 2022.3.14f1 LTS

## Overview
This Unity project implements the core XR application for immersive exposure therapy on Meta Quest 3. It includes:
- Passthrough‐blending exposure engine
- Real‐time head‐pose & ECG telemetry streaming
- REST/WebSocket link to a therapist console

## Prerequisites
1. Install **Unity Hub** and add **Unity 2022.3.14f1 LTS**.
2. Install **Android Build Support** (Android SDK, NDK, OpenJDK).
3. In Unity's **Package Manager**, install:
   - **Oculus XR Plugin** (v3.6.0+)  
   - **OpenXR Plugin**
4. In **Project Settings → XR Plug‐in Management**, enable **Meta Quest** for Android.
5. In **OpenXR Features**, enable **Oculus Touch Controller Profile** and **Passthrough Support**.

## Setup
1. Create a new Unity project (3D template).
2. Copy the provided `Assets/Scenes/MainScene.unity` and `Assets/Scripts/` folder into your project.
3. In **Assets/Scenes**, open **MainScene.unity**.  
   - Add an empty GameObject named **"XRAppSettings"** and attach `XRAppSettings.cs`.
   - Create a new empty GameObject **"ExposureEngine"**, attach `ExposureEngine.cs`, and assign a passthrough‐blend material (see below).
   - Create an empty GameObject **"TelemetryManager"** and attach `TelemetryManager.cs`.
   - Create an empty GameObject **"TherapistConsole"** and attach `TherapistConsoleAPI.cs`, then drag the **ExposureEngine** object into its `engine` slot.
4. **Passthrough Material**: In **Assets → Create → Material**, name it `PassthroughBlend.mat`. In its shader drop‑down select **Oculus/Spatial Passthrough** (or **Unlit/Texture** if unavailable). Expose a float parameter `_VirtualAlpha` in the shader.
5. Build Settings:  
   - **File → Build Settings → Android**  
   - Switch Platform  
   - Tick **Export Project** (optional)  
   - Player Settings: Scripting Backend=IL2CPP, Target Architectures=ARM64.

## Build & Run
1. Connect your Quest 3 via USB‑C and enable Developer Mode.
2. In **Build Settings**, click **Build and Run**.
3. The app will install and launch on your headset.

## Scripts

### XRAppSettings.cs
```csharp
using UnityEngine;
using UnityEngine.XR.Management;

public class XRAppSettings : MonoBehaviour
{
    void Awake()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Debug.Log("XR subsystems initialized.");
    }

    void OnDestroy()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
    }
}
