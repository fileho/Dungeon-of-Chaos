using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
    }
}
