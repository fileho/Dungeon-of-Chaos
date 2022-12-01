using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.Assertions;
#endif

[System.Serializable]
public class SoundSettings
{
    [SerializeField]
    private SoundCategories.SoundCategory soundCategory;
    [SerializeField]
    private int sound;

    [SerializeField]
    private float volume = 1;
    [SerializeField]
    private float pitch = 1;
    [SerializeField]
    private float priority = 0;

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
}

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

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private SoundPoolLooping looping;
    [SerializeField]
    private SoundPool footsteps;
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
        footsteps.Start(transform);
        attack.Start(transform);
        skill.Start(transform);
        ui.Start(transform);
        items.Start(transform);
        deaths.Start(transform);
        takeDamage.Start(transform);
    }

    public void PlaySound(SoundSettings soundSettings)
    {
        var pool = GetPool(soundSettings.GetSoundCategory());
        pool.PlaySound(soundSettings);
    }

    /// <summary>
    /// The called must ensure stopping the played sound by calling StopLoopingSound() with returned SoundData
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

    public void UpdateLoopingSound(SoundData soundData, float volume)
    {
        if (soundData == null)
            return;
        looping.UpdateSound(soundData, volume);
    }

    private SoundPool GetPool(SoundCategories.SoundCategory category)
    {
        Assert.AreNotEqual(category, SoundCategories.SoundCategory.Looping,
                           "Normal sound must not be in loop; use PlaySoundLooping() instead");
        return category switch { SoundCategories.SoundCategory.Footsteps => footsteps,
                                 SoundCategories.SoundCategory.Attack => attack,
                                 SoundCategories.SoundCategory.Skill => skill,
                                 SoundCategories.SoundCategory.Ui => ui,
                                 SoundCategories.SoundCategory.Items => items,
                                 SoundCategories.SoundCategory.Death => deaths,
                                 SoundCategories.SoundCategory.TakeDamage => takeDamage,
                                 _ => throw new ArgumentOutOfRangeException(nameof(category), category, null) };
    }

    public float GetLength(SoundSettings soundSettings)
    {
        var pool = GetPool(soundSettings.GetSoundCategory());
        var sound = pool.GetSoundAtIndex(soundSettings.GetSoundIndex());
        return sound.GetLength();
    }
}
