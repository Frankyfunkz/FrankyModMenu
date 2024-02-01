using SUI;
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using System.Text;
using System.Threading.Tasks;
using static SUI.SettingsRegistry;
using UnityEngine;
using SonsSdk;
using RedLoader;

namespace FrankyModMenu
{

    internal class CustomSettingsReg
    {

        /*
        public static bool ReturnedToTitleScreen = false;

        [HarmonyPatch(typeof(SettingsRegistry), nameof(SettingsRegistry.CreateSettings))]
        public static class PatchSettingsRegistryCreateConfigEntry
        {
            [HarmonyPostfix]
            public static void Postfix(ref SettingsConfigEntry __instance)
            {
                // Check your condition here, e.g., PlayerIsInMainMenu()
                if ((FrankyModMenu._firstStart == false) && (ReturnedToTitleScreen == true))
                {
                    // If the player returned to the main menu and is NOT first start, undo the changes
                    RLog.Msg("Firststart false, returned title screen true, patching");
                    __instance = null;
                }
                // If the player did not return to the main menu and/or is not first start, do nothing and exit the method
                return;

            }


        }
        */
    }
}
