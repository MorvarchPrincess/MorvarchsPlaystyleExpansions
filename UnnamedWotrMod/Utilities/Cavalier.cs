﻿using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;


namespace ExpandedContent.Utilities {
    static class Cavalier {
        public static void AddCavalierFeatures() {
            var MountedBuff = Resources.GetBlueprint<BlueprintBuff>("b2d13e8f3bb0f1d4c891d71b4d983cf7");
            var AnimalCompanionFeatureWolf = Resources.GetBlueprint<BlueprintFeature>("67a9dc42b15d0954ca4689b13e8dedea");



            var CavalierMountFeatureWolf = Helpers.CreateBlueprint<BlueprintFeature>("CavalierMountFeatureWolf", bp => {
                bp.m_DisplayName = AnimalCompanionFeatureWolf.m_DisplayName;
                bp.m_Description = AnimalCompanionFeatureWolf.m_Description;
                bp.m_Icon = AnimalCompanionFeatureWolf.Icon;
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = AnimalCompanionFeatureWolf.Groups;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = AnimalCompanionFeatureWolf.GetComponent<AddPet>().m_Pet;
                    c.m_LevelRank = AnimalCompanionFeatureWolf.GetComponent<AddPet>().m_LevelRank;
                    c.m_LevelContextValue = new ContextValue();
                    c.m_UpgradeFeature = AnimalCompanionFeatureWolf.GetComponent<AddPet>().m_UpgradeFeature;
                    c.UpgradeLevel = AnimalCompanionFeatureWolf.GetComponent<AddPet>().UpgradeLevel;
                });
                bp.AddPrerequisite<PrerequisitePet>(c => {
                    c.NoCompanion = true;
                });
                bp.AddPrerequisite<PrerequisiteSize>(c => {
                    c.Size = Size.Small;
                });
            });
        }
    }
}
