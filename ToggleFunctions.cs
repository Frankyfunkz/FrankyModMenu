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
using System.Text;
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
using Sons.Utils;


namespace FrankyModMenu
{
    internal class ToggleFunctions
    {
        public class ArmourValues
        {
            public float DefaultArmour;
        }

        // Define a list to store processed fire objects
        private static List<GameObject> processedFireObjects = new List<GameObject>();
        public static bool fireCoroShouldRun = true;

        // Method to set infinite fires
        public static void SetInfiFires(bool onoff)
        {
            Config.InfiFire.Value = onoff;

            // Start the coroutine if it's not already running and if onoff is true
            if (onoff && fireCoroShouldRun)
            {
                fireCoroShouldRun = true;
                CheckForFires().RunCoro();
            }

            // Iterate over each Fire object
            for (int i = 1; ; i++)
            {
                GameObject parentGameObjectFire = GameObject.Find("Fire" + i + "/FireElement(Clone)/CookingSystem");
                GameObject parentGameObjectBonFire = GameObject.Find("Fire" + i + "/BonFireElement(Clone)/CookingSystem");

                if (parentGameObjectFire == null && parentGameObjectBonFire == null)
                {
                    // If no more Fire objects are found, exit the loop
                    break;
                }

                UpdateFires(parentGameObjectFire, onoff);
                UpdateFires(parentGameObjectBonFire, onoff);
            }
        }

        // Check for new fires and remove no longer existing fires
        public static IEnumerator CheckForFires()
        {
            while (fireCoroShouldRun)
            {
                // Wait for 2 minutes before checking again
                yield return new WaitForSeconds(120);

                // Check if Config.InfiFire.Value is true
                if (!Config.InfiFire.Value)
                {
                    // If it's false, stop the coroutine
                    fireCoroShouldRun = false;
                    yield break;
                }

                // Check for new fires and remove no longer existing fires
                CheckAndProcessFires();

                // Update the fires after checking for new fires
                foreach (GameObject fireObject in processedFireObjects)
                {
                    UpdateFires(fireObject, Config.InfiFire.Value);
                }
            }
        }

        private static void CheckAndProcessFires()
        {
            // Create a new list to store the updated fire objects
            List<GameObject> updatedFireObjects = new List<GameObject>();

            // Iterate over each Fire object and add it to the updated list if it still exists
            for (int i = 1; ; i++)
            {
                GameObject parentGameObjectFire = GameObject.Find("Fire" + i + "/FireElement(Clone)/CookingSystem");
                GameObject parentGameObjectBonFire = GameObject.Find("Fire" + i + "/BonFireElement(Clone)/CookingSystem");

                if (parentGameObjectFire == null && parentGameObjectBonFire == null)
                {
                    // If no more Fire objects are found, exit the loop
                    break;
                }

                if (parentGameObjectFire != null)
                {
                    updatedFireObjects.Add(parentGameObjectFire);
                }

                if (parentGameObjectBonFire != null)
                {
                    updatedFireObjects.Add(parentGameObjectBonFire);
                }
            }

            // Check for removed fires and remove them from the processed list
            foreach (GameObject fireObject in processedFireObjects)
            {
                if (!updatedFireObjects.Contains(fireObject))
                {
                    // Do something here if needed
                }
            }

            // Update the processed fire objects list with the updated list
            processedFireObjects = updatedFireObjects;

            // Reapply values to all fire objects
            foreach (GameObject fireObject in processedFireObjects)
            {
                UpdateFires(fireObject, Config.InfiFire.Value);
            }
        }

        private static void UpdateFires(GameObject fireElement, bool onoff)
        {
            if (fireElement != null)
            {
                CookingFireNew cookingFire = fireElement.GetComponent<CookingFireNew>();

                if (cookingFire != null)
                {
                    if (onoff)
                    {
                        // Adjust fuel properties
                        cookingFire._fuel._fuelMax = 3660f;
                        cookingFire._fuel.Amount = 3660f;
                        cookingFire._saveData.FuelDrainRate = 0;
                    }
                    else
                    {
                        // Restore default values
                        cookingFire._fuel._fuelMax = 3600f;
                        cookingFire._fuel.Amount = 3660f;
                        cookingFire._saveData.FuelDrainRate = 0.25f;
                    }
                }
            }
        }

        public static void InfiniteArtifact(bool onoff)
        {
            Config.InfiniteArtifact.Value = onoff;
            if (onoff)
            {
                SetInfiArtifact();
                return;
            }
            RestoreInfiArtifact();
        }

        public static void SetInfiArtifact()
        {
            GameObject artiObject = GameObject.Find("ArtifactHeld");
            

            if (artiObject != null)
            {
                ArtifactItemController artiObjItemCont = artiObject.GetComponent<ArtifactItemController>();
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
            GameObject artiObject = GameObject.Find("ArtifactHeld");
            

            if (artiObject != null)
            {
                ArtifactItemController artiObjItemCont = artiObject.GetComponent<ArtifactItemController>();
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
