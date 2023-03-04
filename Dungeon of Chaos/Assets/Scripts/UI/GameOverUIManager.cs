using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Currently, we are not using this class, as this way of handling dying slows down the game too much (we are
/// attempting to have a more fast paced game)
/// </summary>
public class GameOverUIManager : MonoBehaviour
{
    [Header("=== Game Over Menu ===")]
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
