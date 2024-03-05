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
using HarmonyLib;
using Sons.Weapon;
using Sons.Gameplay;
using Construction;
using Sons.Gui;


namespace FrankyModMenu;

public class FrankyModMenu : SonsMod
{
    public delegate void CutsceneCallback();


    public static event CutsceneCallback OnCutsceneComplete;
    public FrankyModMenu()
    {
        OnCutsceneComplete += HandleCutsceneComplete;
        OnWorldUpdatedCallback = OnWorldUpdate;
        HarmonyPatchAll = true;
    }


    [HarmonyPatch(typeof(RangedWeaponController), "OnAmmoSpent")]
    public class AmmoPatches
    {
        [HarmonyPrefix]
        public static bool PrefixOnAmmoSpent()
        {
            if (InfiniteAmmo())
            { // Returning false here prevents the original method from executing
                return false;
            }
            return true;
        }

        private static bool InfiniteAmmo()
        {
            return Config.InfiniteAmmo.Value;

        }
    }

    [HarmonyPatch(typeof(PlayerStats), "HitFire")]
    public class BurningPatch
    {
        [HarmonyPrefix]
        public static bool PrefixBurning()
        {
            if (NoBurny())
            { // Returning false here prevents the original method from executing
                return false;
            }
            return true;
        }

        private static bool NoBurny()
        {

            return Config.NoBurny.Value;
        }
    }

    private bool hasJumpSoundPlayed = false;
    private bool isFalling = false;
    private bool AlternateJumpSound = false;
    public static float baseSpeedMultiplier = TimeOfDayHolder.GetBaseSpeedMultiplier();
    public static bool _hasControl = false;
    public static bool _firstStart = true;
    public static bool _returnedToTitle = false;

    protected override void OnInitializeMod()
    {
        Config.Init();
    }

    protected override void OnSdkInitialized()
    {
        FrankyModMenuUi.Create();
        SoundTools.RegisterSound("JumpSound1", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioJump1.mp3"));
        SoundTools.RegisterSound("JumpSound2", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioJump2.mp3"));
        SoundTools.RegisterSound("FallSound", Path.Combine(LoaderEnvironment.ModsDirectory, @"FrankyModMenu\MarioFalling.mp3"));
    }

    protected override void OnGameStart()
    {
        _firstStart = false;
        _returnedToTitle = false;

        if (CutsceneManager.GetActiveCutScene != null)
        {
            //RLog.Msg("OnGameStart in Cutscene, doing WaitForCutscene coro");
            WaitForCutscene().RunCoro();
        }
        else
        {
            //RLog.Msg("OnGameStart, Not in Cutscene, doing only waitforplayer coro");
            WaitForLocalPlayerFirst().RunCoro();
        }

    }

    protected override void OnSonsSceneInitialized(ESonsScene sonsScene)
    {
        if (sonsScene == ESonsScene.Title)
        {
            if (!_firstStart)
            {
                //_hasControl = false;
                _returnedToTitle = true;
                //RLog.Msg("In Title Screen, not first start, set hascontrol false, set _returnedToTitle " + _returnedToTitle);
                //RLog.Msg("Returned to title, stopping firecheck coro");
                ToggleFunctions.fireCoroShouldRun = false;
                return;
            }
            else
            {
                return;
            }
        }
    }
    
    private void HandleCutsceneComplete()
    {
        //RLog.Msg("HandleCutscene, waitforcutscene coro Complete, doing waitforlocalplayerfirst coro ");
        WaitForLocalPlayerFirst().RunCoro();
    }

    private void OnSettingsUiClosed()
    {
        if (!LocalPlayer._instance && _returnedToTitle)
        {
            SonsTools.ShowMessageBox("Oops", "Cant change settings for FrankyModMenu while not In-Game");
            //SonsTools.ShowMessage("Cant change settings for FrankyModMenu while not In-Game", 3f);
            RLog.Error("Cant change settings for FrankyModMenu while not In-Game");
            return;
        }
        //Config.UpdateSettings();
        ClosedPauseMenu().RunCoro();
    }

    public static IEnumerator ClosedPauseMenu()
    {
        static bool NotInPauseMenu()
        {
            //RLog.Msg("waiting until pausemenu isactive returns false");
            return PauseMenu.IsActive == false;
        }
        yield return CustomWaitUntil.WaitUntil(new Func<bool>(NotInPauseMenu));
        //RLog.Msg("No pause menu instance found, updating settings");
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
        yield return CustomWaitUntil.WaitUntil(new Func<bool>(PlayerExists));
        //RLog.Msg("LocalPlayer._instance is not null. Continuing...");


        if (_hasControl == false)
        {
            //RLog.Msg("waiting for terrainOrflatcontact true, _hasControl is " + _hasControl );
            static bool PlayerHasControl()
            {
                return LocalPlayer.FpCharacter._terrainOrFlatContact == true;
            }
            //Wait until player has control
            yield return CustomWaitUntil.WaitUntil(new Func<bool>(PlayerHasControl));
            _hasControl = true;
            //RLog.Msg("terrainOrflatcontact true, set _hasControl to " + _hasControl);
            SettingsRegistry.CreateSettings(this, null, typeof(Config), callback: OnSettingsUiClosed);
            Config.UpdateOrRestoreDefaults();
            _firstStart = false;
        }
        else
        {
            //RLog.Msg("_hasControl is" + _hasControl);
            SettingsRegistry.CreateSettings(this, null, typeof(Config), callback: OnSettingsUiClosed);
            Config.UpdateOrRestoreDefaults();
            _firstStart = false;

        }
    }


    public static IEnumerator WaitForCutscene()
    {
        static bool IsNotInCutscene()
        {
            //RLog.Msg("Checking if player is in cutscene");
            return CutsceneManager._instance._activeCutscene == null;
        }
        yield return CustomWaitUntil.WaitUntil(new Func<bool>(IsNotInCutscene));
        //RLog.Msg("_activeCutscene is null. Continuing...");
        OnCutsceneComplete?.Invoke();
    }

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