# UI Scale for Shadowrun Returns
## Download the mod — no compiling required

Open the [Releases page](https://github.com/saqibahmad765-commits/shadowrun-returns-ui-scale/releases) and download **UIScale-1.1.0.zip** from the release's **Assets** section. The automatic **Source code** downloads are for developers, not installation. If no public release is listed yet, the player download is still being prepared.

Close the game, then extract the ZIP into the game folder. BepInEx 5.4.23.2 x86 is required; see below.

Configurable UI and text scaling using BepInEx. Version 1.1.0.

Scales NGUI text, panels, buttons and associated controls together. The default is 115%; choose a scale from 100% to 150%. This is global scaling, not a redesign of individual screens: higher values can clip menus or leave less space for dialogue.

## Requirements

- Shadowrun Returns on Windows (32-bit game).
- [BepInEx 5.4.23.2 x86](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2).
- Developed against Unity 4.2 and an Electric 6.2-modified installation. Compatibility with an unmodified installation or other versions is not established.

Game assemblies and third-party runtime binaries are not distributed here. Camera Comfort and the separate text-scaling experiment are not part of this project.

## Installation

Close the game. Extract the player release ZIP into the game directory, so the plugin is at `BepInEx/plugins/UIScale/UIScale.dll`. Replace the previous DLL; do not retain duplicate versions in other plugin folders. Restart the game. Existing configuration is retained.

This source repository is not itself an installable mod ZIP. Player release packaging is a separate step.

## Configuration

After the first launch, edit `BepInEx/config/local.shadowrun.ui-scale.cfg`:

```ini
[Interface]
Enabled = true
Scale = 1.15

[Shortcuts]
EnableHotkeys = true
```

`1.15` means 115%; `1.22` means 122%. Values are limited to 1.0-1.5. Invalid non-finite values use original size.

- **F9:** reload the saved configuration. No visual change is expected if the scale is unchanged. If F10 has selected original size, toggle back to see the configured scale.
- **F10:** temporarily toggle original/configured UI size. This does not edit the configuration.
- **F8 is not a mod shortcut:** the game's native F8 action can change resolution.

While shortcuts are enabled, this plugin blocks the game's F9 load-screen handler and F10 diagnostic-dump handler. Normal menu loading is intended to remain available. To restore these native shortcuts, set `EnableHotkeys = false` with the game closed and restart. UI scaling remains available without shortcuts.

To uninstall, close the game and remove the UIScale plugin folder. Its configuration can optionally be removed. To roll back, replace the DLL with the earlier release; version 1.0.0 has the old F8/F9 shortcut conflicts.

## How it works

A Harmony transpiler adjusts the freshly computed scale in `UIRoot.Update`. Nested active roots are excluded to avoid double enlargement. Two Harmony prefixes reserve the game handlers mentioned above. The plugin calls no resolution/fullscreen setter and does not modify saves. BepInEx manages the configuration file.

## Validation and limitations

F10's visible size toggle was tested successfully by the user. The game log confirms 1.1.0 loaded and F9 reloaded the configuration at 1.22. A changed-value F9 visual test and direct confirmation of absence of the load-screen/diagnostic side effects remain outstanding.

The existing nine tests cover scale boundaries, not Unity/Harmony integration. Text outside NGUI is not affected. Test dialogue, inventory, shops and menus at your chosen size. See [VALIDATION.md](VALIDATION.md).

## Building and review

See [BUILDING.md](BUILDING.md), [CHANGELOG.md](CHANGELOG.md) and the source in `src/`. No network downloads, installation or game launching are performed by the build script.

[Nexus page](https://www.nexusmods.com/shadowrunreturns/mods/284). The reported quarantine cause has not been established; source availability and passing tests are not a security certification.

## License

MIT; see [LICENSE](LICENSE). This license covers this project's code, not the game or third-party dependencies. Developed with OpenAI Codex assistance for code, documentation and packaging, with requirements and in-game testing provided by the mod author.
