using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundSettings
{
    [SerializeField] public string name { get; private set; }
    [SerializeField] public float volume { get => volume; private set => volume = 1; }
    [SerializeField] public float pitch { get => pitch; private set => pitch = 1; }
}

[System.Serializable]
public class Sound
{
    [SerializeField] private AudioClip audioClip;
    [Range(0f,1f)] private float volume;
    [Range(0.5f, 1.5f)] private float pitch;
    public string name;

    private AudioSource audioSource;

    public void SetSource(AudioSource source)
    {
        audioSource = source;
        audioSource.clip = audioClip;
    }

    public void Play(float vol = 1f, float p = 1f)
    {
        audioSource.volume = volume*vol;
        audioSource.pitch = pitch*p;
        audioSource.Play();
    }
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<Sound> soundEffects;
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
        foreach (var s in soundEffects)
        {
            GameObject go = new GameObject(s.name);
            s.SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name, float vol = 1f, float pitch = 1f)
    {
        Sound s = soundEffects.Find(sound => sound.name == name);
        s.Play(vol, pitch);
    }
}
