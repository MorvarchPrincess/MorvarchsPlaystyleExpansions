using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorvarchsPlaystyleExpansions.Utils
{
    class LevelEntryUtils
    {
        public static LevelEntry CreateLevelEntry(int level, params BlueprintFeatureBase[] features)
        {
            return CreateLevelEntry(level, features.Select(f => f.ToReference<BlueprintFeatureBaseReference>()).ToArray());
        }
        public static LevelEntry CreateLevelEntry(int level, params BlueprintFeatureBaseReference[] features)
        {
            LevelEntry levelEntry = new LevelEntry()
            {
                Level = level,
                m_Features = features.ToList()
            };
            return levelEntry;
        }
    }
}
