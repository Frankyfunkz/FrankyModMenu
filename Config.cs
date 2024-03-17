using RedLoader;
using RedLoader.Preferences;
using SonsSdk;
using TheForest.Utils;
using SonsGameManager;
using Sons.Cutscenes;
using UnityEngine;
using SonsSdk.Attributes;
using TMPro;

namespace FrankyModMenu;

public static class Config
{
    public static bool _hasBeenSet = false;

    public static ConfigCategory Category { get; private set; }
    public static ConfigCategory Player { get; private set; }
    public static ConfigCategory Game { get; private set; }
    public static ConfigCategory Physics { get; private set; }
    public static ConfigCategory Misc { get; private set; }

    //public static KeybindConfigEntry ToggleKey { get; private set; }
    public static ConfigEntry<bool> ShouldSaveSettings { get; private set; }
    public static ConfigEntry<float> BadPCDelay { get; private set; }

    public static ConfigEntry<bool> GodMode { get; private set; }
    public static ConfigEntry<bool> UnBreakableArmor { get; private set; }
    public static ConfigEntry<bool> IsInfStamina { get; private set; }
    public static ConfigEntry<bool> IsNoHungry { get; private set; }
    public static ConfigEntry<bool> IsNoDehydration { get; private set; }
    public static ConfigEntry<bool> IsNoSleep { get; private set; }
    public static ConfigEntry<bool> InfiniteBreath { get; private set; }
    public static ConfigEntry<bool> IsNoFallDamage { get; private set; }
    public static ConfigEntry<bool> NoBurny { get; private set; }
    public static ConfigEntry<bool> Invisibility { get; private set; }
    public static ConfigEntry<bool> InfiniteAmmo { get; private set; }
    [SettingsUiHeader("Note: Artifact needs to be equipped to toggle Infinite Artifact On/Off", TextAlignmentOptions.MidlineLeft, true)]
    public static ConfigEntry<bool> InfiniteArtifact { get; private set; }
    public static ConfigEntry<string> DamageMultiplier { get; private set; }

    public static ConfigEntry<bool> CreativeMode { get; private set; }
    public static ConfigEntry<bool> InfiniteBuildItems { get; private set; }
    public static ConfigEntry<bool> InstantBuild { get; private set; }
    public static ConfigEntry<bool> OneHitCutTrees { get; private set; }
    public static ConfigEntry<bool> InfiFire { get; private set; }
    public static ConfigEntry<bool> StopTime { get; private set; }
    public static ConfigEntry<string> TimeMultiplier { get; private set; }

    public static ConfigEntry<float> WalkSpeed { get; private set; }
    public static ConfigEntry<float> RunSpeed { get; private set; }
    public static ConfigEntry<float> SwimSpeed { get; private set; }
    public static ConfigEntry<float> JumpMultiplier { get; private set; }
    public static ConfigEntry<bool> IsInfiniteJumps { get; private set; }

    public static ConfigEntry<bool> IsMarioMode { get; private set; }


    public static Dictionary<string, float> multiplierdict = new()
    {
        { "0", 0f }, { "0.5", 0.5f }, { "1", 1f }, { "1.5", 1.5f }, { "2", 2f }, { "2.5", 2.5f },
        { "3", 3f }, { "3.5", 3.5f }, { "4", 4f }, { "4.5", 4.5f }, { "5", 5f }, { "5.5", 5.5f },
        { "6", 6f }, { "6.5", 6.5f }, { "7", 7f }, { "7.5", 7.5f }, { "8", 8f }, { "8.5", 8.5f },
        { "9", 9f }, { "9.5", 9.5f }, { "10", 10f }, { "15", 15f }, { "20", 20f }, { "30", 30f },
        { "40", 40f }, { "50", 50f }, { "75", 75f }, { "100", 100f }, { "200", 200f },
        { "300", 300f }, { "400", 400f }, { "500", 500f }
    };

    public static void Init()
    {
        Category = ConfigSystem.CreateFileCategory("FrankyModMenu", "FrankyModMenu", "FrankyModMenu.cfg");
        Player = ConfigSystem.CreateFileCategory("Player", "Player", "FrankyModMenu.cfg");
        Game = ConfigSystem.CreateFileCategory("Game", "Game", "FrankyModMenu.cfg");
        Physics = ConfigSystem.CreateFileCategory("Physics", "Physics", "FrankyModMenu.cfg");
        Misc = ConfigSystem.CreateFileCategory("Misc", "Misc", "FrankyModMenu.cfg");

        string defaultMultiplierKey = "1"; // Set your desired default key here

        if (!multiplierdict.ContainsKey(defaultMultiplierKey))
        {
            RLog.Msg("Couldnt find value, shit's borked");
        }

        ShouldSaveSettings = Category.CreateEntry("SaveSettings", true, "Save Settings", "If enabled settings will be persistent across saves", false);
        BadPCDelay = Category.CreateEntry("BadPCDelay", 0f, "Slow Computer Delay(In Seconds)", "Optional delay in case of Slow Computer", false);
        BadPCDelay.SetRange(0f, 10f);
        GodMode = Player.CreateEntry("GodMode", false, "GodMode", "", false);
        UnBreakableArmor = Player.CreateEntry("UnbreakableArmor", false, "Unbreakable Armor", "", false);
        IsInfStamina = Player.CreateEntry("InfStamina", false, "Infinite Stamina", "", false);
        IsNoHungry = Player.CreateEntry("NoHungry", false, "Never Hungry", "", false);
        IsNoDehydration = Player.CreateEntry("NoDehydration", false, "Never Thirsty", "", false);
        IsNoSleep = Player.CreateEntry("NoSleep", false, "Never Tired", "", false);
        InfiniteBreath = Player.CreateEntry("InfiniteBreath", false, "Infinite Breath", "Lungs and Rebreather breath", false);
        IsNoFallDamage = Player.CreateEntry("NoFallDamage", false, "No Fall Damage", "", false);
        NoBurny = Player.CreateEntry("NoBurny", false, "No Fire Damage", "You will still see the burning FX", false);
        Invisibility = Player.CreateEntry("Invisibility", false, "Invisible", "If Enabled Ai wont notice you", false);
        InfiniteAmmo = Player.CreateEntry("InfiniteAmmo", false, "Infinite Ammo", "Does not work for regular bows", false);
        InfiniteArtifact = Player.CreateEntry("InfiniteArtifact", false, "Infinite Artifact", "Sets the power level to 999999, simulating infinite use. Re-enable if it runs out", false);
        DamageMultiplier = Player.CreateEntry("MultiplierMenu", defaultMultiplierKey, "Damage Multiplier", "Might have to be reapplied after you obtain a new weapon", false);
        DamageMultiplier.SetOptions(multiplierdict.Keys.ToArray());
        CreativeMode = Game.CreateEntry("CreativeMode", false, "CreativeMode", "Enable/Disable creative mode inventory option for build items", false);
        InfiniteBuildItems = Game.CreateEntry("InfiniteBuildItems", false, "Infinite Build Items", "Unlimited items for building, works great with Creative Mode", false);
        InstantBuild = Game.CreateEntry("InstantBuild", false, "Instant Blueprint Build", "Instantly build any Blueprint you put down", false);
        OneHitCutTrees = Game.CreateEntry("OneHitCutTrees", false, "One Hit Cut Trees", "Chop trees with one hit", false);
        InfiFire = Game.CreateEntry("InfiFire", false, "Infinite Fires", "", false);
        StopTime = Game.CreateEntry("StopTime", false, "Stop Time", "", false);
        TimeMultiplier = Game.CreateEntry("TimeMultiplier", defaultMultiplierKey, "Time Speed Multiplier", "Will not work if Stop Time is enabled", false);
        TimeMultiplier.SetOptions(multiplierdict.Keys.ToArray());
        WalkSpeed = Physics.CreateEntry("WalkSpeed", 2.6f, "WalkSpeed", "", false);
        WalkSpeed.SetRange(1f, 50f);
        RunSpeed = Physics.CreateEntry("RunSpeed", 5.4f, "RunSpeed", "", false);
        RunSpeed.SetRange(1f, 50f);
        SwimSpeed = Physics.CreateEntry("SwimSpeed", 3f, "SwimSpeed", "", false);
        SwimSpeed.SetRange(1f, 50f);
        JumpMultiplier = Physics.CreateEntry("JumpMultiplier", 0f, "Jump Height Multiplier", "", false);
        JumpMultiplier.SetRange(0f, 100f);
        IsInfiniteJumps = Physics.CreateEntry("InfiniteJumps", false, "Infinite Jumps", "", false);
        IsMarioMode = Misc.CreateEntry("MarioMode", false, "MarioMode", "Only works if Infinite Jumps is enabled", false);
    }

    public static void UpdateSettings()
    {
        
        var axelInstalled = ModTypeBase<SonsMod>.RegisteredMods.Any(x => x.ID == "AxelModMenu");

        if (!axelInstalled)
        {
            BasicToggleFunctions.GodMode(GodMode.Value);
            BasicToggleFunctions.InfStamina(IsInfStamina.Value);
            BasicToggleFunctions.NoHungry(IsNoHungry.Value);
            BasicToggleFunctions.NoDehydration(IsNoDehydration.Value);
            BasicToggleFunctions.NoSleep(IsNoSleep.Value);
            BasicToggleFunctions.NoFallDamage(IsNoFallDamage.Value);
            ValueFunctions.WalkSpeed(WalkSpeed.Value);
            ValueFunctions.RunSpeed(RunSpeed.Value);
            ValueFunctions.SwimSpeed(SwimSpeed.Value);
            ValueFunctions.JumpMultiplier(JumpMultiplier.Value);
            BasicToggleFunctions.StopTime(StopTime.Value);
            //RLog.Msg("Stoptime is on, not updating Time Multiplier");
            if (StopTime.Value == true)
            {
                //RLog.Msg("Stoptime is on, not updating Time Multiplier");
                return;
            }
            else
            {
                //RLog.Msg("Stoptime is off, updating Time Multiplier");
                if (float.TryParse(TimeMultiplier.Value, out float timeMultiplierValue))
                {
                    ValueFunctions.TimeMultiplier(timeMultiplierValue);
                }
            }
        }
                
        
        BasicToggleFunctions.InfiniteJumps(IsInfiniteJumps.Value);
        BasicToggleFunctions.MarioMode(IsMarioMode.Value);
        BasicToggleFunctions.InfiniteBreath(InfiniteBreath.Value);
        BasicToggleFunctions.Invisibility(Invisibility.Value);
        BasicToggleFunctions.InstantBuild(InstantBuild.Value);
        BasicToggleFunctions.OneHitCutTrees(OneHitCutTrees.Value);
        BasicToggleFunctions.CreativeMode(CreativeMode.Value);
        BasicToggleFunctions.InfiniteBuildItems(InfiniteBuildItems.Value);
        ToggleFunctions.UnbreakableArmour(UnBreakableArmor.Value);
        ToggleFunctions.SetInfiFires(InfiFire.Value);

        GameObject artifactHeldObject = GameObject.Find("LocalPlayer/PlayerAnimator/Root/Hips/Spine/Spine1/Spine2/RightShoulder/RightArm/RightForeArm/RightForeArmTwistNew1/RightForeArmTwistNew2/RightForeArmTwistNew3/RightHand/RightHandWeapon/rightHandHeld/NEWITEMS/ArtifactHeld");
        if (InfiniteArtifact.Value == true)
        {

            if (artifactHeldObject != null && _hasBeenSet == false)
            {
                _hasBeenSet = true;
                ToggleFunctions.InfiniteArtifact(InfiniteArtifact.Value);
            }
            else if (artifactHeldObject != null && _hasBeenSet == true)
            {

            }
            else if (artifactHeldObject == null && _hasBeenSet == false)
            {
                InfiniteArtifact.Value = InfiniteArtifact.DefaultValue;
                RLog.Error("Artifact needs to be equipped to toggle InfiniteArtifact.");
                SonsTools.ShowMessage("Artifact needs to be equipped to toggle InfiniteArtifact", 5f);
            }
        }
        else
        {
            if (artifactHeldObject != null)
            {
                if (_hasBeenSet == true)
                {
                    ToggleFunctions.InfiniteArtifact(InfiniteArtifact.Value);
                }
                else
                {
                    RLog.Error("Artifact needs to be equipped to toggle InfiniteArtifact.");
                    SonsTools.ShowMessage("Artifact needs to be equipped to toggle InfiniteArtifact", 5f);
                }
            }
            else
            {
                if (_hasBeenSet == false)
                {
                    InfiniteArtifact.Value = InfiniteArtifact.DefaultValue;
                    //RLog.Error("Artifact needs to be equipped to toggle InfiniteArtifact.");
                    //SonsTools.ShowMessage("Artifact needs to be equipped to toggle InfiniteArtifact", 5f);
                }
                else
                {
                    ToggleFunctions.InfiniteArtifact(InfiniteArtifact.Value);
                }
                //RLog.Msg("Artifact needs to be equipped to toggle InfiniteArtifact.");
            }
        }

        if (float.TryParse(DamageMultiplier.Value, out float damageMultiplierValue))
        {
            ValueFunctions.DamageMultiplier(damageMultiplierValue);
        }
    }

    public static void UpdateOrRestoreDefaults()
    {
        if (Config.ShouldSaveSettings.Value == true)
        {
            //RLog.Msg("RestoreDefaults, ShouldSaveSettings is true, apply stored settings");
            InfiniteArtifact.Value = InfiniteArtifact.DefaultValue;
            UpdateSettings();
            return;
        }
        //RLog.Msg("RestoreDefaults, ShouldSaveSettings is false, reset stuff");
        GodMode.Value = GodMode.DefaultValue;
        UnBreakableArmor.Value = UnBreakableArmor.DefaultValue;
        IsInfStamina.Value = IsInfStamina.DefaultValue;
        InfiniteBreath.Value = InfiniteBreath.DefaultValue;
        IsNoHungry.Value = IsNoHungry.DefaultValue;
        IsNoDehydration.Value = IsNoDehydration.DefaultValue;
        IsNoSleep.Value = IsNoSleep.DefaultValue;
        IsNoFallDamage.Value = IsNoFallDamage.DefaultValue;
        DamageMultiplier.Value = DamageMultiplier.DefaultValue;
        TimeMultiplier.Value = TimeMultiplier.DefaultValue;
        WalkSpeed.Value = WalkSpeed.DefaultValue;
        RunSpeed.Value = RunSpeed.DefaultValue;
        SwimSpeed.Value = SwimSpeed.DefaultValue;
        StopTime.Value = StopTime.DefaultValue;
        JumpMultiplier.Value = JumpMultiplier.DefaultValue;
        IsInfiniteJumps.Value = IsInfiniteJumps.DefaultValue;
        IsMarioMode.Value = IsMarioMode.DefaultValue;
        Invisibility.Value = Invisibility.DefaultValue;
        NoBurny.Value = NoBurny.DefaultValue;
        InstantBuild.Value = InstantBuild.DefaultValue;
        InfiniteAmmo.Value = InfiniteAmmo.DefaultValue;
        NoBurny.Value = NoBurny.DefaultValue;
        InfiFire.Value = InfiFire.DefaultValue;
        InfiniteArtifact.Value = InfiniteArtifact.DefaultValue;
        CreativeMode.Value = CreativeMode.DefaultValue;
        OneHitCutTrees.Value = OneHitCutTrees.DefaultValue;
        InfiniteBuildItems.Value = InfiniteBuildItems.DefaultValue;
    }
}