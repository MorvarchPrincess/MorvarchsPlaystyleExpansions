﻿using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using ExpandedContent.Localization;
using UnnamedWotrMod;
using static UnityModManagerNet.UnityModManager;

namespace ExpandedContent.Config {
    class ModSettings {
        public static ModEntry ModEntry;
        
        public static AddedContent AddedContent;
        
        public static Blueprints Blueprints;
        public static MultiLocalizationPack ModLocalizationPack = new MultiLocalizationPack();

        private static string UserConfigFolder => ModEntry.Path + "UserSettings";
        private static string localizationFolder => ModEntry.Path + "Localization";

        private static JsonSerializerSettings cachedSettings;
        private static JsonSerializerSettings SerializerSettings {
            get {
                if (cachedSettings == null) {
                    cachedSettings = new JsonSerializerSettings {
                        CheckAdditionalContent = false,
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                        DefaultValueHandling = DefaultValueHandling.Include,
                        FloatParseHandling = FloatParseHandling.Double,
                        Formatting = Formatting.Indented,
                        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                        MissingMemberHandling = MissingMemberHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore,
                        ObjectCreationHandling = ObjectCreationHandling.Replace,
                        StringEscapeHandling = StringEscapeHandling.Default,
                    };
                }
                return cachedSettings;
            }
        }

        public static void LoadAllSettings() {
            
            LoadSettings("AddedContent.json", ref AddedContent);
            
            LoadSettings("Blueprints.json", ref Blueprints);
        }
        public static void LoadLocalization() {
            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            var fileName = "LocalizationPack.json";
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"ExpandedContent.Localization.{fileName}"; ;
            var localizationPath = $"{localizationFolder}{Path.DirectorySeparatorChar}{fileName}";
            Directory.CreateDirectory(localizationFolder);
            if (File.Exists(localizationPath)) {
                using (StreamReader streamReader = File.OpenText(localizationPath))
                using (JsonReader jsonReader = new JsonTextReader(streamReader)) {
                    try {
                        MultiLocalizationPack localization = serializer.Deserialize<MultiLocalizationPack>(jsonReader);
                        ModLocalizationPack = localization;
                    } catch {
                        ModLocalizationPack = new MultiLocalizationPack();
                        try { File.Copy(localizationPath, ModEntry.Path + $"{Path.DirectorySeparatorChar}BROKEN_{fileName}", true); } catch {  }
                    }
                }
            } else {
                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader streamReader = new StreamReader(stream))
                using (JsonReader jsonReader = new JsonTextReader(streamReader)) {
                    ModLocalizationPack = serializer.Deserialize<MultiLocalizationPack>(jsonReader);
                }
            }
        }
        public static void SaveLocalization(string fileName, MultiLocalizationPack localizaiton) {
            localizaiton.Strings.Sort((x, y) => string.Compare(x.SimpleName, y.SimpleName));
            Directory.CreateDirectory(UserConfigFolder);
            var localizationPath = $"{localizationFolder}{Path.DirectorySeparatorChar}{fileName}";

            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            using (StreamWriter streamWriter = new StreamWriter(localizationPath))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter)) {
                serializer.Serialize(jsonWriter, localizaiton);
            }
        }
        private static void LoadSettings<T>(string fileName, ref T setting) where T : IUpdatableSettings {
            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = $"ExpandedContent.Config.{fileName}";
            var userPath = $"{UserConfigFolder}{Path.DirectorySeparatorChar}{fileName}";

            Directory.CreateDirectory(UserConfigFolder);
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader)) {
                setting = serializer.Deserialize<T>(jsonReader);
                setting.Init();
            }
            if (File.Exists(userPath)) {
                using (StreamReader streamReader = File.OpenText(userPath))
                using (JsonReader jsonReader = new JsonTextReader(streamReader)) {
                    try {
                        T userSettings = serializer.Deserialize<T>(jsonReader);
                        setting.OverrideSettings(userSettings);
                    } catch {
                        try { File.Copy(userPath, UserConfigFolder + $"{Path.DirectorySeparatorChar}BROKEN_{fileName}", true); } catch {  }
                    }
                }
            }
            SaveSettings(fileName, setting);
        }

        public static void SaveSettings(string fileName, IUpdatableSettings setting) {
            Directory.CreateDirectory(UserConfigFolder);
            var userPath = $"{UserConfigFolder}{Path.DirectorySeparatorChar}{fileName}";

            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            using (StreamWriter streamWriter = new StreamWriter(userPath))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter)) {
                serializer.Serialize(jsonWriter, setting);
            }
        }
    }
}
