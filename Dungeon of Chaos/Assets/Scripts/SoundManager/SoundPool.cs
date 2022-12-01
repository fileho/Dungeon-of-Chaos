using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SoundPool
{
    [SerializeField]
    private int poolSize = 30;
    [SerializeField]
    private List<Sound> sounds;
    private AudioSource[] pool;
    private int poolIndex;

    public void Start(Transform transform)
    {
        pool = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject go = new GameObject();
            go.AddComponent<AudioSource>();
            go.transform.parent = transform;
            pool[i] = go.GetComponent<AudioSource>();
        }
    }

    public Sound GetSoundAtIndex(int index)
    {
        if (index >= sounds.Count)
        {
            return null;
        }
        return sounds[index];
    }

    public void PlaySound(SoundSettings soundSettings)
    {
        var source = FindEmptyAudioSource(soundSettings.GetPriority());
        var sound = GetSoundAtIndex(soundSettings.GetSoundIndex());
        if (sound == null)
        {
            Debug.LogWarning("Sound not found " + soundSettings.GetSoundCategory() + " at index " +
                             soundSettings.GetSoundIndex());
            return;
        }
        source.volume = sound.GetVolume() * soundSettings.GetVolume() * PlayerPrefsManager.MasterVolume *
                        PlayerPrefsManager.SFXVolume;
        source.pitch = sound.GetPitch() * soundSettings.GetPitch();
        source.clip = sound.GetAudioClip();
        source.Play();
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

