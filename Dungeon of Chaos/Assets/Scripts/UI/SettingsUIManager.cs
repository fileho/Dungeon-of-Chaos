using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class SettingsUIManager : MonoBehaviour
{
    [Header("=== Settings Menu ===")]
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider soundTrackVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] Slider brightness;


    private void Init() {
        masterVolume.value = PlayerPrefsManager.MasterVolume;
        soundTrackVolume.value = PlayerPrefsManager.SoundTrackVolume;
        sfxVolume.value = PlayerPrefsManager.SFXVolume;
        brightness.value = PlayerPrefsManager.Brightness;
    }

    private void OnEnable() {
        masterVolume.onValueChanged.AddListener(OnMasterVolumeChanged);
        soundTrackVolume.onValueChanged.AddListener(OnSoundTrackVolumeChanged);
        sfxVolume.onValueChanged.AddListener(OnSFXVolumeChanged);
        brightness.onValueChanged.AddListener(OnBrightnessChanged);

        Init();
    }


    private void OnDisable() {
        masterVolume.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        soundTrackVolume.onValueChanged.RemoveListener(OnSoundTrackVolumeChanged);
        sfxVolume.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        brightness.onValueChanged.RemoveListener(OnBrightnessChanged);
    }


    void OnMasterVolumeChanged(float value) {
        print("MasterVolumeChanged: " + value);
        UIEvents.MasterVolumeChanged?.Invoke(value);
    }

    void OnSoundTrackVolumeChanged(float value) {
        print("SoundTrackVolumeChanged: " + value);
        UIEvents.SoundTrackVolumeChanged?.Invoke(value);
    }

    void OnSFXVolumeChanged(float value) {
        print("SFXVolumeChanged: " + value);
        UIEvents.SFXVolumeChanged?.Invoke(value);
    }

    void OnBrightnessChanged(float value) {
        print("BrightnessChanged: " + value);
        UIEvents.BrightnessChanged?.Invoke(value);
    }
}
