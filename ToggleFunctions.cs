using RedLoader;
using RedLoader.Utils;
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

namespace FrankyModMenu
{
    internal class ToggleFunctions
    {
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

        /*private static List<PlayerArmourSystem.ArmourSlotData> _armourSlotData;
        public static List<ArmourPiece> _armourPieces = new List<ArmourPiece>();

        public static ArmourPiece GetArmourPieceById(int itemId)
        {
            return _armourPieces.Find((ArmourPiece x) => x.ItemId == itemId);
        }

        
       private static int GetItemIdForSlot(WearableSlots slot)
        {
            var singleSlotCollection = new List<Sons.Wearable.WearablePiece.SlotAndRenderable>
            {
                new Sons.Wearable.WearablePiece.SlotAndRenderable { Slot = slot }
            };

            foreach (var equippedArmor in _armourPieces)
            {
                // Check if the slot is valid and the item ID is valid
                if (IsSlotValid(singleSlotCollection) && IsItemIdValid(equippedArmor.ItemId))
                {
                    // If a valid item ID is found in the slots, return it
                    return equippedArmor.ItemId;
                }
            }

            // If no valid item ID is found, return a default value
            return 0;
        }
        private static bool IsSlotValid(IEnumerable<WearablePiece.SlotAndRenderable> slots)
        {
            // Convert the IEnumerable to List before using Any
            var slotList = slots.ToList();

            // Check if any slot in the collection is valid for armor
            foreach (var slot in slotList)
            {
                if (slot.Slot == WearableSlots.Head ||
                    slot.Slot == WearableSlots.Torso ||
                    slot.Slot == WearableSlots.LeftBicep ||
                    slot.Slot == WearableSlots.RightBicep ||
                    slot.Slot == WearableSlots.LeftForearm ||
                    slot.Slot == WearableSlots.RightForearm ||
                    slot.Slot == WearableSlots.LeftThigh ||
                    slot.Slot == WearableSlots.RightThigh ||
                    slot.Slot == WearableSlots.LeftShin ||
                    slot.Slot == WearableSlots.RightShin)
                {
                    // If any valid slot is found, return true
                    return true;
                }
            }

            // If no valid slot is found, return false
            return false;
        }

        private static bool IsSlotValid(WearableSlots slot)
        {
            // Check if any slot in the collection is valid for armor
            return slot != WearableSlots.Head &&
                   slot != WearableSlots.Torso &&
                   slot != WearableSlots.LeftBicep &&
                   slot != WearableSlots.RightBicep &&
                   slot != WearableSlots.LeftForearm &&
                   slot != WearableSlots.RightForearm &&
                   slot != WearableSlots.LeftThigh &&
                   slot != WearableSlots.RightThigh &&
                   slot != WearableSlots.LeftShin &&
                   slot != WearableSlots.RightShin;
        }

        private static bool IsItemIdValid(int itemId)
        {
            // List of valid item IDs
            List<int> validItemIds = new List<int> { 473, 494, 519, 554, 593 };

            // Check if the provided item ID is in the list of valid item IDs
            return validItemIds.Contains(itemId);
        }

        public class ArmourSystem : MonoBehaviour
        {
            private static float unbreakableArmourPoints = 50000f;
            private static List<PlayerArmourSystem.ArmourSlotData> _armourSlotData;
            private static List<ArmourPiece> _armourPieces = new List<ArmourPiece>();

            private void Start()
            {
                // Start the coroutine when the MonoBehaviour initializes
                CheckArmourPointsPeriodically().RunCoro();
            }

            private IEnumerator CheckArmourPointsPeriodically()
            {
                // Run the check every second (adjust the interval as needed)
                while (true)
                {
                    yield return new WaitForSeconds(10f);

                    foreach (var armourSlotData in _armourSlotData)
                    {
                        // Check if RemainingArmourPoints fall below 5000 and reset if needed
                        if (armourSlotData.RemainingArmourpoints < 5000)
                        {
                            armourSlotData.RemainingArmourpoints = unbreakableArmourPoints;
                        }
                    }
                }
            }

            private static void UpdateArmourPoints(ArmourSlotData armourSlotData)
            {
                // Update RemainingArmourPoints directly within ArmourSlotData
                armourSlotData.RemainingArmourpoints = unbreakableArmourPoints;
            }

            public static void UnbreakableArmor(bool onoff)
            {
                if (onoff) 
                { 
                Array values = Enum.GetValues(typeof(WearableSlots));
                _armourSlotData = new List<PlayerArmourSystem.ArmourSlotData>(values.Length);

                    foreach (object obj in values)
                    {
                        WearableSlots slot = (WearableSlots)obj;

                        if (IsSlotValid(slot))
                        {
                            int itemId = GetItemIdForSlot(slot);

                            // Check if the item ID is in the list of valid item IDs
                            if (IsItemIdValid(itemId))
                            {
                                foreach (var armourSlotData in _armourSlotData)
                                {
                                    // Update ArmourPoints for the ArmourSlotData
                                    UpdateArmourPoints(armourSlotData);
                                }

                                // Add the armour data to the list
                                _armourSlotData.Add(new PlayerArmourSystem.ArmourSlotData
                                {
                                    Slot = slot,
                                    ArmourPiece = GetArmourPieceById(itemId),
                                    ArmourInstance = null,
                                    RemainingArmourpoints = unbreakableArmourPoints
                                });
                            }
                        }
                        else
                        {
                            // Skip slots that don't hold armor
                            continue;
                        }
                    }
                }
            }
        }*/
    }
}
