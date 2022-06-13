using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorvarchsPlaystyleExpansions.Utils
{
    class ContextConfigUtils
    {
        public static void AddClassToConfigs(BlueprintComponent[] components, BlueprintCharacterClassReference charclass)
        {
            foreach (BlueprintComponent component in components)
            {
                if (component is ContextRankConfig)
                {
                    AddClassToConfig((ContextRankConfig) component, charclass);
                }
            }
        }

        public static void AddClassToConfig(ContextRankConfig component, BlueprintCharacterClassReference charclass)
        {
            if (component.m_Class != null && component.m_Class.Length > 0)
            {
                component.m_Class.Append(charclass);
            }
        }
    }
}
