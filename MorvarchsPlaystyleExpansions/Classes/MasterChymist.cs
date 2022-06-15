using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template = MorvarchsPlaystyleExpansions.Common.CreatedTemplates;
using CommonTemplates = MorvarchsPlaystyleExpansions.Common.CommonReferencedTemplates;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.Configurators.Classes;
using MorvarchsPlaystyleExpansions.Utils;
using Kingmaker.Blueprints;
using BlueprintCore.Utils;

namespace MorvarchsPlaystyleExpansions.Classes
{
    class MasterChymist
    {
        public static void Configure()
        {
            /*Main.Log("Starting to load Master Chymist");

            Main.Log("Create the Vanguard's Progression");
            BlueprintProgression VanguardProgression = ProgressionConfigurator.New("HellknightVanguardProgression", Template.HellknightVanguardProgression)
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
                        (BlueprintFeatureBase) VanguardExpertise1
                    }),
                    LevelEntryUtils.CreateLevelEntry(4,new BlueprintFeatureBase[]
                    {
                       (BlueprintFeatureBase) SneakAttack
                    }),
                    LevelEntryUtils.CreateLevelEntry(5,new BlueprintFeatureBase[]
                    {
                       (BlueprintFeatureBase) VanguardExpertise5
                    }),
                    LevelEntryUtils.CreateLevelEntry(6,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                    }),
                    LevelEntryUtils.CreateLevelEntry(7,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) VanguardExpertise5
                    }),
                    LevelEntryUtils.CreateLevelEntry(8,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                    }),
                    LevelEntryUtils.CreateLevelEntry(9,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) VanguardExpertise9
                    }),
                    LevelEntryUtils.CreateLevelEntry(10,new BlueprintFeatureBase[]
                    {
                        (BlueprintFeatureBase) SneakAttack,
                        (BlueprintFeatureBase) VanguardExecution
                    }),
                })
                .SetIsClassFeature(true)
                .SetRanks(1)
                .SetClasses(new BlueprintProgression.ClassWithLevel[]
                {
                    new BlueprintProgression.ClassWithLevel()
                    {
                        m_Class = BlueprintUtils.CreateReference<BlueprintCharacterClassReference>(Template.HellknightVanguard),
                        AdditionalLevel = 0,
                    }
                })
                .Configure();

            Main.Log("Create the class itself");

            // Create the Class!
            BlueprintCharacterClass Chymist = CharacterClassConfigurator.New("Master Chymist", Template.MasterChymist)
                .SetLocalizedName(LocalizationTool.CreateString("MasterChymistName", "Master Chymist", false))
                .SetLocalizedDescription(LocalizationTool.CreateString("MasterChymistDescription", "Hellknight Vanguards are the group called upon when the Hellknights need a defter touch. Specialized in subterfuge, scouting and very occasionally assassination, " +
                "the Hellknight Vanguards are a terrifying rumor in the ears of enemies of the Hellknights.", false))
                .SetLocalizedDescriptionShort(LocalizationTool.CreateString("HellknightVanguardShortDescription", "The Hellknight Vanguard is a rogue focused Hellknight prestige class", false))
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
                .SetProgression(VanguardProgression.ToReference<BlueprintProgressionReference>())
                .Configure();\
            */
        }
    }
}
