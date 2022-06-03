using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Template = UnnamedWotrMod.Common.CommonReferencedTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnnamedWotrMod.Feats
{
    public static class BladedBrush
    {
        private static readonly string FeatName = "BladedBrushFeat";
        private static readonly string FeatGuid = "e47a36ab-ebcc-4d94-9888-b795abd398f3";
        private static readonly string DisplayNameKey = "BladedBrushName";
        private static readonly string DisplayName = "Bladed Brush";
        private static readonly string Description = "Its bladed brush, duh.";
        private static readonly string DescriptionKey = "BladedBrushDescription";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, FeatGuid.ToLower())
                .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description, false))
                .SetFeatureTags(FeatureTag.Attack, FeatureTag.Damage, FeatureTag.Melee)
                .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .PrerequisiteFeature(Template.Shelyn)
                .PrerequisiteParameterizedWeaponFeature(Template.WeaponFocus, Kingmaker.Enums.WeaponCategory.Glaive)
                .AddRecommendationStatComparison(Kingmaker.EntitySystem.Stats.StatType.Dexterity, Kingmaker.EntitySystem.Stats.StatType.Strength, 4)
                .AddRecommendationHasFeature(Template.Shelyn)
                .AddWeaponTypeDamageStatReplacement(Kingmaker.EntitySystem.Stats.StatType.Dexterity, Kingmaker.Enums.WeaponCategory.Glaive, false, false)
                .AddAttackStatReplacement(Kingmaker.EntitySystem.Stats.StatType.Dexterity, Kingmaker.Enums.WeaponSubCategory.None, true, new string[] { Template.Glaive })
                .Configure();

            FeatureSelectionConfigurator.For(Template.BasicFeatSelectionGuid).AddToFeatures(FeatName).Configure(); ;
        }
    }
}
