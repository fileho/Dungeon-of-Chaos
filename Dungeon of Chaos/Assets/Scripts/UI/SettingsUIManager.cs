using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SettingsUIManager : MonoBehaviour
{
    [Header("=== Settings Menu ===")]
    [SerializeField]
    Slider masterVolume;
    [SerializeField]
    Slider soundTrackVolume;
    [SerializeField]
    Slider sfxVolume;

    private void Init()
    {
        masterVolume.value = PlayerPrefsManager.MasterVolume;
        soundTrackVolume.value = PlayerPrefsManager.SoundTrackVolume;
        sfxVolume.value = PlayerPrefsManager.SFXVolume;
    }

    private void OnEnable()
    {
        masterVolume.onValueChanged.AddListener(OnMasterVolumeChanged);
        soundTrackVolume.onValueChanged.AddListener(OnSoundTrackVolumeChanged);
        sfxVolume.onValueChanged.AddListener(OnSFXVolumeChanged);

        Init();
    }

    private void OnDisable()
    {
        masterVolume.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        soundTrackVolume.onValueChanged.RemoveListener(OnSoundTrackVolumeChanged);
        sfxVolume.onValueChanged.RemoveListener(OnSFXVolumeChanged);
    }

    void OnMasterVolumeChanged(float value)
    {
        PlayerPrefsManager.MasterVolume = masterVolume.value;
        UIEvents.MasterVolumeChanged?.Invoke(value);
    }

    void OnSoundTrackVolumeChanged(float value)
    {
        PlayerPrefsManager.SoundTrackVolume = soundTrackVolume.value;
        UIEvents.SoundTrackVolumeChanged?.Invoke(value);
    }

    void OnSFXVolumeChanged(float value)
    {
        PlayerPrefsManager.SFXVolume = sfxVolume.value;
        UIEvents.SFXVolumeChanged?.Invoke(value);
    }
}
