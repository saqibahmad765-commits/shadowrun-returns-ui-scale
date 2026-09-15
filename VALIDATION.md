# Validation record

Stage 1: local repository preparation, 2026-09-15.

## Verified

- Built successfully with the Windows .NET Framework 3.5 compiler against the current installed game and BepInEx references.
- All nine existing scale-boundary tests passed.
- Both source files match the prepared 1.1.0 source byte-for-byte. No plugin behaviour was changed during repository preparation.
- User reports F10 successfully toggles the modded UI back to original size.
- Current game log shows `UI Scale 1.1.0 loaded`, hotkeys enabled, and repeated `UI config reloaded. Effective multiplier: 1.22` messages. This establishes F9's reload handler ran successfully.
- Build products are under `artifacts/`, excluded by `.gitignore`. Only source, tests, scripts, documentation and the existing MIT license are intended for source control.
- No game assemblies or runtime dependency binaries were copied into the source tree.
- No remote is configured; nothing was published or uploaded. No installed game files were changed.

## Not established

- A visible F9 reload after changing the saved scale value; unchanged values need not produce any visible effect.
- Direct observation that F9/F10 do not invoke load-screen/diagnostic actions, and that normal menu loading remains available. Code contains the relevant guards, but log messages alone are not proof of every interaction.
- Comprehensive layout coverage, compatibility with an unmodified game or other plugin combinations.
- Nexus quarantine cause, scanner clearance or a byte-identical reproducible build.

## Focused remaining gameplay check

When convenient, confirm configured scaling is active, change the saved Scale value slightly, press F9, and confirm the UI changes without a load screen or resolution change. Restore the preferred value and reload again. Check F10 toggles twice, then confirm normal menu access to Load Game. Do not load a save merely to test menu access.

No additional automated test suite or gameplay features were added in this stage.
