using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SaveSystem saveSystem;

    private void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
        saveSystem.Load();
    }

    public void Death()
    {
        ReloadScene();
    }

    public void SaveAndReload()
    {
        Time.timeScale = 1f;
        saveSystem.SaveProgress();
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
