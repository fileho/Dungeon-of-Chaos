using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundtrack : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> music;
    [SerializeField]
    private List<AudioClip> bossMusic;

    private AudioSource audioSource;

    private int song;
    private bool boss;
    private bool isTransitioning;
    private float volume = 1f;

    private void Awake()
    {
        var soundtracks = FindObjectsOfType<Soundtrack>();
        if (soundtracks.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UIEvents.SoundTrackVolumeChanged += SetVolume;
        UIEvents.MasterVolumeChanged += SetVolume;
        SetVolume();
    }

    private void OnDestroy()
    {
        UIEvents.SoundTrackVolumeChanged -= SetVolume;
        UIEvents.MasterVolumeChanged -= SetVolume;
    }

    private void SetVolume(float _ = 0)
    {
        volume = PlayerPrefsManager.MasterVolume / 100 * PlayerPrefsManager.SoundTrackVolume / 100;
        audioSource.volume = volume;
    }

    private void Update()
    {
        if (isTransitioning)
            return;

        if (audioSource.isPlaying)
            return;

        NextSong();
    }

    public void PlayBossMusic()
    {
        boss = true;
        song = 0;
        StartCoroutine(Transition(bossMusic[0]));
    }

    public void StopBossMusic()
    {
        boss = false;
        song = 0;
        StartCoroutine(Transition(music[0]));
    }

    private void NextSong()
    {
        song++;
        if (!boss)
        {
            song %= music.Count;
            StartCoroutine(Transition(music[song]));
        }
        else
        {
            song %= bossMusic.Count;
            StartCoroutine(Transition(bossMusic[song]));
        }
    }

    private IEnumerator Transition(AudioClip target)
    {
        isTransitioning = true;
        const float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(1 - time / duration);
            audioSource.volume = volume * t;
            yield return null;
        }

        audioSource.clip = target;
        audioSource.Play();

        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            audioSource.volume = volume * t;
            yield return null;
        }

        isTransitioning = false;
    }
}
