using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SaveController saveController;

    private void Start()
    {
        saveController = FindObjectOfType<SaveController>();
        saveController.Load();
    }

    public void Death()
    {
        ReloadScene();
    }

    public void SaveAndReload()
    {
        saveController.SaveProgress();
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
