# Changelog

## 1.1.0

- Move config reload from F8 to F9 and the UI size toggle from F9 to F10.
- Reserve F9/F10 by guarding the native quick-load-screen and diagnostic-dump handlers while mod shortcuts are enabled.
- Add `Shortcuts.EnableHotkeys` to allow restoring native shortcuts.
- Keep the original scaling algorithm and existing configuration values.
- Repository preparation: parameterised local build, documentation and ignored build outputs. Plugin source unchanged from the prepared 1.1.0 release.

## 1.0.0

- Initial configurable NGUI scaling, 1.0-1.5, with nested-root protection.
- Original shortcuts: F8 reload and F9 size toggle. These overlap native resolution/load-screen actions; use 1.1.0 for the revised shortcuts.
