using UnityEngine;
using UnityModManagerNet;
using UnityEngine.UI;
using System;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using UnnamedWotrMod.Feats;
using BlueprintCore.Utils;
using JetBrains.Annotations;
using ExpandedContent.Config;
using ExpandedContent.Utilities;

namespace UnnamedWotrMod
{
    public static class Main
    {
        public static bool Enabled;
        private static LogWrapper Logger = LogWrapper.Get("BladedBrush");

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                modEntry.OnToggle = OnToggle;
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll();
                PostPatchInitializer.Initialize();
                Logger.Info("Finished patching.");
            }
            catch (Exception e)
            {
                Logger.Error("Failed to patch", e);
            }
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }

        [HarmonyPatch(typeof(BlueprintsCache))]
        static class BlueprintsCaches_Patch
        {
            private static bool Initialized = false;

            [HarmonyPriority(Priority.First)]
            [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
            static void Postfix()
            {
                try
                {
                    if (Initialized)
                    {
                        Logger.Info("Already initialized blueprints cache.");
                        return;
                    }
                    Initialized = true;

                    BladedBrush.Configure();
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to initialize.", e);
                }
            }
        }

        internal static void LogPatch(string v, object coupDeGraceAbility)
        {
            throw new NotImplementedException();
        }

        public static void Log(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        public static void LogPatch(string action, [NotNull] IScriptableObjectWithAssetId bp)
        {
            Log($"{action}: {bp.AssetGuid} - {bp.name}");
        }
        public static void LogHeader(string msg)
        {
            Log($"--{msg.ToUpper()}--");
        }
        public static void Error(Exception e, string message)
        {
            Log(message);
            Log(e.ToString());
        }
        public static void Error(string message)
        {
            Log(message);
        }

        internal static void LogPatch(string v, Action addPulura)
        {
            throw new NotImplementedException();
        }
    }
}