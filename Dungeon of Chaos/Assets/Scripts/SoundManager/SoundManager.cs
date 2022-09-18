using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSettings
{
    [SerializeField] private string name;
    [SerializeField] private float volume = 1;
    [SerializeField] private float pitch = 1;

    public string GetName()
    {
        return name;
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetPitch()
    {
        return pitch;
    }
}

[System.Serializable]
public class Sound
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] [Range(0f,1f)] private float volume = 1;
    [SerializeField] [Range(0.5f, 1.5f)] private float pitch = 1;
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

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }

    public float GetLenght()
    {
        return audioClip.length;
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
        if (s == null)
            return;
        if (s.IsPlaying())
            return;
        s.Play(vol, pitch);
    }

    public void StopPlaying(string name)
    {
        Sound s = soundEffects.Find(sound => sound.name == name);
        if (s == null)
            return;
        s.StopPlaying();
    }

    public float GetLenght(string name)
    {
        Sound s = soundEffects.Find(sound => sound.name == name);
        if (s == null)
            return 0;
        return s.GetLenght();
    }
}
