using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;
using Resources = ExpandedContent.Resources;
using UnityEngine;
using ExpandedContent.Utilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace UnnamedWotrMod.Classes.SanguineArchon
{
    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    public class SanguineArchonClassAdder
    {
        private static bool Initialized;

        public static void Postfix()
        {
            if (SanguineArchonClassAdder.Initialized) return;
            SanguineArchonClassAdder.Initialized = true;
            // TODO: Class setting turnoff

        }

        public static void AddSanguineArchonClass()
        {
            var test = Resources.GetBlueprint<BlueprintFeature>("");

            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var SavesPrestigeHigh = Resources.GetBlueprint<SimpleBlueprint>("1f309006cd2855e4e91a6c3707f3f700");
            var SavesPrestigeLow = Resources.GetBlueprint<SimpleBlueprint>("dc5257e1100ad0d48b8f3b9798421c72");
            var FullBab = Resources.GetBlueprint<SimpleBlueprint>("b3057560ffff3514299e8b93e7648a9d");

            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");

            string ClassName = "Sanguine Archon";
            string ClassNameKey = "SanguineArchonName";

            var SanguineArchonClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("SanguineArchonClass", bp =>
            {
                // Name and Description
                bp.LocalizedName = LocalizationTool.CreateString(ClassNameKey,ClassName,false);
                bp.LocalizedDescription = LocalizationTool.CreateString("", "", false);
                bp.LocalizedDescriptionShort = LocalizationTool.CreateString("", "", false);

                // Basic class stats
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D10;
                bp.m_BaseAttackBonus = FullBab.ToReference<BlueprintStatProgressionReference>();
                bp.m_FortitudeSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.m_ReflexSave = SavesPrestigeLow.ToReference<BlueprintStatProgressionReference>();
                bp.m_WillSave = SavesPrestigeHigh.ToReference<BlueprintStatProgressionReference>();
                bp.PrestigeClass = true;
                bp.m_Difficulty = 1;
                bp.SkillPoints = 2;
                bp.ClassSkills = new Kingmaker.EntitySystem.Stats.StatType[]
                {
                    Kingmaker.EntitySystem.Stats.StatType.SkillPersuasion,
                    Kingmaker.EntitySystem.Stats.StatType.SkillAthletics,
                    Kingmaker.EntitySystem.Stats.StatType.SkillKnowledgeWorld,
                    Kingmaker.EntitySystem.Stats.StatType.SkillPerception
                };
                bp.AddComponent<PrerequisiteNoClassLevel>(c =>
                {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
            });
            Helpers.RegisterClass(SanguineArchonClass);
        }

        public static void AddSanguineArchonProgression()
        {
            var SanguineArchonProgression = Helpers.CreateBlueprint<BlueprintProgression>("SanguineArchonProgression");
            SanguineArchonProgression.SetName("Sanguine Archon");
            SanguineArchonProgression.SetDescription("");
            SanguineArchonProgression.LevelEntries = new LevelEntry[]
            {
                Helpers.LevelEntry(1),
            };
        }
    }
}
