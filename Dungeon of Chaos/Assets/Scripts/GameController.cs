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

        LoadMapElements(FindObjectsOfType<Checkpoint>());
        LoadMapElements(FindObjectsOfType<MapFragment>());
    }

    // TODO remove this later
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Level complete - cheat");
            LevelComplete();
        }
    }

    private void LoadMapElements<T>(IEnumerable<T> list)
        where T : IMapSavable
    {
        foreach (var elem in list)
        {
            if (saveSystem.DungeonData.IsSaved(elem.GetUniqueId()))
                elem.Load();
        }
    }

    public void LevelComplete()
    {
        saveSystem.LevelComplete();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
