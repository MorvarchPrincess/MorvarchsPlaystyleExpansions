﻿using UnityEngine;
using UnityModManagerNet;
using UnityEngine.UI;
using System;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using MorvarchsPlaystyleExpansions.Feats;
using BlueprintCore.Utils;
using MorvarchsPlaystyleExpansions.Classes;

namespace MorvarchsPlaystyleExpansions
{
    public static class Main
    {
        public static bool Enabled;
        private static readonly LogWrapper Logger = LogWrapper.Get("MorvarchsPlaystyleExpansions");

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                modEntry.OnToggle = OnToggle;
                var harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll();
                Logger.Info("Finished patching.");
            }
            catch (Exception e)
            {
                Logger.Error("Failed to patch", e);
            }
            return true;
        }

        public static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
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

                    Logger.Info("Patching blueprints.");
                    DivineFightingTechniques.Configure();
                    BladedBrush.Configure();
                    DivineSignifier.Configure();
                    HellknightShadow.Configure();
                }
                catch (Exception e)
                {
                    Logger.Error("Failed to initialize.", e);
                }
            }
        }

        public static void Log(string msg)
        {
            Logger.Info(msg);
        }
    }
}