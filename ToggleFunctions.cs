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
using Sons.Crafting.Structures;


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
                Sons.Settings.Cheats.GodMode = true;
                return;

            }
            Sons.Settings.Cheats.GodMode = false;
        }

        public static void InfStamina(bool onoff)
        {
            Config.IsInfStamina.Value = onoff;
            if (onoff)
            {
                Sons.Settings.Cheats.InfiniteEnergy = true;
                return;
            }
            Sons.Settings.Cheats.InfiniteEnergy = false;
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

        public static void UnbreakableArmour(bool onoff)
        {
            Config.UnBreakableArmor.Value = onoff;
            if (onoff)
            {
                SetUnbreakableArmour();
                return;
            }
            RestoreDefaultArmorValues();
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
