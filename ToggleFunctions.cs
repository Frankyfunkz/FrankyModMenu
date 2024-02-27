using RedLoader;
using RedLoader.Utils;
using Sons.Ai.Vail;
using SonsSdk;
using SUI;
using Sons.Environment;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.Windows;
using System.Collections;
using Sons.Prefabs;
using Sons.Settings;
using Sons.Wearable.Armour;
using Sons.Wearable;
using static Sons.Wearable.Armour.PlayerArmourSystem;
using Sons.Items.Core;
using TheForest;
using SonsSdk.Attributes;
using System.Runtime.CompilerServices;
using Sons.Player;
using Sons.Weapon;
using System.Reflection;
using Sons.Crafting.Structures;
using Construction;
using static FrankyModMenu.ValueFunctions;
using Endnight.Utilities;
using Sons.Gameplay;
using Sons.Cutscenes;
using Sons.Items;
using Sons.Inventory;
using Sons.Gameplay.GameSetup;


namespace FrankyModMenu
{
    internal class ToggleFunctions
    {
        public class ArmourValues
        {
            public float DefaultArmour;
        }
        public static void GodMode(bool onoff)
        {
            Config.GodMode.Value = onoff;
            if (onoff)
            {
                Sons.Settings.Cheats.Setup.GodMode = true;
                return;

            }
            Sons.Settings.Cheats.Setup.GodMode = false;
        }

        public static void InfStamina(bool onoff)
        {
            Config.IsInfStamina.Value = onoff;
            if (onoff)
            {
                Sons.Settings.Cheats.Setup.InfiniteEnergy = true;
                return;
            }
            Sons.Settings.Cheats.Setup.InfiniteEnergy = false;
        }

        public static void NoHungry(bool onoff)
        {
            Config.IsNoHungry.Value = onoff;
            if (onoff)
            {
                LocalPlayer.Vitals.Fullness.SetMin(100);
                return;
            }
            LocalPlayer.Vitals.Fullness.SetMin(0);
        }

        public static void NoDehydration(bool onoff)
        {
            Config.IsNoDehydration.Value = onoff;
            if (onoff)
            {
                LocalPlayer.Vitals.Hydration.SetMin(100);
                return;
            }
            LocalPlayer.Vitals.Hydration.SetMin(0);
        }

        public static void NoSleep(bool onoff)
        {
            Config.IsNoSleep.Value = onoff;
            if (onoff)
            {
                                
                LocalPlayer.Vitals.Rested.SetMin(100);
                return;
            }
            LocalPlayer.Vitals.Rested.SetMin(0);
        }

        public static void InfiniteBreath(bool onoff)
        {
            Config.InfiniteBreath.Value = onoff;
            if (onoff)
            {
                LocalPlayer.FpCharacter._vitals.LungBreathing.CurrentLungAir = 99999;
                LocalPlayer.FpCharacter._vitals.LungBreathing.MaxLungAirCapacity = 99999;
                LocalPlayer.FpCharacter._vitals.LungBreathing.MaxRebreatherAirCapacity = 99999;
                return;

            }
            LocalPlayer.FpCharacter._vitals.LungBreathing.CurrentLungAir = 20;
            LocalPlayer.FpCharacter._vitals.LungBreathing.MaxLungAirCapacity = 20;
            LocalPlayer.FpCharacter._vitals.LungBreathing.MaxRebreatherAirCapacity = 300;
        }

        public static void SetInfiFires(bool onoff)
        {
            Config.InfiFire.Value = onoff;
            // Iterate over each Fire object
            for (int i = 1; ; i++)
            {
                GameObject parentGameObjectFire = GameObject.Find("Fire" + i + "/FireElement(Clone)/CookingSystem");
                GameObject parentGameObjectBonFire = GameObject.Find("Fire" + i + "/BonFireElement(Clone)");
                if (parentGameObjectFire == null && parentGameObjectBonFire == null)
                {
                    // If no more Fire objects are found, exit the loop
                    break;
                }

                
                CookingFireNew cookingFire = parentGameObjectFire?.GetComponent<CookingFireNew>();
                // Apply logic if CookingFireNew component is found
                if (cookingFire != null)
                {
                    // Apply logic based on the configuration setting
                    if (onoff)
                    {
                        //RLog.Msg("InfiFire value true, checking islit");
                        if (!cookingFire.IsLit)
                        {
                            // Set the fire alight and adjust fuel amount and drain rate
                            //RLog.Msg("Fire not lit, setting alight, setting maxfuel and fuel 99999 and low drainrate");
                            cookingFire.SetAlight();
                            cookingFire._fuel._fuelMax = 999999f;
                            cookingFire._fuel.Amount = 999999f;
                            cookingFire._saveData.FuelDrainRate = 1E-05f;
                        }
                        else
                        {

                            // Adjust fuel amount and drain rate
                            //RLog.Msg("Fire already lit, setting maxfuel and fuel 99999 and low drainrate");
                            cookingFire._fuel._fuelMax = 999999f;
                            cookingFire._fuel.Amount = 999999f;
                            cookingFire._saveData.FuelDrainRate = 1E-05f;
                        }

                    }
                    else
                    {
                        // Restore default values
                        //RLog.Msg("InfiFire value false, restoring defaults");
                        cookingFire._fuel._fuelMax = 1260f;
                        cookingFire._fuel.Amount = 1260f;
                        cookingFire._saveData.FuelDrainRate = 0.25f;
                    }
                }
                else
                {
                    // Handle case where CookingFireNew component is not found
                    //RLog.Msg("CookingFireNew component not found on Fire" + i + ".");
                }

                CookingFireNew cookingBonFire = parentGameObjectBonFire?.GetComponent<CookingFireNew>();
                if (cookingBonFire != null)
                {
                    // Apply logic based on the configuration setting
                    if (onoff)
                    {
                        //RLog.Msg("InfiFire value true, checking islit");
                        if (!cookingBonFire.IsLit)
                        {
                            // Set the fire alight and adjust fuel amount and drain rate
                            //RLog.Msg("Fire not lit, setting alight, setting maxfuel and fuel 99999 and low drainrate");
                            cookingBonFire.SetAlight();
                            cookingBonFire._fuel._fuelMax = 999999f;
                            cookingBonFire._fuel.Amount = 999999f;
                            cookingBonFire._saveData.FuelDrainRate = 1E-05f;
                        }
                        else
                        {

                            // Adjust fuel amount and drain rate
                            //RLog.Msg("Fire already lit, setting maxfuel and fuel 99999 and low drainrate");
                            cookingBonFire._fuel._fuelMax = 999999f;
                            cookingBonFire._fuel.Amount = 999999f;
                            cookingBonFire._saveData.FuelDrainRate = 1E-05f;
                        }

                    }
                    else
                    {
                        // Restore default values
                        //RLog.Msg("InfiFire value false, restoring defaults");
                        cookingBonFire._fuel._fuelMax = 1260f;
                        cookingBonFire._fuel.Amount = 1260f;
                        cookingBonFire._saveData.FuelDrainRate = 0.25f;
                    }
                }
                else
                {
                    // Handle case where cookingBonFireNew component is not found
                    //RLog.Msg("cookingBonFireNew component not found on Fire" + i + ".");
                }
            }
        }

        public static void Invisibility(bool onoff)
        {
            Config.Invisibility.Value = onoff;
            if (onoff)
            {
                //RLog.Msg(ConsoleColor.Green, "Invisible is true");
                VailActorManager.SetGhostPlayer(true);

                return;
            }
            //RLog.Msg(ConsoleColor.Red, "Invisible is false");
            VailActorManager.SetGhostPlayer(false);

        }

        public static void InstantBuild(bool onoff)
        {
            Config.InstantBuild.Value = onoff;
            if (onoff)
            {
                StructureCraftingSystem._instance.InstantBuild = true;
                return;
            }
            StructureCraftingSystem._instance.InstantBuild = false;
        }

        static float? fallDamage;
        public static void NoFallDamage(bool onoff)
        {
            fallDamage ??= LocalPlayer.FpCharacter._baseFallDamage;
            Config.IsNoFallDamage.Value = onoff;
            if (onoff)
            {
                LocalPlayer.FpCharacter._baseFallDamage = 0;
                return;
            }
            LocalPlayer.FpCharacter._baseFallDamage = (float)fallDamage;
        }
        public static void StopTime(bool onoff)
        {
            Config.StopTime.Value = onoff;
            if (onoff)
            {
                //RLog.Msg("StopTime on basetimespeed 0f");

                TimeOfDayHolder.SetBaseTimeSpeed(0f);
                return;
            }
            TimeOfDayHolder.SetBaseTimeSpeed(FrankyModMenu.baseSpeedMultiplier);
            //RLog.Msg("StopTime off basetimespeed set to default");

        }
        public static void InfiniteJumps(bool onoff)
        {
            Config.IsInfiniteJumps.Value = onoff;
        }
        public static void MarioMode(bool onoff)
        {
            Config.IsMarioMode.Value = onoff;
        }

        public static void CreativeMode(bool onoff)
        {
            //Sons.Gameplay.GameSetup.CreativeModeUiEnabler creativeModeUiEnabler = new Sons.Gameplay.GameSetup.CreativeModeUiEnabler();
            //Sons.Gameplay.GameSetup.GameSetupManager.ApplyCreativeGameModeSettings()
            //Sons.Gameplay.GameSetup.GameSetupManager.SetCreativeModeSetting(true);
            Config.CreativeMode.Value = onoff;
            if(onoff)
            {
                Sons.Gameplay.GameSetup.GameSetupManager.SetCreativeModeSetting(true);
                return;
            }
            Sons.Gameplay.GameSetup.GameSetupManager.SetCreativeModeSetting(false);
        }

        public static void InfiniteArtifact(bool onoff)
        {
            Config.InfiniteArtifact.Value = onoff;
            if(onoff)
            {
                SetInfiArtifact();
                return;
            }
            RestoreInfiArtifact();
        }

        public static void SetInfiArtifact()
        {
            GameObject artiObject = GameObject.Find("ArtifactHeld");
            ArtifactItemController artiObjItemCont = artiObject.GetComponent<ArtifactItemController>();
            
            if (artiObject != null)
            {
                //RLog.Msg("SetInfiArti - artiObject isnt null");
                if (artiObjItemCont != null)
                {
                    //RLog.Msg("SetInfiArti - ArtiObjectItemController isnt null, setting max and current to 999999");
                    artiObjItemCont._fuel.MaxVolume = 999999;
                    artiObjItemCont._fuel.CurrentVolume = 999999;
                }
                else
                {
                    //RLog.Msg("SetInfiArti - artiObjectItemControll not found");
                    return;
                }
            }
            else
            {
                //RLog.Msg("SetInfiArti - ArtifactHeld not found");
                return; 
            }
            
        }

        public static void RestoreInfiArtifact()
        {
            /*
            VolumeContainerItemInstanceModule VolData;
            if (VolData.ItemId == 707)
            {

            }
            //var artiItem = ItemDatabaseManager.ItemById(707);
            */
            GameObject artiObject = GameObject.Find("ArtifactHeld");
            ArtifactItemController artiObjItemCont = artiObject.GetComponent<ArtifactItemController>();

            if (artiObject != null)
            {
                //RLog.Msg("RestoreInfiArti - artiObject isnt null");
                if (artiObjItemCont != null)
                {
                    //RLog.Msg("RestoreInfiArti - ArtiObjectItemController isnt null, setting max and current to 600");
                    artiObjItemCont._fuel.MaxVolume = 600;
                    artiObjItemCont._fuel.CurrentVolume = 600;
                }
                else
                {
                    //RLog.Msg("RestoreInfiArti - artiObjectItemControll not found");
                    return;
                }
            }
            else
            {
                //RLog.Msg("RestoreInfiArti - ArtifactHeld not found");
                return;
            }
        }
        public static void UnbreakableArmour(bool onoff)
        {
            Config.UnBreakableArmor.Value = onoff;
            if (onoff)
            {
                SetUnbreakableArmour();
                CheckArmourValues().RunCoro();
                return;
            }
            RestoreDefaultArmorValues();
        }

        private static bool NeedsArmourRefresh(PlayerArmourSystem armorSystem)
        {
            if (armorSystem != null)
            {
                foreach (var armorSlot in armorSystem._armourSlotData)
                {
                    if (armorSlot.RemainingArmourpoints <= 10000)
                    {
                        //RLog.Msg("Found armor piece with <= 10000 armor");
                        return true;
                    }
                }
            }

            return false;
        }
        public static IEnumerator CheckArmourValues()
        {

            var armorSystem = LocalPlayer.Transform.Find("PlayerAnimator/ArmourSystem").GetComponent<PlayerArmourSystem>();

            while (true)
            {
                if (!Config.UnBreakableArmor.Value)
                {
                    // Stop the coroutine if unbreakable armour is turned off
                    // RLog.Msg("UnbreakableArmour is off, stopping coro");
                    yield break;
                }

                // Check remaining armour points and perform actions as needed
                if (NeedsArmourRefresh(armorSystem))
                {
                    // RLog.Msg("Enum - Setting pieces to unbreakableArmourPoints");
                    SetUnbreakableArmour();
                }

                // Wait for 60 seconds before checking again
                yield return new WaitForSeconds(60f);
                // RLog.Msg("Waited 60 seconds");
            }
        }
  
        public static void SetUnbreakableArmour()
        {

            float unbreakableArmourPoints = 50000f;

            var armorSystem = LocalPlayer.Transform.Find("PlayerAnimator/ArmourSystem").GetComponent<PlayerArmourSystem>();
            if (armorSystem != null)
            {
                {
                    for (int i = 0; i < armorSystem._armourSlotData.Count; i++)
                    {
                        var currentSlot = armorSystem._armourSlotData[i];
                        var currentArmourPieces = currentSlot.ArmourPiece;

                        if (currentArmourPieces != null)
                        {
                            //RLog.Msg($"Slot {i} Armour Pieces == {currentArmourPieces}");
                            currentSlot.RemainingArmourpoints = unbreakableArmourPoints;
                        }
                        else
                        {
                            //RLog.Msg($"Slot {i} Armour Pieces == null");
                        }
                    }
                }
            }
        }

        private static Dictionary<int, ArmourValues> defaultArmourValues = new()
        {
            { 473, new ArmourValues { DefaultArmour = 25f } },  // Leaf
            { 519, new ArmourValues { DefaultArmour = 40f } },  // Deer
            { 494, new ArmourValues { DefaultArmour = 65f } },  // Bone
            { 593, new ArmourValues { DefaultArmour = 80f } },  // Creepy
            { 554, new ArmourValues { DefaultArmour = 100f } }   // Tech
        };

        public static void RestoreDefaultArmorValues()
        {
            var armorSystem = LocalPlayer.Transform.Find("PlayerAnimator/ArmourSystem").GetComponent<PlayerArmourSystem>();

            if (armorSystem != null)
            {
                foreach (var armorSlot in armorSystem._armourSlotData)
                {
                    int itemId = armorSlot.ArmourPiece?.ItemId ?? -1; // Assuming ItemId is an int property in your ArmourPiece class

                    // Use GetArmourPieceById to retrieve the WearablePiece based on the item ID
                    WearablePiece armorPiece = armorSystem.GetArmourPieceById(itemId);

                    if (armorPiece != null)
                    {
                        if (defaultArmourValues.TryGetValue(itemId, out var defaultValues))
                        {
                            armorSlot.RemainingArmourpoints = defaultValues.DefaultArmour;
                            //RLog.Msg($"Restored default armor value for ItemId {itemId}");
                        }
                        else
                        {
                            //RLog.Msg($"Default values not found for ItemId {itemId}");
                        }
                    }
                    else
                    {
                        //RLog.Msg($"ArmourPiece not found for ItemId {itemId}");
                    }
                }
            }
        }
    }
}
