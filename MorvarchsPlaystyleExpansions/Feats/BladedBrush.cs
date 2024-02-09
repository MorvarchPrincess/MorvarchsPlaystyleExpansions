using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.Components.Replacements;
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
using Kingmaker.Blueprints.Items.Weapons;
using MorvarchsPlaystyleExpansions.Utils;

namespace MorvarchsPlaystyleExpansions.Feats
{
    public static class BladedBrush
    {
        private static readonly string FeatName = "BladedBrushFeat";
        private static readonly string FeatGuid = "e47a36ab-ebcc-4d94-9888-b795abd398f3";
        public static void Configure()
        {
            AttackStatReplacementFixed replacement = new AttackStatReplacementFixed(Kingmaker.EntitySystem.Stats.StatType.Dexterity, BlueprintUtils.GetBlueprint<BlueprintWeaponType>(Template.Glaive).ToReference<BlueprintWeaponTypeReference>());


            FeatureConfigurator.New("BladedBrushFeat", FeatGuid.ToLower())
                .SetDisplayName(LocalizationTool.CreateString("BladedBrushName", "Bladed Brush", false))
                .SetDescription(LocalizationTool.CreateString("BladedBrushDescription", "A worshipper of Shelyn can take this feat to use their dexterity on attack and damage with glaives.", false))
                .AddFeatureTagsComponent(FeatureTag.Attack)
                .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddPrerequisiteParametrizedWeaponFeature(Template.WeaponFocus, Kingmaker.Enums.WeaponCategory.Glaive)
                .AddRecommendationHasFeature(Template.Shelyn)
                .AddWeaponTypeDamageStatReplacement(Kingmaker.Enums.WeaponCategory.Glaive, false, Kingmaker.EntitySystem.Stats.StatType.Dexterity, false)
                .AddAttackStatReplacementFixed(replacement)
                .Configure();
        }
    }
}
