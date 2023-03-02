using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Utils;
using ExpandedContent;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates = MorvarchsPlaystyleExpansions.Common.CreatedTemplates;
using CommonTemplates = MorvarchsPlaystyleExpansions.Common.CommonReferencedTemplates;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;

namespace UnnamedWotrMod.Classes.SanguineArchon
{
    internal class HardenedHeart
    {
        public static void AddHardenedHeart()
        {
            var HardenedHeart = FeatureConfigurator.New("HardenedHeart", Templates.HardenedHeart)
                .SetDisplayName(LocalizationTool.CreateString("HardenedHeartFeatureName", "Hardened Heart", false))
                .SetDescription(LocalizationTool.CreateString("HardenedHeartFeatureDescription", "", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddFacts(new(){ CommonTemplates.Bravery })
                .Configure();
        }
    }
}
