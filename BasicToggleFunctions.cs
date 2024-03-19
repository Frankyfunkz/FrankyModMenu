using Sons.Ai.Vail;
using Sons.Crafting.Structures;
using Sons.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheForest.Utils;

namespace FrankyModMenu
{
    internal class BasicToggleFunctions
    {
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
        
        public static void CreativeMode(bool onoff)
        {
            Config.CreativeMode.Value = onoff;
            if (onoff)
            {
                Sons.Gameplay.GameSetup.GameSetupManager.SetCreativeModeSetting(true);
                return;
            }
            Sons.Gameplay.GameSetup.GameSetupManager.SetCreativeModeSetting(false);
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

        public static void InfiniteBuildItems(bool onoff)
        {
            Config.InfiniteBuildItems.Value = onoff;
            if (onoff)
            {
                LocalPlayer.Inventory.HeldOnlyItemController.InfiniteHack = true;
                return;

            }
            LocalPlayer.Inventory.HeldOnlyItemController.InfiniteHack = false;
        }
        
        public static void OneHitCutTrees(bool onoff)
        {
            Config.OneHitCutTrees.Value = onoff;
            if (onoff)
            {
                Sons.Settings.Cheats.Setup.OneHitTreeCutting = true;
                return;

            }
            Sons.Settings.Cheats.Setup.OneHitTreeCutting = false;
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
            TimeOfDayHolder.SetBaseTimeSpeed(FrankyModMenu.FbaseSpeedMultiplier);
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
    }
}
