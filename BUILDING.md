# Building UI Scale

## Prerequisites

Use Windows PowerShell and the .NET Framework 3.5 C# compiler. The default compiler location is `%WINDIR%\Microsoft.NET\Framework\v3.5\csc.exe`. The build stops with an explanation if prerequisites are missing; it does not install them.

Provide your own Shadowrun Returns installation with BepInEx 5.4.23.2 x86. The four required references are:

- `BepInEx/core/BepInEx.dll`
- `BepInEx/core/0Harmony.dll`
- `Shadowrun_Data/Managed/Assembly-CSharp.dll`
- `Shadowrun_Data/Managed/UnityEngine.dll`

The tested game uses Unity 4.2 and Electric 6.2. References from different installations may compile but are not thereby proven compatible. Do not upload proprietary game assemblies to this repository.

## Command

From the repository folder, run this command with your actual game location:

```powershell
.\build.ps1 -GamePath 'C:\Games\Shadowrun Returns'
```

If needed, supply `-CompilerPath` pointing to a compatible compiler. Only the .NET Framework 3.5 compiler has been validated for this project.

The script compiles the plugin, compiles and runs the existing scale tests, then leaves output in ignored `artifacts/`:

- `UIScale.dll`: plugin build.
- `ScaleTests.exe`: development test executable, not a mod dependency.

The game installation is read only. The script does not install the plugin, launch the game, download dependencies, or publish files. Review the script before running it; no execution-policy change is required by the script itself.

## Scope of reproducibility

These steps allow rebuilding from source with locally supplied dependencies. They do not promise a byte-identical DLL: compiler metadata and dependency versions can affect its hash. Release evidence should identify the source commit, compiler/dependency versions and actual distributed binary hashes.

## Player package (later release step)

Package the plugin under `BepInEx/plugins/UIScale/`, with README and LICENSE. Keep build/test executables and game/runtime dependencies out of the player ZIP. GitHub's automatic source archive is not the player installation package.
