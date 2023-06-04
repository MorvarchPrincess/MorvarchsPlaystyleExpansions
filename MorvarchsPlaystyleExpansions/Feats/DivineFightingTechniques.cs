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
    public static class DivineFightingTechniques
    {
        public static void Configure()
        {
            ConfigureShootingStar();
        }

        public static void ConfigureShootingStar()
        {
            string FeatName = "ShootingStarFeat";

            AttackStatReplacementFixed replacement = new AttackStatReplacementFixed(Kingmaker.EntitySystem.Stats.StatType.Charisma, BlueprintUtils.GetBlueprint<BlueprintWeaponType>(Template.Starknife).ToReference<BlueprintWeaponTypeReference>());

            FeatureConfigurator.New(FeatName, "3643D358-1387-461F-A0E5-14954AF42828")
                .SetDisplayName(LocalizationTool.CreateString("ShootingStarName", "Way of the Shooting Star", false))
                .SetDescription(LocalizationTool.CreateString("ShootingStarhDescription", "Utilizing the fighting techniques channeled through Desna, add your charisma to attack and damage with starknives instead of dexterity or strength.", false))
                .AddFeatureTagsComponent(FeatureTag.Attack)
                .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddPrerequisiteParametrizedWeaponFeature(Template.WeaponFocus, Kingmaker.Enums.WeaponCategory.Starknife)
                .AddRecommendationHasFeature(Template.Desna)
                .AddWeaponTypeDamageStatReplacement(Kingmaker.Enums.WeaponCategory.Starknife, false, Kingmaker.EntitySystem.Stats.StatType.Charisma, false)
                .AddAttackStatReplacementFixed(replacement)
                .Configure();
        }
    }
}
