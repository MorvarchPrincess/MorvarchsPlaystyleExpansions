using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Template = MorvarchsPlaystyleExpansions.Common.CommonReferencedTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;

namespace MorvarchsPlaystyleExpansions.Feats
{
    public static class BladedBrush
    {
        private static readonly string FeatName = "BladedBrushFeat";
        private static readonly string FeatGuid = "e47a36ab-ebcc-4d94-9888-b795abd398f3";
        public static void Configure()
        {
            FeatureConfigurator.New("BladedBrushFeat", FeatGuid.ToLower())
                .SetDisplayName(LocalizationTool.CreateString("BladedBrushName", "Bladed Brush", false))
                .SetDescription(LocalizationTool.CreateString("BladedBrushDescription", "A worshipper of Shelyn can take this feat ao use their dexterity on attack and damage with glaives.", false))
                .AddFeatureTagsComponent(FeatureTag.Attack)
                .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddPrerequisiteParametrizedWeaponFeature(Template.WeaponFocus, Kingmaker.Enums.WeaponCategory.Glaive)
                .AddRecommendationStatComparison(4,Kingmaker.EntitySystem.Stats.StatType.Dexterity, Kingmaker.EntitySystem.Stats.StatType.Strength)
                .AddRecommendationHasFeature(Template.Shelyn)
                .AddWeaponTypeDamageStatReplacement(Kingmaker.Enums.WeaponCategory.Glaive, false, Kingmaker.EntitySystem.Stats.StatType.Dexterity, false)
                .AddAttackStatReplacement(true,null, BlueprintCore.Blueprints.CustomConfigurators.ComponentMerge.Fail,Kingmaker.EntitySystem.Stats.StatType.Dexterity,Kingmaker.Enums.WeaponSubCategory.None, new List<Blueprint<BlueprintWeaponTypeReference>> {  Template.Glaive })
                .Configure();

            FeatureSelectionConfigurator.For(Template.BasicFeatSelectionGuid).AddToAllFeatures(FeatName).Configure(); ;
        }
    }
}
