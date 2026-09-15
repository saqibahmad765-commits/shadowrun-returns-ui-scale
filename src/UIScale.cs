using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace ShadowrunUIScale
{
    [BepInPlugin("local.shadowrun.ui-scale", "UI Scale", "1.1.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        private static ConfigEntry<float> scale;
        private static ConfigEntry<bool> enabledSetting;
        private static bool bypass;
        private static ConfigEntry<bool> hotkeys;
        private static bool ready;
        private Harmony harmony;

        private void Awake()
        {
            enabledSetting = Config.Bind<bool>("Interface", "Enabled", true, "Scale UI panels, controls and text together.");
            scale = Config.Bind<float>("Interface", "Scale", 1.15f, "1 = original, 1.15 = 15% larger. Supported range 1 to 1.5. F9 reloads this file; F10 toggles original size temporarily. Large values may crop menus.");
            hotkeys = Config.Bind<bool>("Shortcuts", "EnableHotkeys", true, "F9 reloads config; F10 toggles UI size. Reserves these keys by blocking the game quick-load-screen and diagnostic-dump shortcuts. Set false and restart to restore the game shortcuts.");
            harmony = new Harmony("local.shadowrun.ui-scale");
            try
            {
                MethodInfo target = AccessTools.Method(typeof(UIRoot), "Update", Type.EmptyTypes, null);
                if (target == null) throw new MissingMethodException("UIRoot.Update");
                MethodInfo patch = typeof(Plugin).GetMethod("Resize", BindingFlags.NonPublic | BindingFlags.Static);
                harmony.Patch(target, null, null, new HarmonyMethod(patch), null, null);
                MethodInfo guard = typeof(Plugin).GetMethod("AllowGameShortcut", BindingFlags.NonPublic | BindingFlags.Static);
                foreach (string name in new string[] { "InputQuickLoadGame", "InputDump" })
                {
                    MethodInfo action = AccessTools.Method(typeof(RunManager), name, Type.EmptyTypes, null);
                    if (action == null) throw new MissingMethodException("RunManager." + name);
                    harmony.Patch(action, new HarmonyMethod(guard), null, null, null, null);
                }
                ready = true;
                Logger.LogInfo("UI Scale 1.1.0 loaded. Requested multiplier: " + scale.Value + ". F9 reloads; F10 toggles original size. Hotkeys enabled: " + hotkeys.Value + ". Game shortcut conflicts guarded.");
            }
            catch (Exception ex)
            {
                ready = false;
                harmony.UnpatchSelf();
                Logger.LogError("UI Scale disabled because the UI hook failed: " + ex);
            }
        }

        // Modify the freshly computed root scale, before its comparison and assignment.
        // This avoids cumulative scaling and avoids resetting/rescaling the root twice per frame.
        private static IEnumerable<CodeInstruction> Resize(IEnumerable<CodeInstruction> input)
        {
            List<CodeInstruction> code = new List<CodeInstruction>(input);
            int count = 0;
            for (int i = 0; i < code.Count; i++) if (code[i].opcode == OpCodes.Div) count++;
            if (count != 1) throw new InvalidOperationException("Unsupported UIRoot.Update: expected one scale division, found " + count);
            int division = code.FindIndex(delegate(CodeInstruction c) { return c.opcode == OpCodes.Div; });
            FieldInfo height = division >= 2 ? code[division - 2].operand as FieldInfo : null;
            if (height == null || height.DeclaringType != typeof(UIRoot) || height.Name != "manualHeight"
                || code[division - 1].opcode != OpCodes.Conv_R4)
                throw new InvalidOperationException("Unsupported UIRoot.Update scale calculation; no changes applied.");
            List<CodeInstruction> result = new List<CodeInstruction>();
            MethodInfo multiplier = typeof(Plugin).GetMethod("Multiplier", BindingFlags.NonPublic | BindingFlags.Static);
            foreach (CodeInstruction instruction in code)
            {
                result.Add(instruction);
                if (instruction.opcode == OpCodes.Div)
                {
                    result.Add(new CodeInstruction(OpCodes.Ldarg_0, null));
                    result.Add(new CodeInstruction(OpCodes.Call, multiplier));
                    result.Add(new CodeInstruction(OpCodes.Mul, null));
                }
            }
            return result;
        }

        private static float Multiplier(UIRoot root)
        {
            if (bypass || enabledSetting == null || !enabledSetting.Value || scale == null) return 1f;
            // Nested roots inherit their parent's scale; do not enlarge twice.
            Transform parent = root.transform.parent;
            while (parent != null)
            {
                UIRoot ancestor = parent.GetComponent<UIRoot>();
                if (ancestor != null && ancestor.enabled) return 1f;
                parent = parent.parent;
            }
            return ScalePolicy.Clamp(scale.Value);
        }

        // Reserve both key-down and key-up game actions while our shortcuts are enabled.
        // The normal load-game menu remains available; only its keyboard handler is patched.
        private static bool AllowGameShortcut()
        {
            return !ready || hotkeys == null || !hotkeys.Value;
        }

        private void Update()
        {
            if (!ready || hotkeys == null || !hotkeys.Value) return;
            if (Input.GetKeyDown(KeyCode.F9))
            {
                try { Config.Reload(); Logger.LogInfo("UI config reloaded. Effective multiplier: " + ScalePolicy.Clamp(scale.Value)); }
                catch (Exception ex) { Logger.LogWarning("Could not reload UI config: " + ex.Message); }
            }
            if (Input.GetKeyDown(KeyCode.F10))
            {
                bypass = !bypass;
                Logger.LogInfo(bypass ? "Original UI size temporarily restored." : "Configured UI size restored.");
            }
        }

        private void OnDestroy()
        {
            ready = false;
            if (harmony != null) harmony.UnpatchSelf();
            bypass = false;
        }
    }
}


