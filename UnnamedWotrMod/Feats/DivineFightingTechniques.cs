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
    public static class DivineFightingTechniques
    {
        public static void Configure()
        {
            ConfigureShootingStar();
        }

        public static void ConfigureShootingStar()
        {
            string DisplayName = "Way of the Shooting Star";
            string DisplayNameKey = "ShootingStarName";
            string FeatName = "ShootingStarFeat";
            string FeatGuid = "3643D358-1387-461F-A0E5-14954AF42828";
            string Description = "Utilizing the fighting techniques channeled through Desna, add your charisma to attack and damage with starknives instead of dexterity or strength.";
            string DescriptionKey = "ShootingStarhDescription";

            FeatureConfigurator.New(FeatName, FeatGuid.ToLower())
                .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description, false))
                .SetFeatureTags(FeatureTag.Attack, FeatureTag.Damage, FeatureTag.Melee)
                .SetFeatureGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                .AddPrerequisiteAlignment(new Kingmaker.UnitLogic.Alignments.AlignmentMaskType[] { Kingmaker.UnitLogic.Alignments.AlignmentMaskType.ChaoticGood })
                .PrerequisiteParameterizedWeaponFeature(Template.WeaponFocus, Kingmaker.Enums.WeaponCategory.Starknife)
                .AddRecommendationStatComparison(Kingmaker.EntitySystem.Stats.StatType.Charisma, Kingmaker.EntitySystem.Stats.StatType.Dexterity, 4)
                .AddRecommendationHasFeature(Template.Desna)
                .AddWeaponTypeDamageStatReplacement(Kingmaker.EntitySystem.Stats.StatType.Charisma, Kingmaker.Enums.WeaponCategory.Starknife, false, false)
                .AddAttackStatReplacement(Kingmaker.EntitySystem.Stats.StatType.Charisma, Kingmaker.Enums.WeaponSubCategory.None, true, new string[] { Template.Starknife })
                .Configure();

            FeatureSelectionConfigurator.For(Template.BasicFeatSelectionGuid).AddToFeatures(FeatName).Configure();
        }
    }
}
