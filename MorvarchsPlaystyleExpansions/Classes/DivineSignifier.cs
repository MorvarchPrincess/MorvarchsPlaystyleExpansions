using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Template = MorvarchsPlaystyleExpansions.Common.CreatedTemplates;
using CommonTemplates = MorvarchsPlaystyleExpansions.Common.CommonReferencedTemplates;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Classes;
using MorvarchsPlaystyleExpansions.Utils;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Designers.Mechanics.Facts;
using TabletopTweaks.Core.Utilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;

namespace MorvarchsPlaystyleExpansions.Classes
{
    class DivineSignifier
    {
        public static void Configure()
        {
            ModifyChannelEnergy();

            ModifyDomains();

            var Signifier = BlueprintUtils.GetBlueprint<BlueprintCharacterClass>(CommonTemplates.Signifier).ToReference<BlueprintCharacterClassReference>();
            FixHellknightOrders(Signifier);

            BlueprintFeature Catechesis = FeatureConfigurator.New("Catechesis", Template.Catechesis)
                .SetDisplayName(LocalizationTool.CreateString("CatechesisName", "Catechesis", false))
                .SetDescription(LocalizationTool.CreateString("CatechesisDescription", "A divine signifier's level stacks with other divine spellcasting classes for the purposes of determining the effects of those classes domain powers, inquisitions, mysteries and channel energy.", false))
                .AddClassLevelsForPrerequisites(CommonTemplates.Signifier, CommonTemplates.Oracle, 1.0, 0)
                .SetIsClassFeature(true)
                .SetRanks(1)
                .Configure();

            BlueprintProgression SignifierProgression = BlueprintUtils.GetBlueprint<BlueprintProgression>("9dbe8102673d3e94e98e25cb9260e1c5");
            SignifierProgression.LevelEntries[0].m_Features.Add(Catechesis.ToReference<BlueprintFeatureBaseReference>());
        }

        public static void ModifyChannelEnergy()
        {
            var ChannelsList = new List<BlueprintAbility>() {
                BlueprintUtils.GetBlueprint<BlueprintAbility>("f5fc9a1a2a3c1a946a31b320d1dd31b2"), // Cleric Channel Energy
                BlueprintUtils.GetBlueprint<BlueprintAbility>("279447a6bf2d3544d93a0a39c3b8e91d"), // Cleric channel positive damage
                BlueprintUtils.GetBlueprint<BlueprintAbility>("89df18039ef22174b81052e2e419c728"), // Cleric channel negative
                BlueprintUtils.GetBlueprint<BlueprintAbility>("9be3aa47a13d5654cbcb8dbd40c325f2"), // Cleric channel negative heal
                BlueprintUtils.GetBlueprint<BlueprintAbility>("b9eca127dd82f554fb2ccd804de86cf6"), // Oracle channel
                BlueprintUtils.GetBlueprint<BlueprintAbility>("ab0635df6b4674e4e96809bd718cab89"), // Oracle channel damage
            };

            foreach (BlueprintAbility channeltype in ChannelsList)
            {
                ContextRankConfig ChannelScaling = (ContextRankConfig)Array.Find(channeltype.ComponentsArray, component => component is ContextRankConfig);
                ChannelScaling.m_Class = ChannelScaling.m_Class.Append(BlueprintUtils.GetBlueprint<BlueprintCharacterClass>(CommonTemplates.Signifier).ToReference<BlueprintCharacterClassReference>()).ToArray();
                ChannelScaling.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
            }
        }

        public static void ModifyDomains()
        {
            Main.Log("Attempting to modify Domains");

            var DomainSelection = BlueprintUtils.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");

            var Signifier = BlueprintUtils.GetBlueprint<BlueprintCharacterClass>(CommonTemplates.Signifier).ToReference<BlueprintCharacterClassReference>();

            // Fix Abilities
            List<String> DomainsAbilityList = new List<String>()
            {
                "b3494639791901e4db3eda6117ad878f",
                "5d8c4161d21f63e4a99b47d1e99e654e", // Chaos Greater Ability
                "84cd24a110af59140b066bc2c69619bd", // Charm Domain Base
                "b1b8efd70ba5dd84aa6985d46dc299d5", // Community Domain Base
                "76291e62d2496ad41824044aba3077ea", // Community Domain Greater
                "39ed9d4b1e033e042aac4f9eb9c7315f", // Domain Darkness Base Ability
                "31acd268039966940872c916782ae018", // Darkness Domain Greater Ability, 2 Context Rank Configs!
                "979f63920af22344d81da5099c9ec32e", // Death Domain Base Ability
                "3ff40918d33219942929f0dbfe5d1dee", // Earth Domain Base Ability
                "5f2d11d9ae72aa740926d8b865d23cb0", // Evil Domain Base Ability
                "ab548995edf186f449937413ea463cd5", // Evil Domain Greater Ability
                "4ecdf240d81533f47a5279f5075296b9", // Fire Domain Base Ability
                "d018241b5a761414897ad6dc4df2db9f", // Glory Domain Base Ability
                "c89e92387e940e541b02c1969cd1fe2a", // Glory Domain Greater Ability
                "017afe6934e10c3489176e759a5f01b0", // Good Domain Base Ability
                "7fc3e743ba28fd64f977fb55b7536053", // Good Domain Greater Ability
                "18f734e40dd7966438ab32086c3574e1", // Healing Domain Base Ability
                "02a79a205bce6f5419dcdf26b64f13c6", // Knowledge Domain Base Ability
                "ec582b195ccb2ef4ea8dcd96a5a6e009", // Knowledge Domain Greater Ability
                "0b1615ec2dabc6f4294a254b709188a4", // Law Domain Greater Ability
                "0e0668a703fbfcf499d9aa9d918b71ea", // Luck Domain Greater Ability
                "c3e4ff89950f1d748be6f5958b1aa19c", // Madness Domain Base Attack Ability
                "c09446b861bac7b4b83877db863150d9", // Madness Domain Base Saves Ability
                "d92b2eac4dbf31f439e5bc9d2d467ff1", // Madness Domain Base Skills Ability
                "a3d470a27ec5e4540aeaf9723e9b8ae7", // Madness Domain Greater Ability
                "7a305ef528cb7884385867a2db410102", // Nobility Domain Base Ability
                "2972215a5367ae44b8ddfe435a127a6e", // Nobility Domain Greater Ability
                "c5815bd0bf87bdb4fa9c440c8088149b", // Protection Domain Base Ability              
                "92c821ecc8d73564bad15a8a07ed40f2", // Rune Domain Base Ability Acid
                "2b81ff42fcbe9434eaf00fb0a873f579", // Rune Domain Base Ability Cold
                "b67978e3d5a6c9247a393237bc660339", // Rune Domain Base Ability Electricity
                "eddfe26a8a3892b47add3cb08db7069d", // Rune Domain Base Ability Fire
                "9171a3ce8ea8cac44894b240709804ce", // Rune Domain Greater Ability
                "1d6364123e1f6a04c88313d83d3b70ee", // Strength Domain Base Ability
                "ee7eb5b9c644a0347b36eec653d3dfcb", // Trickery Domain Base Ability
                "fbef6b2053ab6634a82df06f76c260e3", // War Domain Base Ability
                "5e1db2ef80ff361448549beeb7785791", // Water Domain Base Ability
                "f166325c271dd29449ba9f98d11542d9", // Weather Domain Base Ability
            };

            foreach (string ability in DomainsAbilityList)
            {
                Main.Log("Fixing Ability - " + ability);
                BlueprintAbility DomainAbility = BlueprintUtils.GetBlueprint<BlueprintAbility>(ability);
                ContextConfigUtils.AddClassToConfigs(DomainAbility.ComponentsArray, Signifier);
            }

            Main.Log("Fixing Animal Companion Progression");
            BlueprintProgression AnimalDomainProgression = BlueprintUtils.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            AnimalDomainProgression.m_Classes = AnimalDomainProgression.m_Classes.Append(new BlueprintProgression.ClassWithLevel()
            {
                m_Class = Signifier,
                AdditionalLevel = 0
            }).ToArray();
            

            List<String> DomainsAbilityAreaEffects = new List<String>()
            {
                "f042f2d62e6785d4e8612a027de1f298", // Artifice Domain Ability Area Effect
                "98c3a36f2a3636c49a3f77c001a25f29", // Rune Domain Ability Area Effect Acid
                "8b8e98e8e0000f643ad97c744f3f850b", // Rune Domain Ability Area Effect Cold
                "db868c576c69d0e4a8462645267c6cdc", // Rune Domain Ability Area Effect Electricity
                "9b786945d2ec1884184235a488e5cb9e", // Rune Domain Ability Area Effect Fire
                "e26de8b0164db23458eb64c21fac2846", // Rune Domain Greater Ability Area Effect
                "cfe8c5683c759f047a56a4b5e77ac93f", // Sun Domain Greater Ability Area Effect
            };

            foreach (string areaeffect in DomainsAbilityAreaEffects)
            {
                Main.Log("Fixing Area Effect - " + areaeffect);
                BlueprintAbilityAreaEffect area = BlueprintUtils.GetBlueprint<BlueprintAbilityAreaEffect>(areaeffect);
                ContextConfigUtils.AddClassToConfigs(area.ComponentsArray, Signifier);
            }

            List<String> DomainAbilityResource = new List<String>()
            {
                "d2c3c7c7efbc71c438dc4e0c3f216407",
                "c55c470a0262f3f4993e3618e4fd5114", // Chaos Greater Resource
                "d49f0e3460fd52d4e9660a8ce52142a0", // Charm Greater Resource
                "55efb511a2290b94bb218e2d56a51f1f", // Darkness Domain Greater Resource
                "98f07eabe9cb4f34cb1127de625f4bee", // Destruction Domain Greater Resource
                "db334ce9b929481458819f1ffd7e930e", // Evil Domain Greater Resource
                "8d45a527ce4d3ec47853faaa972c2362", // Good Domain Greater Resource
                "f88f616a4b6bd5f419025115c52cb329", // Knowledge Domain Base Resource
                "34f0a288ff5106645a88440b800686ca", // Knowledge Domain Greater Resource
                "de7945c4cc6a0a24790941d7e2b85838", // Law Domain Greater Resource
                "8ddc7f532cf2b3b4c877497856cc5b97", // Liberation Domain Base Resource
                "d19e900012a69954c93f3b7533bc3911", // Liberation Domain Greater Resource
                "b209ca75fbea5144c9d73ecb29055a08", // Luck Domain Greater Resource
                "3289ee86c57f6134d81770865c315e8b", // Madness Domain Greater Resource
                "3aecc0c5d17390443b30774309145854", // Magic Domain Greater Resource
                "cb3efe82596c908418c0dba4ef6f4210", // Nobility Domain Greater Resource
                "881d696940ec99041aefafd5b2fda189", // Plant Domain Greater Resource
                "f3d878f77d0ee854b864f5ea1c80e752", // Protection Domain Greater Resource
                "aefe627a3a2f8d94ea9d2b3961261282", // Repose Domain Greater Resource
                "f179b35a846d87b45bf4322752bc6d17", // Rune Domain Greater Resource
                "6bea29e2257fa6742923ba757435aba8", // Sun Domain Greater Resource
                "52ee1ad8d1ac94d4b92a62acfa8931ad", // Travel Domain Base Resource
                "657bfb21544642e4f8aef532c9f04ac2", // Travel Domain Greater Resource
                "520ad6381e09f8349a237ac4b247082e", // Trickery Domain Greater Resource
                "5c88b557e79eaee41a4190712b178970", // Weather Domain Greater Ability
            };

            foreach (string abilityresource in DomainAbilityResource)
            {
                Main.Log("Fixing Resource - " + abilityresource);
                BlueprintAbilityResource resource = BlueprintUtils.GetBlueprint<BlueprintAbilityResource>(abilityresource);
                if (resource.m_MaxAmount.m_Class.Length > 0)
                {
                    resource.m_MaxAmount.m_Class = resource.m_MaxAmount.m_Class.Append(Signifier).ToArray();
                }
            }

            List<String> DomainBuffs = new List<String>()
            {
                "0dfe08afb3cf3594987bab12d014e74b", // Destruction Domain Base Buff
                "f9de414e53a9c23419fa3cfc0daabde7", // Destruction Domain Greater Buff
                "58d86cc848805024abbbefd6abe2d433", // Plant Domain Greater Buff
                "74a4fb45f23705d4db2784d16eb93138", // Protection Domain Self Buff
                "fea7c44605c90f14fa40b2f2f5ae6339", // Protection Domain Greater Buff, 2 context rank configs!
                "aefec65136058694ab20cd71941eec81", // War Domain Base Buff
            };

            foreach (string domainbuff in DomainBuffs)
            {
                Main.Log("Fixing Buff - " + domainbuff);
                BlueprintBuff Buff = BlueprintUtils.GetBlueprint<BlueprintBuff>(domainbuff);
                ContextConfigUtils.AddClassToConfigs(Buff.ComponentsArray, Signifier);
            }

            List<String> DomainFeatures = new List<String>()
            {
                "3298fd30e221ef74189a06acbf376d29", // Strength Domain Major Feature
                
                "d577aba79b5727a4ab74627c4c6ba23c", // Animal Domain Base Feature
            };

            foreach (string domainfeature in DomainFeatures)
            {
                Main.Log("Fixing Feature - " + domainfeature);
                BlueprintFeature feature = BlueprintUtils.GetBlueprint<BlueprintFeature>(domainfeature);
                ContextConfigUtils.AddClassToConfigs(feature.ComponentsArray, Signifier);
            }

            Main.Log("Fixing Sun Domain Ability");
            BlueprintFeature sunfeature = BlueprintUtils.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9"); // Sun Domain Feature which is Wierd
            IncreaseSpellDamageByClassLevel ChannelScaling = (IncreaseSpellDamageByClassLevel)Array.Find(sunfeature.ComponentsArray, component => component is IncreaseSpellDamageByClassLevel);
            ChannelScaling.m_AdditionalClasses = ChannelScaling.m_AdditionalClasses.Append(Signifier).ToArray();

            /*foreach (BlueprintFeatureReference DomainReference in DomainSelection.m_AllFeatures)
            {
                Main.Log("Domain: "+DomainReference.Guid.ToString());
                var Domain = BlueprintUtils.GetBlueprint<BlueprintProgression>(DomainReference.deserializedGuid);
                if (Domain)
                {
                    foreach (LevelEntry level in Domain.LevelEntries)
                    {
                        foreach (BlueprintFeature domainFeature in level.m_Features)
                        {
                            AddFacts domainFacts = (AddFacts)Array.Find(domainFeature.ComponentsArray, component => component is AddFacts);
                            if (domainFacts)
                            {
                                foreach (BlueprintUnitFactReference fact in domainFacts.m_Facts)
                                {
                                    Main.Log("BlueprintFactReference: " + fact.Guid);
                                    BlueprintUnitFact unitFact = BlueprintUtils.GetBlueprint<BlueprintUnitFact>(fact.deserializedGuid);
                                    if (unitFact is BlueprintAbility)
                                    {
                                        ContextRankConfig DomainScaling = (ContextRankConfig)Array.Find(unitFact.ComponentsArray, component => component is ContextRankConfig);
                                        DomainScaling.m_Class.Append(Signifier);
                                    }
                                }
                            }
                        }
                    }
                }       
            }*/
        }

        public static void FixHellknightOrders(BlueprintCharacterClassReference charclass)
        {
            // Order of the Chain
            BlueprintFeature OrderChain = BlueprintUtils.GetBlueprint<BlueprintFeature>(CommonTemplates.OrderChain);


            OrderChain.ComponentsArray = OrderChain.ComponentsArray.Concat(new BlueprintComponent[]
            {
                new AddFeatureOnClassLevel()
                {
                    Level = 1,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("6c27b29fc314c744e9ac860d6beec5de").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Chain1"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 5,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("35e04fbe27a880e4cbf0d82d9fcc2a89").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Chain2"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 9,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("d20cb27b25ab2c144ad610788d12f9d9").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Chain3"+charclass.guid
                },
            }).ToArray();

            // Order of the Godclaw
            BlueprintFeature OrderGodclaw = BlueprintUtils.GetBlueprint<BlueprintFeature>(CommonTemplates.OrderGodclaw);

            // Give the new class features
            OrderGodclaw.ComponentsArray = OrderGodclaw.ComponentsArray.Concat(new BlueprintComponent[]
            {
                new AddFeatureOnClassLevel()
                {
                    Level = 1,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("4b46f15dcb606954e84d954d5a6596eb").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Claw1"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 5,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("c6984918b13d01f4c960b6bbd8238bb7").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Claw2"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 9,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("1333c86f7d5fb9b46bf989b54a3f2a59").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Claw3"+charclass.guid
                },
            }).ToArray();

            // Fix scaling
            BlueprintAbilityResource GodclawResource = BlueprintUtils.GetBlueprint<BlueprintAbilityResource>("01fc99f99d293404d87bc8b7a03ddb22");
            GodclawResource.m_MaxAmount.m_Class = GodclawResource.m_MaxAmount.m_Class.Append(charclass).ToArray();
            GodclawResource.m_MaxAmount.m_ClassDiv = GodclawResource.m_MaxAmount.m_ClassDiv.Append(charclass).ToArray();

            // Order of the Nail
            BlueprintProgression OrderNail = BlueprintUtils.GetBlueprint<BlueprintProgression>(CommonTemplates.OrderNail);

            OrderNail.m_Classes = OrderNail.m_Classes.Append(new BlueprintProgression.ClassWithLevel() { m_Class = charclass, AdditionalLevel = 0 }).ToArray();

            // Order of the Pyre
            BlueprintFeature OrderPyre = BlueprintUtils.GetBlueprint<BlueprintFeature>(CommonTemplates.OrderPyre);

            BlueprintFeature PyreFinal = BlueprintUtils.GetBlueprint<BlueprintFeature>("5c3a2912db578c547bd1884a864dcec4");

            OrderPyre.ComponentsArray = OrderPyre.ComponentsArray.Concat(new BlueprintComponent[]
            {
                new AddFeatureOnClassLevel()
                {
                    Level = 1,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("babbd657ca5068248a3ea7b246054e45").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Pyre1"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 5,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("d0d8a4d7803b116458d01121e6416e9e").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Pyre2"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 9,
                    m_Feature = PyreFinal.ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Pyre3"+charclass.guid
                },
            }).ToArray();

            // Order of the Pyre final ability scaling
            ContextRankConfig PyreScaling = (ContextRankConfig)Array.Find(PyreFinal.ComponentsArray, component => component is ContextRankConfig);
            PyreScaling.m_Class = PyreScaling.m_Class.Append(charclass).ToArray();

            // Order of the Rack
            /*BlueprintFeature OrderRack = BlueprintUtils.GetBlueprint<BlueprintFeature>(CommonTemplates.OrderRack);

            BlueprintFeature RackFirstFeature = BlueprintUtils.GetBlueprint<BlueprintFeature>("44b24a99d1c0c30438b814ad2eff79a7").CreateCopy(;          

            OrderRack.ComponentsArray = OrderRack.ComponentsArray.Concat(new BlueprintComponent[]
            {
                new AddFeatureOnClassLevel()
                {
                    Level = 1,
                    m_Feature = RackFirstFeature.ToReference<BlueprintFeatureReference>(),
                    m_Class = Signifier
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 5,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("fc7c385b155a3744bb7ee5d8b0179ac2").ToReference<BlueprintFeatureReference>(),
                    m_Class = Signifier
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 9,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("dc2fdb62064f1254d8927dc40068f5b8").ToReference<BlueprintFeatureReference>(),
                    m_Class = Signifier
                },
            }).ToArray();

            // Fix the Rack Resource
            BlueprintAbilityResource RackResource = BlueprintUtils.GetBlueprint<BlueprintAbilityResource>("553e894d17eea52429f912219ea531b2");
            RackResource.m_MaxAmount.m_Class.Append(Signifier);
            RackResource.m_MaxAmount.m_ClassDiv.Append(Signifier);

            // Fix the Rack ability feature
            ContextRankConfig RackFeatureScaling = (ContextRankConfig)Array.Find(RackFirstFeature.ComponentsArray, component => component is ContextRankConfig);
            RackFeatureScaling.m_Class.Append(Signifier);

            // Fix the Rack Ability Ability
            BlueprintAbility RackAbility = BlueprintUtils.GetBlueprint<BlueprintAbility>("2714684e63581ed41b3b13b62d648621");
            ContextRankConfig RackAbilityScaling = (ContextRankConfig)Array.Find(RackAbility.ComponentsArray, component => component is ContextRankConfig);
            RackAbilityScaling.m_Class.Append(Signifier);
            */

            // Order of the Scourge
            BlueprintFeature OrderScourge = BlueprintUtils.GetBlueprint<BlueprintFeature>(CommonTemplates.OrderScourge);
            OrderScourge.ComponentsArray = OrderScourge.ComponentsArray.Concat(new BlueprintComponent[]
            {
                new AddFeatureOnClassLevel()
                {
                    Level = 1,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("8c5974ab11a8a0c4b8ee93d98b2473d7").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Scourge1"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 5,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("e4553aa5d0395a346a6b4c7817c9867e").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Scourge2"+charclass.guid
                },
                new AddFeatureOnClassLevel()
                {
                    Level = 9,
                    m_Feature = BlueprintUtils.GetBlueprint<BlueprintFeature>("10bb771a02b0c5c409530ceb4e0594a4").ToReference<BlueprintFeatureReference>(),
                    m_Class = charclass,
                    name = "Scourge3"+charclass.guid
                },
            }).ToArray();
        }
    }
}
