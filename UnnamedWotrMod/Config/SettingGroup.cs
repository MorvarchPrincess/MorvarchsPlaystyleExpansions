﻿using Kingmaker.Utility;
using System.Collections.Generic;
using System.Linq;

namespace ExpandedContent.Config {
    public class SettingGroup : IDisableableGroup {
        public bool DisableAll = false;
        public virtual bool GroupIsDisabled() => DisableAll;
        public virtual void SetGroupDisabled(bool value) => DisableAll = value;
        public SortedDictionary<string, SettingData> Settings = new SortedDictionary<string, SettingData>();
        public virtual bool this[string key] => IsEnabled(key);
        public bool IsExpanded = true;

        public void LoadSettingGroup(SettingGroup group, bool frozen) {
            DisableAll = group.DisableAll;
            if (frozen) {
                this.Settings.Keys.ToList().ForEach(key => {
                    Settings[key].Enabled = false;
                });
            }
            this.DisableAll = group.DisableAll;
            group.Settings.ForEach(entry => {
                if (Settings.ContainsKey(entry.Key)) {
                    Settings[entry.Key].Enabled = entry.Value.Enabled;
                }
            });
        }
        public virtual bool IsEnabled(string key) {
            if (!Settings.TryGetValue(key, out SettingData result)) {
                Main.LogDebug($"COULD NOT FIND SETTING KEY: {key}");
            }
            return result.Enabled && !GroupIsDisabled();
        }
        public virtual bool IsDisabled(string key) {
            return !IsEnabled(key);
        }

        public virtual void ChangeSetting(string key, bool value) {
            if (GroupIsDisabled()) {
                return;
            }
            Settings[key].Enabled = value;
        }

        ref bool ICollapseableGroup.IsExpanded() {
            return ref IsExpanded;
        }

        public void SetExpanded(bool value) {
            IsExpanded = value;
        }

        public class SettingData {
            public bool Enabled;
            public bool Homebrew;
            public string Description;

            public static implicit operator SettingData(bool enabled) {
                return new SettingData {
                    Enabled = enabled,
                    Description = string.Empty
                };
            }
        }
    }
}
