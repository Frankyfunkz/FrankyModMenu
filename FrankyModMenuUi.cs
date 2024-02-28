namespace FrankyModMenu;

using SonsSdk;
using SUI;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static SUI.SUI;
using RedLoader;
using UnityEngine.Playables;
using TheForest.Utils;

public class FrankyModMenuUi
{
    /*
    public static readonly string PanelId = "FrankyModMenu";
    public static SContainerOptions Panel;
    static SScrollContainerOptions _mainContainer;

    static string defaultMultiplierKey = "1";
    public static float initialValue = float.Parse(defaultMultiplierKey);
    static float _padding = 100f;
    static string _divsColor = "#7F1727";
    static Observable<bool> _showPanel = new(false);
    */
    public static void Create()
    {
        /*
        Panel = RegisterNewPanel("FrankyModMenu", true).Background(new Color(0.03f, 0.03f, 0.03f, 0.98f), EBackground.Sons)
             .Margin(200, 60)
             .Vertical(0, "EC")
             .OverrideSorting(100)
             .BindVisibility(_showPanel)
             .Active(false);

        var scrollBar = SDiv.FlexHeight(1);
        Panel.Add(scrollBar);
        _mainContainer = SScrollContainer
        .Dock(EDockType.Fill)
        .Background(Color.black.WithAlpha(0), EBackground.None)
        .Size(-20, -20)
        .As<SScrollContainerOptions>();
        _mainContainer.ContainerObject.Spacing(10);
        _mainContainer.ContainerObject.PaddingHorizontal(10);
        scrollBar.Add(_mainContainer);

        _mainContainer
            .Add(NewContainer(SLabelDivider.RichText($"<color={_divsColor}>FrankyModMenu</color>")))
            .Add(NewContainer(SToggle.Text("Save settings").Value(Config.ShouldSaveSettings.Value)))

            .Add(NewContainer(SLabelDivider.RichText($"<color={_divsColor}>Player</color>")))
            .Add(NewContainer(SToggle.Text("God mode").Value(Config.GodMode.Value).Notify(ToggleFunctions.GodMode)))
            .Add(NewContainer(SToggle.Text("Unbreakable armor").Value(Config.UnBreakableArmor.Value).Notify(ToggleFunctions.UnbreakableArmour)))
            .Add(NewContainer(SToggle.Text("Infinite stamina").Value(Config.IsInfStamina.Value).Notify(ToggleFunctions.InfStamina)))
            .Add(NewContainer(SToggle.Text("Never hungry").Value(Config.IsNoHungry.Value).Notify(ToggleFunctions.NoHungry)))
            .Add(NewContainer(SToggle.Text("Never thirsty").Value(Config.IsNoDehydration.Value).Notify(ToggleFunctions.NoDehydration)))
            .Add(NewContainer(SToggle.Text("Never tired").Value(Config.IsNoSleep.Value).Notify(ToggleFunctions.NoSleep)))
            .Add(NewContainer(SToggle.Text("Infinite breath").Value(Config.InfiniteBreath.Value).Notify(ToggleFunctions.InfiniteBreath)))
            .Add(NewContainer(SToggle.Text("No fall damage").Value(Config.IsNoFallDamage.Value).Notify(ToggleFunctions.NoFallDamage)))
            .Add(NewContainer(SToggle.Text("No fire damage").Value(Config.NoBurny.Value)))
            .Add(NewContainer(SToggle.Text("Invisible").Value(Config.Invisibility.Value).Notify(ToggleFunctions.Invisibility)))
            .Add(NewContainer(SToggle.Text("Infinite ammo").Value(Config.InfiniteAmmo.Value)))
            .Add(NewContainer(SLabel.RichText($"<color={_divsColor}>Artifact needs to be equipped to toggle Infinite Artifact On/Off</color>").Alignment(TMPro.TextAlignmentOptions.Left)))
            .Add(NewContainer(SToggle.Text("Infinite artifact").Value(Config.InfiniteArtifact.Value).Notify(ToggleFunctions.InfiniteArtifact)))
            .Add(CreateDamageMultiplierSlider(initialValue))

            .Add(NewContainer(SLabelDivider.RichText($"<color={_divsColor}>Game</color>")))
            .Add(NewContainer(SToggle.Text("Creative mode").Value(Config.CreativeMode.Value).Notify(ToggleFunctions.CreativeMode)))
            .Add(NewContainer(SToggle.Text("Instant build").Value(Config.InstantBuild.Value).Notify(ToggleFunctions.InstantBuild)))
            .Add(NewContainer(SToggle.Text("Infinite fires").Value(Config.InfiFire.Value).Notify(ToggleFunctions.SetInfiFires)))
            .Add(NewContainer(SToggle.Text("Stop time").Value(Config.StopTime.Value).Notify(ToggleFunctions.StopTime)))
            .Add(CreateTimeMultiplierSlider(initialValue))

            .Add(NewContainer(SLabelDivider.RichText($"<color={_divsColor}>Physics</color>")))
            .Add(WalkSpeedSlider(defaultWalk))
            .Add(RunSpeedSlider(defaultRun))
            .Add(SwimSpeedSlider(defaultSwim))
            .Add(JumpHeightSlider(defaultJump))
            .Add(NewContainer(SToggle.Text("Infinite jumps").Value(Config.IsInfiniteJumps.Value).Notify(ToggleFunctions.InfiniteJumps)))
            .Add(NewContainer(SSlider.Text("Walk speed").Range(1f, 50f).Value(Config.WalkSpeed.Value).Notify(ValueFunctions.WalkSpeed)))
            .Add(NewContainer(SSlider.Text("Run speed").Range(1f, 50f).Value(Config.RunSpeed.Value).Notify(ValueFunctions.RunSpeed)))
            .Add(NewContainer(SSlider.Text("Swim speed").Range(1f, 50f).Value(Config.SwimSpeed.Value).Notify(ValueFunctions.SwimSpeed)))
            .Add(NewContainer(SSlider.Text("Jump height multiplier").Range(0f, 100f).Value(Config.JumpMultiplier.Value).Notify(ValueFunctions.JumpMultiplier)))
            

            .Add(NewContainer(SLabelDivider.RichText($"<color={_divsColor}>Misc</color>")))
            .Add(NewContainer(SToggle.Text("Mario Mode (fun mode for Infinite Jumps)").Value(Config.IsMarioMode.Value).Notify(ToggleFunctions.MarioMode)));

        var bottomContainer = SContainer.Background(Color.black.WithAlpha(0), EBackground.None).Horizontal(10, "XX")
            - SBgButton.Text("Back").Background(EBackground.RoundedStandard).Color(new Color(0.60f, 0.00f, 0.05f)).Width(180).Height(50)
            .Notify(ToggleModMenu)

            - SBgButton.Text("Revert").Background(EBackground.RoundedStandard).Color(new Color(0.47f, 0.42f, 0.30f)).Width(180).Height(50)
            .Notify(() => { Config.RevertToDefault(); });

        Panel.Add(bottomContainer);
        Panel.Padding(_padding);
        */
    }
    /*
    public static float defaultWalk = 2.6f;
    public static float defaultRun = 5.4f;
    public static float defaultSwim = 3f;
    public static float defaultJump = 0f;
    public static SContainerOptions WalkSpeedSlider(float defaultWalk)
    {
        // Initialize the slider with the initial value from the Config
        //float defaultValue = 2.6f;

        // Create the slider
        SSliderOptions slider = SSlider.Text("Walk Speed")
                                .Value(defaultWalk) // Set default value
                                .Min(1) // Set minimum value
                                .Max(50) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.WalkSpeed.Value = newValue;

                                    // Notify the ValueFunctions.DamageMultiplier method
                                    ValueFunctions.WalkSpeed(newValue);
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }
    public static SContainerOptions RunSpeedSlider(float defaultRun)
    {
        // Initialize the slider with the initial value from the Config
        //float defaultValue = 2.6f;

        // Create the slider
        SSliderOptions slider = SSlider.Text("Run Speed")
                                .Value(defaultRun) // Set default value
                                .Min(1) // Set minimum value
                                .Max(50) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.WalkSpeed.Value = newValue;

                                    // Notify the ValueFunctions.DamageMultiplier method
                                    ValueFunctions.WalkSpeed(newValue);
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }

    public static SContainerOptions SwimSpeedSlider(float defaultSwim)
    {
        // Initialize the slider with the initial value from the Config
        //float defaultValue = 2.6f;

        // Create the slider
        SSliderOptions slider = SSlider.Text("Swim Speed")
                                .Value(defaultSwim) // Set default value
                                .Min(1) // Set minimum value
                                .Max(50) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.SwimSpeed.Value = newValue;

                                    // Notify the ValueFunctions.DamageMultiplier method
                                    ValueFunctions.SwimSpeed(newValue);
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }
    public static SContainerOptions JumpHeightSlider(float defaultJump)
    {
        // Initialize the slider with the initial value from the Config
        //float defaultValue = 2.6f;

        // Create the slider
        SSliderOptions slider = SSlider.Text("Jump Height Multiplier")
                                .Value(defaultJump) // Set default value
                                .Min(0) // Set minimum value
                                .Max(100) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.JumpMultiplier.Value = newValue;

                                    // Notify the ValueFunctions.DamageMultiplier method
                                    ValueFunctions.JumpMultiplier(newValue);
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }
    public static SContainerOptions CreateDamageMultiplierSlider(float initialValue)
    {
        // Initialize the slider with the initial value from the Config
        //float initialValue = float.Parse(defaultMultiplierKey);

        // Create the slider
        SSliderOptions slider = SSlider.Text("Damage multiplier")
                                .Value(initialValue) // Set initial value
                                .Min(0) // Set minimum value
                                .Max(500) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.DamageMultiplier.Value = newValue.ToString();

                                    // Notify the ValueFunctions.DamageMultiplier method
                                    ValueFunctions.DamageMultiplier(newValue);
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }

    public static SContainerOptions CreateTimeMultiplierSlider(float initialValue)
    {
        // Initialize the slider with the initial value from the Config
        //float initialValue = float.Parse(defaultMultiplierKey);

        // Create the slider
        SSliderOptions slider = SSlider.Text("Time Speed Multiplier")
                                .Value(initialValue) // Set initial value
                                .Min(0) // Set minimum value
                                .Max(500) // Set maximum value
                                .Notify((newValue) =>
                                {
                                    // Update the Config value
                                    Config.TimeMultiplier.Value = newValue.ToString();

                                    // Notify the ValueFunctions.TimeMultiplier method
                                    if (!Config.StopTime.Value) // Only update time multiplier if stop time is not enabled
                                    {
                                        ValueFunctions.TimeMultiplier(newValue);
                                    }
                                });

        // Return the container containing the slider
        return NewContainer(slider);
    }

    static SContainerOptions NewContainer(SUiElement sUiElement, string tooltip = "")
    {
        return SContainer.Horizontal(0, "EE").PHeight(50).Background(Color.black.WithAlpha(0), EBackground.None).Tooltip(tooltip)
            - sUiElement;
    }

    public static void ToggleModMenu()
    {

        if (!LocalPlayer._instance && FrankyModMenu._returnedToTitle)
        {
            _showPanel.Value = false;
            SonsTools.ShowMessageBox("Oops", "Cant change settings for FrankyModMenu while not In-Game");
            //SonsTools.ShowMessage("Cant change settings for FrankyModMenu while not In-Game", 3f);
            RLog.Error("Cant change settings for FrankyModMenu while not In-Game");
            return;
        }
        _showPanel.Value = !_showPanel.Value;
        TogglePanel(PanelId, _showPanel.Value);

        
    }
    */
}
