﻿using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Localization;
using Kingmaker.Localization.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ExpandedContent.Config;
using ExpandedContent.Utilities;

namespace ExpandedContent.Localization {
    [JsonObject(MemberSerialization.OptIn)]
    class MultiLocalizationPack {

        public MultiLocalizationPack() { }

        public SortedDictionary<string, MultiLocaleString> Text {
            get {
                if (text == null) {
                    text = new SortedDictionary<string, MultiLocaleString>();
                    foreach (var entry in Strings) {
                        text[entry.enGB] = entry;
                    }
                }
                return text;
            }
        }
        public SortedDictionary<string, MultiLocaleString> Ids {
            get {
                if (ids == null) {
                    ids = new SortedDictionary<string, MultiLocaleString>();
                    foreach (var entry in Strings) {
                        ids[entry.Key] = entry;
                    }
                }
                return ids;
            }
        }

        public void ApplyToCurrentPack() {
            LocalizationManager.CurrentPack.AddStrings(GeneratePack());
        }

        public void ResetCache() {
            ids = null;
            text = null;
        }

        public bool TryGetText(string text, out MultiLocaleString result, Locale locale = Locale.enGB) {
            return Text.TryGetValue(text, out result);
        }

        public void AddString(MultiLocaleString newString) {
            Ids[newString.Key] = newString;
            Text[newString.StringEntry(LocalizationManager.CurrentLocale).Text] = newString;
            Strings.Add(newString);
            LocalizationManager.CurrentPack.m_Strings[newString.Key] = newString.StringEntry(LocalizationManager.CurrentLocale);
        }

        private LocalizationPack GeneratePack() {
            var pack = new LocalizationPack {
                Locale = LocalizationManager.CurrentPack.Locale,
                m_Strings = new Dictionary<string, LocalizationPack.StringEntry>()
            };
            foreach (var entry in Strings) {
                pack.m_Strings[entry.Key] = entry.StringEntry(pack.Locale);
            }
            return pack;
        }

        [NotNull]
        [JsonProperty(PropertyName = "LocalizedStrings")]
        public List<MultiLocaleString> Strings = new List<MultiLocaleString>();
        private SortedDictionary<string, MultiLocaleString> text;
        private SortedDictionary<string, MultiLocaleString> ids;

        [JsonObject(MemberSerialization.OptIn)]
        public class MultiLocaleString {
            [JsonProperty]
            public string Key;
            [JsonProperty]
            public string SimpleName;
            [JsonProperty]
            public bool ProcessTemplates;
#if DEBUG
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            [DefaultValue(true)]
            private bool IsUsed = false;
#endif
            [JsonProperty]
            public string enGB = "";
            [JsonProperty]
            public string ruRU;
            [JsonProperty]
            public string deDE;
            [JsonProperty]
            public string frFR;
            [JsonProperty]
            public string zhCN;
            [JsonProperty]
            public string esES;
            private LocalizedString m_LocalizedString;
            public LocalizedString LocalizedString {
                get {
                    m_LocalizedString ??= new LocalizedString {
                        m_Key = Key,
                        m_ShouldProcess = ProcessTemplates
                    };
#if DEBUG
                    IsUsed = true;
#endif
                    return m_LocalizedString;
                }
            }
            public MultiLocaleString() { }
            public MultiLocaleString(string simpleName, string text, bool shouldProcess = false, Locale locale = Locale.enGB) {
                ProcessTemplates = shouldProcess;
                SimpleName = simpleName;
                Key = Guid.NewGuid().ToString("N");
                SetText(locale, text);
            }

            public void SetText(Locale locale, string text) {
                switch (locale) {
                    case Locale.enGB:
                        enGB = text;
                        break;
                    case Locale.ruRU:
                        ruRU = text;
                        break;
                    case Locale.deDE:
                        deDE = text;
                        break;
                    case Locale.frFR:
                        frFR = text;
                        break;
                    case Locale.zhCN:
                        zhCN = text;
                        break;
                    case Locale.esES:
                        esES = text;
                        break;
                    default:
                        enGB = text;
                        break;
                }
            }

            public LocalizationPack.StringEntry StringEntry(Locale locale = Locale.enGB) {
                string result;
                switch (locale) {
                    case Locale.enGB:
                        result = enGB;
                        break;
                    case Locale.ruRU:
                        result = ruRU;
                        break;
                    case Locale.deDE:
                        result = deDE;
                        break;
                    case Locale.frFR:
                        result = frFR;
                        break;
                    case Locale.zhCN:
                        result = zhCN;
                        break;
                    case Locale.esES:
                        result = esES;
                        break;
                    default:
                        result = enGB;
                        break;
                }
                if (string.IsNullOrEmpty(result)) {
                    result = enGB;
                }
                return new LocalizationPack.StringEntry {
                    Text = ProcessTemplates ? DescriptionTools.TagEncyclopediaEntries(result) : result
                };
            }
            public override string ToString() {
                return this.StringEntry(LocalizationManager.CurrentLocale).Text;
            }
            public override int GetHashCode() {
                return Key.GetHashCode() ^ enGB.GetHashCode();
            }
        }
    }
    [HarmonyPatch(typeof(StartGameLoader), nameof(StartGameLoader.LoadPackTOC))]
    public static class StartGameLoader_LocalizationPatch {
        static void Postfix() {
            ModSettings.ModLocalizationPack.ApplyToCurrentPack();
            ModSettings.SaveLocalization("LocalizationPack.Json", ModSettings.ModLocalizationPack);
        }
    }
    [HarmonyPatch(typeof(LocalizationManager), nameof(LocalizationManager.OnLocaleChanged))]
    public static class LocalizationManager_OnLocaleChanged_LocalizationPatch {
        static void Postfix() {
            ModSettings.ModLocalizationPack.ResetCache();
            ModSettings.ModLocalizationPack.ApplyToCurrentPack();
        }
    }
    [HarmonyPatch(typeof(BlueprintsCache), nameof(BlueprintsCache.Init))]
    public static class BlueprintsCache_LocalizationPatch {
        static bool Prefix() {
            ModSettings.ModLocalizationPack.ApplyToCurrentPack();
            return true;
        }
    }
}
