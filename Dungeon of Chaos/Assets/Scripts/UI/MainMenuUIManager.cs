using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class MainMenuUIManager : MonoBehaviour
{
    [Header("=== Main Menu ===")]
    [SerializeField]
    Button newGame;
    [SerializeField]
    Button quitGame;

    private void OnEnable()
    {
        newGame.onClick.AddListener(OnNewGamePressed);
        quitGame.onClick.AddListener(OnQuitGamePressed);
    }

    private void OnDisable()
    {
        newGame.onClick.RemoveListener(OnNewGamePressed);
        quitGame.onClick.RemoveListener(OnQuitGamePressed);
    }

    void OnNewGamePressed()
    {
        print("New Game Called");
        UIEvents.NewGamePressed?.Invoke();
    }

    void OnQuitGamePressed()
    {
        print("Application Quit Called");
        Application.Quit();
    }
}

public static class UIEvents
{
    public static UnityAction<int> LoadSlotPressed;
    public static UnityAction NewGamePressed;
    public static UnityAction<float> MasterVolumeChanged;
    public static UnityAction<float> SoundTrackVolumeChanged;
    public static UnityAction<float> SFXVolumeChanged;
    public static UnityAction<float> BrightnessChanged;
    public static UnityAction<int> SaveSlotPressed;
}
