using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Data about sound we would like to play
/// </summary>
[System.Serializable]
public class SoundSettings
{
    [SerializeField]
    private SoundCategories.SoundCategory soundCategory;
    // The index of the sound, stored as int since it can be from different enums and every enum to convertible to int
    [SerializeField]
    private int sound;

    // normalized [0,1]
    [SerializeField]
    private float volume = 1;
    [SerializeField]
    private float pitch = 1;
    [SerializeField]
    private float priority = 0;

    private float maxVolume = float.MaxValue;

    public SoundCategories.SoundCategory GetSoundCategory()
    {
        return soundCategory;
    }

    public int GetSoundIndex()
    {
        return sound;
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetPitch()
    {
        return pitch;
    }

    public float GetPriority()
    {
        return priority;
    }

    public float GetVolumeFromDistance(float distance, float maxDistance)
    {
        return Mathf.Max(0, maxVolume - maxVolume / maxDistance * distance);
    }

    public void SetVolumeFromDistance(float distance, float maxDistance)
    {
        // Store the max volume in the first iteration
        if (maxVolume == float.MaxValue)
            maxVolume = volume;
        volume = GetVolumeFromDistance(distance, maxDistance);
    }
}

/// <summary>
/// Information about the sfx
/// </summary>
[System.Serializable]
public class Sound
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    [Range(0f, 1f)]
    private float volume = 1;
    [SerializeField]
    [Range(0.5f, 1.5f)]
    private float pitch = 1;

    public AudioClip GetAudioClip()
    {
        return audioClip;
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetPitch()
    {
        return pitch;
    }

    public float GetLength()
    {
        return audioClip.length;
    }
}

/// <summary>
/// Sound system
/// </summary>
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundPoolLooping looping;
    [SerializeField]
    private SoundPool enemyAmbients;
    [SerializeField]
    private SoundPool attack;
    [SerializeField]
    private SoundPool skill;
    [SerializeField]
    private SoundPool ui;
    [SerializeField]
    private SoundPool items;
    [SerializeField]
    private SoundPool deaths;
    [SerializeField]
    private SoundPool takeDamage;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;

        looping.Start(transform);
        enemyAmbients.Start(transform);
        attack.Start(transform);
        skill.Start(transform);
        ui.Start(transform);
        items.Start(transform);
        deaths.Start(transform);
        takeDamage.Start(transform);
    }

    /// <summary>
    /// Play the sound
    /// </summary>
    /// <param name="soundSettings">setting of the sound</param>
    public void PlaySound(SoundSettings soundSettings)
    {
        var pool = GetPool(soundSettings.GetSoundCategory());
        pool.PlaySound(soundSettings);
    }

    /// <summary>
    /// Play looping sound
    /// The caller must ensure stopping the played sound by calling StopLoopingSound() with returned SoundData
    /// </summary>
    [Pure]
    public SoundData PlaySoundLooping(SoundSettings soundSettings)
    {
        Assert.AreEqual(soundSettings.GetSoundCategory(), SoundCategories.SoundCategory.Looping,
                        "Sound must be in Looping category; use PlaySound() instead");
        return looping.PlaySound(soundSettings);
    }

    public void StopLoopingSound(SoundData soundData)
    {
        if (soundData == null)
            return;
        looping.StopSound(soundData);
    }

    /// <summary>
    /// Update volume of the looping sound
    /// </summary>
    public void UpdateLoopingSound(SoundData soundData, float volume)
    {
        if (soundData == null)
            return;
        looping.UpdateSound(soundData, volume);
    }

    /// <summary>
    /// Return the proper audiopool for a given sound category
    /// </summary>

    private SoundPool GetPool(SoundCategories.SoundCategory category)
    {
        Assert.AreNotEqual(category, SoundCategories.SoundCategory.Looping,
                           "Normal sound must not be in loop; use PlaySoundLooping() instead");
        return category switch { SoundCategories.SoundCategory.EnemyAmbients => enemyAmbients,
                                 SoundCategories.SoundCategory.Attack => attack,
                                 SoundCategories.SoundCategory.Skill => skill,
                                 SoundCategories.SoundCategory.Ui => ui,
                                 SoundCategories.SoundCategory.Items => items,
                                 SoundCategories.SoundCategory.Death => deaths,
                                 SoundCategories.SoundCategory.TakeDamage => takeDamage,
                                 _ => throw new ArgumentOutOfRangeException(nameof(category), category, null) };
    }

    public float GetSoundLenght(SoundSettings settings)
    {
        SoundPool pool = GetPool(settings.GetSoundCategory());
        Sound sound = pool.GetSoundAtIndex(settings.GetSoundIndex());
        return sound.GetLength();
    }
}
