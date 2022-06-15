using BlueprintCore.Blueprints.Configurators.Classes;
using Template = MorvarchsPlaystyleExpansions.Common.CreatedTemplates;
using CommonTemplates = MorvarchsPlaystyleExpansions.Common.CommonReferencedTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Utils;
using MorvarchsPlaystyleExpansions.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints.Classes.Selection;
using BlueprintCore.Utils.Types;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Blueprints.Classes.Spells;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints.Root;
using BlueprintCore.Blueprints.Configurators.Root;
using Kingmaker;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;

namespace MorvarchsPlaystyleExpansions.Classes
{
    class HellknightShadow
    {
        public static void Configure()
        {
            Main.Log("Load main Shadow BPs");

            BlueprintParametrizedFeature WeaponFocus = BlueprintUtils.GetBlueprint<BlueprintParametrizedFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e");

            BlueprintFeatureSelection CombatTrick = BlueprintUtils.GetBlueprint<BlueprintFeatureSelection>("c5158a6622d0b694a99efb1d0025d2c1");     
            BlueprintFeatureSelection RangerStyle2 = BlueprintUtils.GetBlueprint<BlueprintFeatureSelection>("c6d0da9124735a44f93ac31df803b9a9");
            BlueprintFeatureSelection RangerStyle6 = BlueprintUtils.GetBlueprint<BlueprintFeatureSelection>("61f82ba786fe05643beb3cd3910233a8");
            BlueprintFeatureSelection RangerStyle10 = BlueprintUtils.GetBlueprint<BlueprintFeatureSelection>("78177315fc63b474ea3cbb8df38fafcd");

            BlueprintFeature SmiteChaos = BlueprintUtils.GetBlueprint<BlueprintFeature>("67d08ed4892de3441a43aae91a7dd7c9");
            BlueprintFeature FastStealth = BlueprintUtils.GetBlueprint<BlueprintFeature>("97a6aa2b64dd21a4fac67658a91067d7");
            BlueprintFeature CannyObserver = BlueprintUtils.GetBlueprint<BlueprintFeature>("68a23a419b330de45b4c3789649b5b41");
            BlueprintFeature IntimidatingProwess = BlueprintUtils.GetBlueprint<BlueprintFeature>("d76497bfc48516e45a0831628f767a0f");
            BlueprintFeature DispellingAttack = BlueprintUtils.GetBlueprint<BlueprintFeature>("1b92146b8a9830d4bb97ab694335fa7c");
            BlueprintFeature Opportunist = BlueprintUtils.GetBlueprint<BlueprintFeature>("5bb6dc5ce00550441880a6ff8ad4c968");

            Main.Log("Create Shadow class features");

            Main.Log("ShadowExpertise 1");

            BlueprintFeatureSelection ShadowExpertise1 = FeatureSelectionConfigurator.New("ShadowExpertise1", Template.ShadowExpertise1)
                .SetDisplayName(LocalizationTool.CreateString("ShadowExpertiseName", "Shadow Expertise", false))
                .SetDescription(LocalizationTool.CreateString("ShadowExpertiseDescription", "A Hellknight Shadow is trained on the common situations they will be expected to run into when deployed to their squadron," +
                " every two levels starting at level 3, a Hellknight Shadow can pick one Vangaurd Expertise to add to their repetoire. A Shadow cannot select an individual talent more than once.", false))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddToAllFeatures(
                    WeaponFocus.ToReference<BlueprintFeatureReference>(),
                    CombatTrick.ToReference<BlueprintFeatureReference>(),
                    RangerStyle2.ToReference<BlueprintFeatureReference>(),
                    SmiteChaos.ToReference<BlueprintFeatureReference>(),
                    FastStealth.ToReference<BlueprintFeatureReference>(),
                    CannyObserver.ToReference<BlueprintFeatureReference>(),
                    IntimidatingProwess.ToReference<BlueprintFeatureReference>()
                )
                .Configure();

            BlueprintFeatureSelection ShadowExpertise5 = FeatureSelectionConfigurator.New("ShadowExpertise5", Template.ShadowExpertise5)
                .SetDisplayName(LocalizationTool.GetString("ShadowExpertiseName"))
                .SetDescription(LocalizationTool.GetString("ShadowExpertiseDescription"))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddToAllFeatures(
                    WeaponFocus.ToReference<BlueprintFeatureReference>(),
                    CombatTrick.ToReference<BlueprintFeatureReference>(),
                    RangerStyle2.ToReference<BlueprintFeatureReference>(),
                    RangerStyle6.ToReference<BlueprintFeatureReference>(),
                    SmiteChaos.ToReference<BlueprintFeatureReference>(),
                    FastStealth.ToReference<BlueprintFeatureReference>(),
                    CannyObserver.ToReference<BlueprintFeatureReference>(),
                    IntimidatingProwess.ToReference<BlueprintFeatureReference>()
                )
                .Configure();

            Main.Log("ShadowExpertise 9");

            BlueprintFeatureSelection ShadowExpertise9 = FeatureSelectionConfigurator.New("ShadowExpertise9", Template.ShadowExpertise9)
                .SetDisplayName(LocalizationTool.GetString("ShadowExpertiseName"))
                .SetDescription(LocalizationTool.GetString("ShadowExpertiseDescription"))
                .SetIsClassFeature(true)
                .SetRanks(1)
                .AddToAllFeatures(
                    WeaponFocus.ToReference<BlueprintFeatureReference>(),
                    CombatTrick.ToReference<BlueprintFeatureReference>(),
                    RangerStyle2.ToReference<BlueprintFeatureReference>(),
                    RangerStyle6.ToReference<BlueprintFeatureReference>(),
                    RangerStyle10.ToReference<BlueprintFeatureReference>(),
                    SmiteChaos.ToReference<BlueprintFeatureReference>(),
                    FastStealth.ToReference<BlueprintFeatureReference>(),
                    CannyObserver.ToReference<BlueprintFeatureReference>(),
                    IntimidatingProwess.ToReference<BlueprintFeatureReference>()
                )
                .Configure();

            Main.Log("Hellknights Dread");
            var DreadContextRankConfig = ContextRankConfigs.ClassLevel(new string[] { Template.HellknightShadow }, false, Kingmaker.Enums.AbilityRankType.Default, 1, 10);

            var DreadContextValue = new ContextValue()
            {
                ValueRank = Kingmaker.Enums.AbilityRankType.Default,
                ValueType = ContextValueType.Rank,
                ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage
            };

            BlueprintFeature HellknightsDread = FeatureConfigurator.New("HellknightsDread", Template.HellknightsDread)
                .SetDisplayName(LocalizationTool.CreateString("HellknightsDreadName", "Hellknights Dread", false))
                .SetDescription(LocalizationTool.CreateString("HellknightsDreadDescription", "Hellknights have a dreaded reputation throughout most of Golarion, and none more so than the Hellknight Shadow. They use this reputation to great effect, terrifying" +
                "near all they come across. This gives the Hellknight Shadow a bonus to persuasion checks to intimidate equal to the Shadow's Class Level."))
                .AddContextStatBonus(StatType.CheckIntimidate, DreadContextValue, Kingmaker.Enums.ModifierDescriptor.Insight, 0, 1)
                .AddContextRankConfig(DreadContextRankConfig)
                .SetIsClassFeature(true)
                .SetRanks(1)
                .Configure();


            Main.Log("Load some more BPs");
            BlueprintFeature SneakAttack = BlueprintUtils.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87");
            BlueprintFeature HellknightOrder = BlueprintUtils.GetBlueprint<BlueprintFeature>("e31d849e4eb2578458419c9df622270f");


            Main.Log("Shadow Execution");
            var ShadowExecutionContextRankConfig = ContextRankConfigs.FeatureRank("e31d849e4eb2578458419c9df622270f", false, Kingmaker.Enums.AbilityRankType.StatBonus, 3, 3);

            var FearSpellDescriptor = new SpellDescriptorWrapper();
            FearSpellDescriptor.m_IntValue = 4194352;

            var ShadowExecutionContext = new ContextValue()
            {
                ValueRank = Kingmaker.Enums.AbilityRankType.StatBonus,
                ValueType = ContextValueType.Rank,
            };

            BlueprintFeature ShadowExecution = FeatureConfigurator.New("ShadowExecution", Template.ShadowExecution)
                .SetDisplayName(LocalizationTool.CreateString("ShadowExecutionName", "Shadow Execution", false))
                .SetDescription(LocalizationTool.CreateString("ShadowExecutionDescription", "A Hellknight Shadow's pinnacle technique is learning to deftly slide into the gaps left by opponents not in their right mind, taking advantage " +
                "of a scared or otherwise mentally imparied opponent to strike true. This gives the Hellknight Shadow a +3 to attack against any opponent affected by a fear or mind affecting effect.", false))
                .AddAttackBonusConditional(ShadowExecutionContext, false, ConditionsBuilder.New().HasBuffWithDescriptor(false, FearSpellDescriptor), Kingmaker.Enums.ModifierDescriptor.Insight)
                .AddContextRankConfig(ShadowExecutionContextRankConfig)
                .SetIsClassFeature(true)
                .SetRanks(1)
                .Configure();

            Main.Log("Create the Shadow's Progression");
            BlueprintProgression ShadowProgression = ProgressionConfigurator.New("HellknightShadowProgression", Template.HellknightShadowProgression)
                .SetDisplayName(LocalizationTool.CreateString("HellknightShadowProgression", "Hellknight Shadow Progression", false))
                .AddToLevelEntries(new LevelEntry[]
                {
                    LevelEntryUtils.CreateLevelEntry(1,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) HellknightOrder,
                        (BlueprintFeatureBase) HellknightsDread
                    }),
                    LevelEntryUtils.CreateLevelEntry(2,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                    }),
                    LevelEntryUtils.CreateLevelEntry(3,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) ShadowExpertise1
                    }),
                    LevelEntryUtils.CreateLevelEntry(4,new BlueprintFeatureBase[]
                    {
                       (BlueprintFeatureBase) SneakAttack
                    }),
                    LevelEntryUtils.CreateLevelEntry(5,new BlueprintFeatureBase[]
                    {
                       (BlueprintFeatureBase) ShadowExpertise5
                    }),
                    LevelEntryUtils.CreateLevelEntry(6,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                    }),
                    LevelEntryUtils.CreateLevelEntry(7,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) ShadowExpertise5
                    }),
                    LevelEntryUtils.CreateLevelEntry(8,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                    }),
                    LevelEntryUtils.CreateLevelEntry(9,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) ShadowExpertise9
                    }),
                    LevelEntryUtils.CreateLevelEntry(10,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                        (BlueprintFeatureBase) ShadowExecution
                    }),
                })
                .SetIsClassFeature(true)
                .SetRanks(1)
                .SetClasses(new BlueprintProgression.ClassWithLevel[]
                {
                    new BlueprintProgression.ClassWithLevel()
                    {
                        m_Class = BlueprintUtils.CreateReference<BlueprintCharacterClassReference>(Template.HellknightShadow),
                        AdditionalLevel = 0,
                        
                    }
                })
                .Configure();

            Main.Log("Create the class itself");

            // Create the Class!
            BlueprintCharacterClass Shadow = CharacterClassConfigurator.New("Hellknight Shadow", Template.HellknightShadow)
                .SetLocalizedName(LocalizationTool.CreateString("HellknightShadowName", "Hellknight Shadow", false))
                .SetLocalizedDescriptionShort(LocalizationTool.CreateString("HellknightShadowShortDesc", "", false))
                .SetLocalizedDescription(LocalizationTool.CreateString("HellknightShadowDescription", "Hellknight Shadows are the group called upon when the Hellknights need a defter touch. Specialized in subterfuge, scouting and very occasionally assassination, " +
                "the Hellknight Shadows are a terrifying rumor in the ears of enemies of the Hellknights.", false))
                .SetLocalizedDescriptionShort(LocalizationTool.CreateString("HellknightShadowShortDescription","The Hellknight Shadow is a rogue focused Hellknight prestige class",false))
                .AddPrerequisiteStatValue(StatType.SkillPerception, 5)
                .AddPrerequisiteStatValue(StatType.SkillThievery, 5)
                .AddPrerequisiteStatValue(StatType.SkillMobility, 5)
                .AddPrerequisiteFullStatValue(false, Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.All, false, StatType.SneakAttack, 2)
                .AddPrerequisiteAlignment(Kingmaker.UnitLogic.Alignments.AlignmentMaskType.Lawful)
                .AddPrerequisiteIsPet(false, Kingmaker.Blueprints.Classes.Prerequisites.Prerequisite.GroupType.All, true, true)
                .SetBaseAttackBonus(BlueprintUtils.GetBlueprint<BlueprintStatProgression>("4c936de4249b61e419a3fb775b9f2581").ToReference<BlueprintStatProgressionReference>())
                .SetFortitudeSave(BlueprintUtils.GetBlueprint<BlueprintStatProgression>("1f309006cd2855e4e91a6c3707f3f700").ToReference<BlueprintStatProgressionReference>())
                .SetWillSave(BlueprintUtils.GetBlueprint<BlueprintStatProgression>("dc5257e1100ad0d48b8f3b9798421c72").ToReference<BlueprintStatProgressionReference>())
                .SetReflexSave(BlueprintUtils.GetBlueprint<BlueprintStatProgression>("1f309006cd2855e4e91a6c3707f3f700").ToReference<BlueprintStatProgressionReference>())
                .SetHitDie(Kingmaker.RuleSystem.DiceType.D8)
                .SetPrestigeClass(true)
                .SetSkillPoints(4)
                .AddToClassSkills(new StatType[]
                {
                    StatType.SkillMobility,
                    StatType.SkillThievery,
                    StatType.SkillStealth,
                    StatType.SkillPerception,
                    StatType.SkillPersuasion,
                })
                .SetProgression(ShadowProgression.ToReference<BlueprintProgressionReference>())
                .Configure();

            Main.Log("Shadow successfully created!");

            BlueprintCharacterClassReference ShadowReference = Shadow.ToReference<BlueprintCharacterClassReference>();

            Main.Log("Add Shadow to the Root.");

            BlueprintRoot Root = Game.Instance.BlueprintRoot;
            Root.Progression.m_CharacterClasses = Root.Progression.m_CharacterClasses.Append(ShadowReference).ToArray();

            Main.Log("Fixing Orders for the Shadow!");

            DivineSignifier.FixHellknightOrders(ShadowReference);

            Main.Log("Domains Fixed!");
        }
    }
}
