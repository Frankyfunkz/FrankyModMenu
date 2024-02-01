using RedLoader;
using RedLoader.Preferences;
using SonsSdk;
using TheForest.Utils;
using SonsGameManager;
using Sons.Cutscenes;

namespace FrankyModMenu;

public static class Config
{
    public static ConfigCategory Category { get; private set; }
    //public static KeybindConfigEntry ToggleKey { get; private set; }
    public static ConfigEntry<bool> GodMode { get; private set; }
    public static ConfigEntry<bool> UnBreakableArmor { get; private set; }
    public static ConfigEntry<bool> IsInfStamina { get; private set; }
    public static ConfigEntry<bool> IsNoHungry { get; private set; }
    public static ConfigEntry<bool> IsNoDehydration { get; private set; }
    public static ConfigEntry<bool> IsNoSleep { get; private set; }
    public static ConfigEntry<bool> IsNoFallDamage { get; private set; }
    public static ConfigEntry<bool> IsInfiniteJumps { get; private set; }
    public static ConfigEntry<bool> IsMarioMode { get; private set; }
    public static ConfigEntry<bool> StopTime { get; private set; }
    public static ConfigEntry<float> WalkSpeed { get; private set; }
    public static ConfigEntry<float> RunSpeed { get; private set; }
    public static ConfigEntry<float> SwimSpeed { get; private set; }
    public static ConfigEntry<float> JumpMultiplier { get; private set; }
    public static ConfigEntry<string> DamageMultiplier { get; private set; }
    public static ConfigEntry<string> TimeMultiplier { get; private set; }
    public static ConfigEntry<bool> ShouldSaveSettings { get; private set; }
  
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

        string defaultMultiplierKey = "1"; // Set your desired default key here

        if (!multiplierdict.ContainsKey(defaultMultiplierKey))
        {
            RLog.Msg("Couldnt find value, shit's borked");
        }


        //ToggleKey = Category.CreateKeybindEntry("toggle_key", EInputKey.f11, "Toggle Menu", "The key that toggles.");
        GodMode = Category.CreateEntry("GodMode", false, "GodMode", "", false);
        UnBreakableArmor = Category.CreateEntry("UnbreakableArmor", false, "Unbreakable Armor", "", false);
        IsInfStamina = Category.CreateEntry("InfStamina", false, "Infinite Stamina", "", false);
        IsNoHungry = Category.CreateEntry("NoHungry", false, "Never Hungry", "", false);
        IsNoDehydration = Category.CreateEntry("NoDehydration", false, "Never Thirsty", "", false);
        IsNoSleep = Category.CreateEntry("NoSleep", false, "Never Tired", "", false);
        IsNoFallDamage = Category.CreateEntry("NoFallDamage", false, "No Fall Damage", "", false);
        DamageMultiplier = Category.CreateEntry("MultiplierMenu", defaultMultiplierKey, "Damage Multiplier", "", false);
        DamageMultiplier.SetOptions(multiplierdict.Keys.ToArray());
        TimeMultiplier = Category.CreateEntry("TimeMultiplier", defaultMultiplierKey, "Time Speed Multiplier", "", false);
        TimeMultiplier.SetOptions(multiplierdict.Keys.ToArray());
        WalkSpeed = Category.CreateEntry("WalkSpeed", 2.6f, "WalkSpeed", "", false);
        WalkSpeed.SetRange(1f, 50f);
        RunSpeed = Category.CreateEntry("RunSpeed", 5.4f, "RunSpeed", "", false);
        RunSpeed.SetRange(1f, 50f);
        SwimSpeed = Category.CreateEntry("SwimSpeed", 3f, "SwimSpeed", "", false);
        SwimSpeed.SetRange(1f, 50f);
        StopTime = Category.CreateEntry("StopTime", false, "Stop Time", "", false);
        JumpMultiplier = Category.CreateEntry("JumpMultiplier", 0f, "Jump Multiplier", "", false);
        JumpMultiplier.SetRange(0f, 100f);
        IsInfiniteJumps = Category.CreateEntry("InfiniteJumps", false, "Infinite Jumps", "", false);
        IsMarioMode = Category.CreateEntry("MarioMode", false, "MarioMode", "", false);
        ShouldSaveSettings = Category.CreateEntry("SaveSettings", true, "Save Settings on load", "If enabled saving settings across saves will be persistent", false);
    }

    public static void UpdateSettings()
    {
        if (!LocalPlayer.IsInWorld)
        {
            SonsTools.ShowMessageBox("Oops", "Cant change settings for FrankyModMenu while not ingame");
            //SonsTools.ShowMessage("Cant change settings for FrankyModMenu while not In-Game", 3f);
            RLog.Error("Cant change settings for FrankyModMenu while not In-Game");
            return;
        }
        if (CutsceneManager.GetActiveCutScene != null)
        {
            FrankyModMenu.WaitForCutscene().RunCoro();
        }
        ToggleFunctions.GodMode(GodMode.Value);
        ToggleFunctions.UnbreakableArmour(UnBreakableArmor.Value);
        ToggleFunctions.InfStamina(IsInfStamina.Value);
        ToggleFunctions.NoHungry(IsNoHungry.Value);
        ToggleFunctions.NoDehydration(IsNoDehydration.Value);
        ToggleFunctions.NoSleep(IsNoSleep.Value);
        ToggleFunctions.NoFallDamage(IsNoFallDamage.Value);
        ToggleFunctions.StopTime(StopTime.Value);
        ToggleFunctions.InfiniteJumps(IsInfiniteJumps.Value);
        ToggleFunctions.MarioMode(IsMarioMode.Value);
        ValueFunctions.WalkSpeed(WalkSpeed.Value);
        ValueFunctions.RunSpeed(RunSpeed.Value);
        ValueFunctions.SwimSpeed(SwimSpeed.Value);
        ValueFunctions.JumpMultiplier(JumpMultiplier.Value);
        if (float.TryParse(DamageMultiplier.Value, out float damageMultiplierValue))
        {
            ValueFunctions.DamageMultiplier(damageMultiplierValue);
        }
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
    public static void UpdateOrRestoreDefaults()
    {
        if (Config.ShouldSaveSettings.Value == true)
        {
            RLog.Msg("RestoreDefaults, ShouldSaveSettings is true, apply stored settings");
            UpdateSettings();
            return;
        }
        RLog.Msg("RestoreDefaults, ShouldSaveSettings is false, reset stuff");
        GodMode.Value = GodMode.DefaultValue;
        UnBreakableArmor.Value = UnBreakableArmor.DefaultValue;
        IsInfStamina.Value = IsInfStamina.DefaultValue;
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

    }
}