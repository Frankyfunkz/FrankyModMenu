using RedLoader;
using RedLoader.Utils;
using SonsSdk;
using SUI;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.Windows;
using System.Collections;
using Sons.Environment;
using System.Linq;
using Sons.Items.Core;
using Sons.Wearable.Armour;
using Sons.Wearable;
using static Sons.Wearable.Armour.PlayerArmourSystem;

namespace FrankyModMenu
{
    internal class ValueFunctions
    {
        private static float floatValue;
        public class DamageValues
        {
            public float Damage;
            public float Charged;
            public float Smash;
        }
        public static void WalkSpeed(float value)
        {
            Config.WalkSpeed.Value = value;
            LocalPlayer.FpCharacter.SetWalkSpeed(value);
        }

        public static void RunSpeed(float value)
        {
            Config.RunSpeed.Value = value;
            LocalPlayer.FpCharacter.SetRunSpeed(value);
        }

        public static void SwimSpeed(float value)
        {
            Config.SwimSpeed.Value = value;
            LocalPlayer.FpCharacter.SetSwimSpeed(value);
        }

        public static void JumpMultiplier(float value)
        {
            Config.JumpMultiplier.Value = value;
            LocalPlayer.FpCharacter.SetSuperJump(value);
        }

        public static void TimeMultiplier(float value)
        {
            if (float.TryParse(Config.TimeMultiplier.Value, out floatValue))
            {
                SetTimeMultiplier();
            }
            else
            {
                RLog.Error("value for config.TimeMultiplier did not pass as a valid float - Contact Franky");
            }
        }

        public static void SetTimeMultiplier()
        {
            if (Config.StopTime.Value)
            {
                RLog.Error("Cannot change time multiplier when Stop Time is Enabled");
                return;
            }
            ///    RLog.Msg("SetTimeMultiplier called 1f x setvalue");
            TimeOfDayHolder.SetBaseTimeSpeed(1f * floatValue);
        }
        public static void DamageMultiplier(float value)
        {
            if (float.TryParse(Config.DamageMultiplier.Value, out floatValue))
            {
                SetDamageMultiplier();
            }
            else
            {
                RLog.Error("value for config.DamageMultiplier did not pass as a valid float - Contact Franky");
            }
        }

        public static void SetDamageMultiplier()
        {
            if (!LocalPlayer.IsInWorld)
            {
                RLog.Msg("DamageMultiplier returned, player not in the world");
                return;
            }

            foreach (var entry in defaultValues)
            {
                SetNewDamage(entry.Key, entry.Value);
            }
        }

        public void RestoreDefaultDamageValues()
        {
            foreach (var entry in defaultValues)
            {
                SetNewDamage(entry.Key, entry.Value);
            }
        }

        private static Dictionary<int, DamageValues> defaultValues = new()
        {
                // Ammo Type
                { 476, new DamageValues { Damage = 2f, Charged = 0f, Smash = 0f } },  // Small Rock
                { 524, new DamageValues { Damage = 2f, Charged = 0f, Smash = 0f } },  // Golf Ball
                { 618, new DamageValues { Damage = 30f, Charged = 0f, Smash = 0f } }, // 3dPrintedArrow
                { 373, new DamageValues { Damage = 35f, Charged = 0f, Smash = 0f } }, // TacticalBowAmmo
                { 507, new DamageValues { Damage = 20f, Charged = 0f, Smash = 0f } }, // CraftedArrow
                { 368, new DamageValues { Damage = 100f, Charged = 0f, Smash = 0f } }, // CrossBowBolt
                { 364, new DamageValues { Damage = 25f, Charged = 0f, Smash = 0f } }, // BuckShotAmmo
                { 363, new DamageValues { Damage = 100f, Charged = 0f, Smash = 0f } }, // SlugAmmo
                { 362, new DamageValues { Damage = 35f, Charged = 0f, Smash = 0f } }, // 9mmAmmo
                { 387, new DamageValues { Damage = 100f, Charged = 0f, Smash = 0f } }, // RifleAmmo

                // Explosive Type
                { 382, new DamageValues { Damage = 200f, Charged = 0f, Smash = 0f } }, // GrenadeAmmo
                { 417, new DamageValues { Damage = 200f, Charged = 0f, Smash = 0f } }, // TimeBomb

                // Melee Type
                { 503, new DamageValues { Damage = 3f, Charged = 0f, Smash = 0f } },  // Torch
                { 392, new DamageValues { Damage = 4f, Charged = 0f, Smash = 4f } },  // Stick
                { 393, new DamageValues { Damage = 10f, Charged = 0f, Smash = 20f } },  // Rock
                { 405, new DamageValues { Damage = 5f, Charged = 0f, Smash = 8f } },  // Bone
                { 380, new DamageValues { Damage = 12f, Charged = 0f, Smash = 0f } }, // CombatKnife
                { 474, new DamageValues { Damage = 10f, Charged = 0f, Smash = 25f } }, // Spear 
                { 340, new DamageValues { Damage = 8f, Charged = 0f, Smash = 15f } },  // Guitar
                { 396, new DamageValues { Damage = 12f, Charged = 0f, Smash = 14f } }, // TaserStick
                { 525, new DamageValues { Damage = 4f, Charged = 4f, Smash = 4f } },  // Putter 
                { 485, new DamageValues { Damage = 8f, Charged = 0f, Smash = 0f } }, // Shovel
                { 359, new DamageValues { Damage = 14f, Charged = 22f, Smash = 18f } }, // Machete
                { 477, new DamageValues { Damage = 15f, Charged = 20f, Smash = 20f } }, // CraftedClub
                { 379, new DamageValues { Damage = 12f, Charged = 26f, Smash = 25f } }, // TacticalAxe
                { 356, new DamageValues { Damage = 30f, Charged = 35f, Smash = 40f } }, // ModernAxe
                { 431, new DamageValues { Damage = 35f, Charged = 45f, Smash = 50f } }, // FireAxe
                { 394, new DamageValues { Damage = 12f, Charged = 25f, Smash = 30f } }, // Chainsaw
                { 367, new DamageValues { Damage = 30f, Charged = 45f, Smash = 60f } }  // Katana
        
            };

        public static void SetNewDamage(int itemId, DamageValues defaultValues)
        {
            ItemData item = ItemDatabaseManager.ItemById(itemId);
            //Handle Grenade and Bomb
            if (item.Ammo != null && item.Ammo.ProjectileInfo.ExplosionDamage > 1)
            {
                item.Ammo.ProjectileInfo._explosionDamage = defaultValues.Damage * floatValue;
            }
            //Handle Bullets and Regular arrows
            else if (item.Ammo != null && item.Ammo.ProjectileInfo.ProjectileType == ImpactProjectileType.Bullet)
            {
                item.Ammo.ProjectileInfo.muzzleDamage = defaultValues.Damage * floatValue;
            }
            // Handle Small Rocks and Golf Balls
            else if (item.Ammo != null && item.Ammo.ProjectileInfo.ExplosionDamage <= 0 && item.Ammo.ProjectileInfo.ProjectileType == ImpactProjectileType.NonLethal)
            {
                item.Ammo.ProjectileInfo.muzzleDamage = defaultValues.Damage * floatValue;
            }
            // Handle Spear
            else if (item.Ammo != null && item.Ammo.ProjectileInfo.ProjectileType == ImpactProjectileType.Spear)
            {
                item.Ammo.ProjectileInfo.muzzleDamage = defaultValues.Damage * floatValue;
                item.MeleeWeaponData.Damage = defaultValues.Damage * floatValue;
                item.MeleeWeaponData.GroundSmashDamage = defaultValues.Smash * floatValue;
            }
            // Handle melee with Normal, Charge, and Smash
            else if (item.MeleeWeaponData.ChargeAttackDamage > 0 && item.MeleeWeaponData.GroundSmashDamage > 0)
            {
                item.MeleeWeaponData.Damage = defaultValues.Damage * floatValue;
                item.MeleeWeaponData.ChargeAttackDamage = defaultValues.Charged * floatValue;
                item.MeleeWeaponData.GroundSmashDamage = defaultValues.Smash * floatValue;
            }
            // Handle Melee with Normal and Smash
            else if (item.MeleeWeaponData.ChargeAttackDamage <= 0 && item.MeleeWeaponData.GroundSmashDamage > 0)
            {
                item.MeleeWeaponData.Damage = defaultValues.Damage * floatValue;
                item.MeleeWeaponData.GroundSmashDamage = defaultValues.Smash * floatValue;
            }
            // Handle Melee with only Normal damage
            else
            {
                item.MeleeWeaponData.Damage = defaultValues.Damage * floatValue;
            }
        }

        

        /*public static void UnbreakableArmor()
        {

            //LocalPlayer.Stats._armourSystem = 

               Array values = Enum.GetValues(typeof(WearableSlots));
            _armourSlotData = new List<PlayerArmourSystem.ArmourSlotData>(values.Length);
            
            foreach (object obj in values)
            {
                _armourSlotData.Add(new PlayerArmourSystem.ArmourSlotData
                {
                    Slot = (WearableSlots)obj,
                    ArmourPiece = GetArmourPieceById(494, 593, 519, ),
                    ArmourInstance = null,
                    RemainingArmourpoints = 50000f
                });
            }

        }*/
    }
}
