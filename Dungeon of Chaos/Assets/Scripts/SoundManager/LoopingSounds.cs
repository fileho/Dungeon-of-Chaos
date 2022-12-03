using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to interact with looping sounds
/// </summary>
public class SoundData
{
    public SoundData(int index, int uid)
    {
        Index = index;
        Uid = uid;
    }

    public int Index { get; }
    public int Uid { get; }
}

[System.Serializable]
public class SoundPoolLooping
{
    [SerializeField]
    private int poolSize = 30;
    [SerializeField]
    private List<Sound> sounds;
    private PoolData[] pool;
    private int poolIndex;

    private int currentId;

    struct PoolData
    {
        public AudioSource audioSource;
        public int uid;
    }

    public void Start(Transform transform)
    {
        pool = new PoolData[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            GameObject go = new GameObject();
            var source = go.AddComponent<AudioSource>();
            source.loop = true;
            go.transform.parent = transform;
            pool[i].audioSource = go.GetComponent<AudioSource>();
        }
    }

    private Sound GetSoundAtIndex(int index)
    {
        if (index >= sounds.Count)
        {
            return null;
        }
        return sounds[index];
    }

    public SoundData PlaySound(SoundSettings soundSettings)
    {
        var sound = GetSoundAtIndex(soundSettings.GetSoundIndex());
        var audioIndex = FindEmptyAudioSource(soundSettings.GetPriority());

        var audio = pool[audioIndex].audioSource;
        audio.clip = sound.GetAudioClip();
        audio.volume = soundSettings.GetVolume() * sound.GetVolume() * PlayerPrefsManager.MasterVolume / 100 *
                       PlayerPrefsManager.SFXVolume / 100;
        audio.Play();
        var uid = NextUid();
        pool[audioIndex].uid = uid;

        return new SoundData(audioIndex, uid);
    }

    public void StopSound(SoundData soundData)
    {
        var p = pool[soundData.Index];

        if (p.uid == soundData.Uid)
        {
            p.audioSource.Stop();
        }
    }

    public void UpdateSound(SoundData soundData, float volume)
    {
        var p = pool[soundData.Index];

        if (p.uid == soundData.Uid)
        {
            p.audioSource.volume = sounds[soundData.Index].GetVolume() * volume * PlayerPrefsManager.MasterVolume / 100 *
                                   PlayerPrefsManager.SFXVolume / 100;
        }
    }

    private int NextUid()
    {
        currentId %= 1000000000;
        ++currentId;
        return currentId;
    }

    // TODO add priority handling
    private int FindEmptyAudioSource(float priority)
    {
        int lastIndex = poolIndex;
        // start search from last empty position
        while (++poolIndex < poolSize)
        {
            if (!pool[poolIndex].audioSource.isPlaying)
                return poolIndex;
        }

        poolIndex = 0;
        while (++poolIndex < lastIndex)
        {
            if (!pool[poolIndex].audioSource.isPlaying)
                return poolIndex;
        }

        return (lastIndex + 1) % poolSize;
    }
}
