using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class PlayerPrefsManager
{
    public static float MasterVolume
    {
        get { return PlayerPrefs.GetFloat(Constants.MASTER_VOLUME, 100f); }
        set { PlayerPrefs.SetFloat(Constants.MASTER_VOLUME, value); }
    }

    public static float SFXVolume
    {
        get { return PlayerPrefs.GetFloat(Constants.SFX_VOLUME, 100f); }
        set { PlayerPrefs.SetFloat(Constants.SFX_VOLUME, value); }
    }

    public static float SoundTrackVolume
    {
        get { return PlayerPrefs.GetFloat(Constants.SOUNDTRACK_VOLUME, 100f); }
        set { PlayerPrefs.SetFloat(Constants.SOUNDTRACK_VOLUME, value); }
    }

    public static float Brightness
    {
        get { return PlayerPrefs.GetFloat(Constants.BRIGHTNESS, 100f); }
        set { PlayerPrefs.SetFloat(Constants.BRIGHTNESS, value); }
    }


    public static void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }


    public static Dictionary<string, bool> Tutorial = new Dictionary<string, bool>() {
        {"Movement", Movement},
        {"Dash", Dash},
        {"Attack", Attack},
        {"Skills", Skills},
        {"EnemyAttack", EnemyAttack},
        {"CheckPoint", CheckPoint},
        {"MapUnlock", MapUnlock},
    };


    static bool Movement
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_MOVEMENT, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_MOVEMENT, value ? 1 : 0); }
    }

    static bool Dash
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_DASH, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_DASH, value ? 1 : 0); }
    }

    static bool Attack
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_ATTACK, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_ATTACK, value ? 1 : 0); }
    }

    static bool Skills
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_SKILLS, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_SKILLS, value ? 1 : 0); }
    }

    static bool EnemyAttack
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_ENEMY_ATTACK, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_ENEMY_ATTACK, value ? 1 : 0); }
    }

    static bool CheckPoint
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_CHECKPOINT, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_CHECKPOINT, value ? 1 : 0); }
    }

    static bool MapUnlock
    {
        get { return PlayerPrefs.GetInt(Constants.TUTORIAL_MAP_UNLOCK, 0) == 1; }
        set { PlayerPrefs.SetInt(Constants.TUTORIAL_MAP_UNLOCK, value ? 1 : 0); }
    }



}