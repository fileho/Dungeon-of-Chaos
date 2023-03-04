using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class PlayerPrefsManager
{
    public static float MasterVolume
    {
        get {
            return PlayerPrefs.GetFloat(Constants.MASTER_VOLUME, 100f);
        }
        set {
            PlayerPrefs.SetFloat(Constants.MASTER_VOLUME, value);
        }
    }

    public static float SFXVolume
    {
        get {
            return PlayerPrefs.GetFloat(Constants.SFX_VOLUME, 100f);
        }
        set {
            PlayerPrefs.SetFloat(Constants.SFX_VOLUME, value);
        }
    }

    public static float SoundTrackVolume
    {
        get {
            return PlayerPrefs.GetFloat(Constants.SOUNDTRACK_VOLUME, 100f);
        }
        set {
            PlayerPrefs.SetFloat(Constants.SOUNDTRACK_VOLUME, value);
        }
    }

    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
}
