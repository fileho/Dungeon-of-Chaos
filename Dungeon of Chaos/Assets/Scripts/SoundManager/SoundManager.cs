using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEngine.Assertions;
#endif




[System.Serializable]
public class SoundSettings
{
    [SerializeField] private SoundCategories.SoundCategory soundCategory;
    [SerializeField] private int sound;

    [SerializeField] private float volume = 1;
    [SerializeField] private float pitch = 1;
    [SerializeField] private float priority = 0;

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
    [SerializeField] private AudioClip audioClip;
    [SerializeField] [Range(0f,1f)] private float volume = 1;
    [SerializeField] [Range(0.5f, 1.5f)] private float pitch = 1;

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

[System.Serializable]
public class SoundPool
{
    [SerializeField] private int poolSize = 30;
    [SerializeField] private List<Sound> sounds;
    private AudioSource[] pool;
    private int poolIndex;

    public void Start(Transform transform)
    {
        pool = new AudioSource[poolSize];
        for(int i = 0; i < poolSize; ++i)
        {
            GameObject go = new GameObject();
            go.AddComponent<AudioSource>();
            go.transform.parent = transform;
            pool[i] = go.GetComponent<AudioSource>();
        }
    }

    public Sound GetSoundAtIndex(int index)
    {
        return sounds[index];
    }

    // TODO add priority handling
    public AudioSource FindEmptyAudioSource(float priority)
    {
        int lastIndex = poolIndex;
        // start search from last empty position
        while (++poolIndex < poolSize)
        {
            if (!pool[poolIndex].isPlaying)
                return pool[poolIndex];
        }

        poolIndex = 0;
        while (++poolIndex < lastIndex)
        {
            if (!pool[poolIndex].isPlaying)
                return pool[poolIndex];
        }

        return pool[(lastIndex + 1) % poolSize];
    }
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundPool ambients;
    [SerializeField] private SoundPool footsteps;
    [SerializeField] private SoundPool attack;
    [SerializeField] private SoundPool skill;
    [SerializeField] private SoundPool ui;
    [SerializeField] private SoundPool items;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this;

        ambients.Start(transform);
        footsteps.Start(transform);
        attack.Start(transform);
        skill.Start(transform);
        ui.Start(transform);
        items.Start(transform);
    }

    public void PlaySound(SoundSettings soundSettings)
    {
        var pool = GetPool(soundSettings.GetSoundCategory());
        var source = pool.FindEmptyAudioSource(soundSettings.GetPriority());
        var sound = pool.GetSoundAtIndex(soundSettings.GetSoundIndex());
        PlaySound(source, sound, soundSettings);
    }

    private void PlaySound(AudioSource source, Sound sound, SoundSettings soundSettings)
    {
        source.volume = sound.GetVolume() * soundSettings.GetVolume();
        source.pitch = sound.GetPitch() * soundSettings.GetPitch();
        source.clip = sound.GetAudioClip();
        source.Play();
    }

    private SoundPool GetPool(SoundCategories.SoundCategory category)
    {
        return category switch
        {
            SoundCategories.SoundCategory.Ambient => ambients,
            SoundCategories.SoundCategory.Footsteps => footsteps,
            SoundCategories.SoundCategory.Attack => attack,
            SoundCategories.SoundCategory.Skill => skill,
            SoundCategories.SoundCategory.Ui => ui,
            SoundCategories.SoundCategory.Items => items,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }

    public float GetLength(SoundSettings soundSettings)
    {
        var pool = GetPool(soundSettings.GetSoundCategory());
        var sound = pool.GetSoundAtIndex(soundSettings.GetSoundIndex());
        return sound.GetLength();
    }
}
