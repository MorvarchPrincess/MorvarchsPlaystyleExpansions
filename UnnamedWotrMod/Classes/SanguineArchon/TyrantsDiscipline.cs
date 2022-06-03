using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Utils;
using ExpandedContent;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.FactLogic;
using Templates = UnnamedWotrMod.Common.CreatedTemplates;
using CommonTemplates = UnnamedWotrMod.Common.CommonReferencedTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.Configurators.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Blueprints.Components;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints.Classes.Spells;

namespace UnnamedWotrMod.Classes.SanguineArchon
{
    class TyrantsDiscipline
    {
        public static void AddTyrantsDiscipline() 
        {
            var Rage = Resources.GetBlueprint<SimpleBlueprint>("2479395977cfeeb46b482bc3385f4647");
        }
    }



    class FuriousHuntress
    {
        public static void AddFuriousHuntress()
        {
            var Longbow = "7a1211c05ec2c46428f41e3c0db9423f";
            var Shortbow = "99ce02fb54639b5439d07c99c55b8542";

            var FuriousHuntress = FeatureConfigurator.New("Furious Huntress", Templates.FuriousHuntressFeature)
                .SetDisplayName(LocalizationTool.CreateString("FuriousHuntressName", "Furious Huntress", false))
                .SetDescription(LocalizationTool.CreateString("FuriousHuntressDescription", "", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddAttackStatReplacement(Kingmaker.EntitySystem.Stats.StatType.Strength, Kingmaker.Enums.WeaponSubCategory.None, true, new string[] { Longbow, Shortbow })
                .Configure();
        }
    }

    class Castling
    {
        public static void AddCastling()
        {
            Kingmaker.Utility.Feet fivefeet = new Kingmaker.Utility.Feet();
            fivefeet.m_Value = 5;

            var CastlingAbility = AbilityConfigurator.New("Castling", Templates.CastlingAbility)
                .SetDisplayName(LocalizationTool.CreateString("CastlingAbilityName", "Castling", false))
                .SetDescription(LocalizationTool.CreateString("CastlingAbilityDescription", "", false))
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift, true)
                .SetCustomRange(5)
                .SetAnimationStyle(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Special)
                .AddAbilityCustomDimensionDoorSwap("")
                .AddAbilityTargetIsAlly(true)
                .AddAbilityTargetRangeRestriction(fivefeet)
                .Configure();



            var CastlingFeature = FeatureConfigurator.New("Castling", Templates.CastlingFeature)
                .SetDisplayName(LocalizationTool.CreateString("CastlingAbilityName", "Castling", false))
                .SetDescription(LocalizationTool.CreateString("CastlingAbilityDescription", "", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddFacts(new string[] { Templates.CastlingAbility })
                .Configure();
        }
    }

    class ErinyesFury
    {
        public static void AddErinyesFury()
        {
            var ErinyesFuryFeature = FeatureConfigurator.New("Eriynes Fury", Templates.EriynesFuryFeature)
                .SetDisplayName(LocalizationTool.CreateString("EriynesFuryFeatureName", "Eriynes Fury", false))
                .SetDescription(LocalizationTool.CreateString("EriynesFuryFeatureDescription", "", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddFacts(new string[] { CommonTemplates.Rage })
                .Configure();
        }
    }

    class Kinslayer
    {
        public static void AddKinslayer()
        {
            var KinslayerFeature = FeatureConfigurator.New("Kinslayer", Templates.KinslayerFeature)
                .SetDisplayName(LocalizationTool.CreateString("KinslayerFeatureName", "Eriynes Fury", false))
                .SetDescription(LocalizationTool.CreateString("KinslayerFeatureDescription", "", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddFacts(new string[] { CommonTemplates.HumanFavoredEnemy })
                .Configure();
        }
    }

    class MercilessMassacre
    {
        public static void AddMercilessMassecre()
        {
            var MercilessMassacreContextValue = ContextRankConfigs.ClassLevel(new string[] { Templates.SanguineArchonClass }, false, Kingmaker.Enums.AbilityRankType.DamageBonus).WithBonusValueProgression(4);

            var FearSpellDescriptor = new SpellDescriptorWrapper();
            FearSpellDescriptor.m_IntValue = 4194352;

            var MercilessMassacreContext = new ContextValue()
            {

            };


            var MercilessMassacreFeature = FeatureConfigurator.New("MercilessMassacre", Templates.MercilessMassacreFeature)
                .SetDisplayName(LocalizationTool.CreateString("MercilessMassacreFeatureName", "Merciless Massacre", false))
                .SetDescription(LocalizationTool.CreateString("MercilessMassacreFeatureDescription", "", false))
                .SetIsClassFeature(true)
                .AddDamageBonusConditional(true, true, Kingmaker.Enums.ModifierDescriptor.Profane, MercilessMassacreContextValue, ConditionsBuilder.New().ContextConditionHasBuffWithDescriptor(FearSpellDescriptor))
                .Damage
               
        }
    }
}
