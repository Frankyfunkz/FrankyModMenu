using RedLoader;
using RedLoader.Utils;
using Sons.Environment;
using SonsGameManager;
using SonsSdk;
using SUI;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.Windows;
using System.Collections;
using Input = UnityEngine.Input;
using Sons.Input;
using Sons.Cutscenes;


namespace FrankyModMenu;

public class FrankyModMenu : SonsMod
{
    public FrankyModMenu()
    {

        OnWorldUpdatedCallback = OnWorldUpdate;
        OnUpdateCallback = OnUpdate;
        HarmonyPatchAll = true;
    }
    private bool hasJumpSoundPlayed = false;
    private bool isFalling = false;
    private bool AlternateJumpSound = false;
    public static float baseSpeedMultiplier = TimeOfDayHolder.GetBaseSpeedMultiplier();

    protected override void OnInitializeMod()
    {
        // Do your early mod initialization which doesn't involve game or sdk references here
        Config.Init();
    }


    protected override void OnSdkInitialized()
    {
        // Do your mod initialization which involves game or sdk references here
        // This is for stuff like UI creation, event registration etc.
        FrankyModMenuUi.Create();
        SoundTools.RegisterSound("JumpSound1", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioJump1.mp3"));
        SoundTools.RegisterSound("JumpSound2", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioJump2.mp3"));
        SoundTools.RegisterSound("FallSound", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioFalling.mp3"));
    }

    public void OnUpdate()
    {
        /*
        if (Input.GetKeyDown(Config.ToggleKey.Value))
        {
            //RLog.Msg("ToggleKey pressed");
            FrankyModMenuToggle();
        }
        */
    }
    protected override void OnGameStart()
    {
        if (CutsceneManager.GetActiveCutScene != null)
        {
            WaitForCutscene().RunCoro();
            WaitForLocalPlayerFirst().RunCoro();
        }
        else
        {
            //RLog.Msg("OnGameStart, Not in Cutscene, doing waitforplayer coro");
            WaitForLocalPlayerFirst().RunCoro();
        }
    }
    private void OnSettingsUiClosed()
    {
        Config.UpdateSettings();
    }
    IEnumerator WaitForLocalPlayerFirst()
    {
        static bool PlayerExists()
        {
            //RLog.Msg("waiting for LocalPlayer._instance...");
            return LocalPlayer._instance != null;
        }
        //Wait until LocalPlayer._instance is not null
        yield return new WaitUntil(new Func<bool>(PlayerExists));
        //RLog.Msg("LocalPlayer._instance is not null. Continuing...");

        yield return new WaitForSeconds(5f);
        //RLog.Msg("Waited for 5 seconds to make sure player has control");
        SettingsRegistry.CreateSettings(this, null, typeof(Config), callback: OnSettingsUiClosed);
        if (Config.ShouldSaveSettings.Value == false)
        {
            Config.RestoreDefaults();
        }
    }

    public IEnumerator WaitForLocalPlayer()
    {
        static bool PlayerExists()
        {
            //RLog.Msg("waiting for LocalPlayer._instance...");
            return LocalPlayer._instance != null;
        }
        //Wait until LocalPlayer._instance is not null
        yield return new WaitUntil(new Func<bool>(PlayerExists));
        //RLog.Msg("LocalPlayer._instance is not null. Continuing...");
    }

    public static IEnumerator WaitForCutscene()
    {
        static bool IsNotInCutscene()
        {
            //RLog.Msg("Checking if player is in cutscene");
            return CutsceneManager._instance._activeCutscene == null;
        }
        yield return new WaitUntil(new Func<bool>(IsNotInCutscene));
        //RLog.Msg("_activeCutscene is null. Continuing...");
    }
    /*
    private static bool IsMenuToggled = false;
    public static void FrankyModMenuToggle()
    {
        if (!IsMenuToggled)
        {
            RLog.Msg("IsMenuOpen is false - Opening menu and unlocking mouse");
            ModSettingsUi.Open("FrankyModMenu");
            SonsTools.MenuMode(true);
            IsMenuToggled = true;
        }
        else
        {
            RLog.Msg("IsMenuOpen is true - closing menu and locking mouse");
            ModSettingsUi.Close();
            SonsTools.MenuMode(false);
            IsMenuToggled = false;
        }
    }
    */
    public void OnWorldUpdate()
    {
        if (Config.IsInfiniteJumps.Value)
        {
            if (InputSystem.InputMapping.@default.Jump.IsPressed() && !LocalPlayer.FpCharacter._isGrounded)
            {
                // Check if the sound conditions are met
                if (!hasJumpSoundPlayed && Config.IsMarioMode.Value)
                {

                    string jumpSoundName = AlternateJumpSound ? "JumpSound2" : "JumpSound1";

                    // Play jump sound
                    SoundTools.PlaySound(jumpSoundName);

                    // Toggle bool for the next jump
                    AlternateJumpSound = !AlternateJumpSound;

                    // Set the flag to true so the sound won't be played again until the conditions change
                    hasJumpSoundPlayed = true;
                }

                // Execute Jump logic
                LocalPlayer.FpCharacter.OnJumpInput(true);
                LocalPlayer.FpCharacter.fallShakeBlock = true;
            }
            else
            {
                // Reset flag when the conditions are not met
                hasJumpSoundPlayed = false;
            }
        }
        if (Config.IsMarioMode.Value)
        {
            if (LocalPlayer.FpCharacter.IsJumping && LocalPlayer.FpCharacter._velocity.y < -10f)
            {
                // Check if falling is not already in progress
                if (!isFalling)
                {
                    // Set the flag to true to indicate falling has started
                    isFalling = true;

                    // Start playing the fall sound on a loop until the conditions change
                    PlayFallSoundLoop().RunCoro();
                }
            }
        }
        else
        {
            // Reset the falling flag when the conditions are not met
            isFalling = false;
        }

        IEnumerator PlayFallSoundLoop()
        {
            // Adjust the delay value based on the duration of the "FallSound"
            float soundDuration = 3.0f;

            while (LocalPlayer.FpCharacter._velocity.y < -10f)
            {
                // Play the fall sound
                SoundTools.PlaySound("FallSound");

                // Wait for the full sound duration before playing it again
                yield return new WaitForSeconds(soundDuration);
            }

            // Falling has stopped, reset the falling flag
            isFalling = false;
        }
    }
}