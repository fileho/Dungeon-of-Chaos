using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SaveSystem saveSystem;

    private bool loaded = false;

    private void LateUpdate()
    {
        if (loaded)
            return;

        loaded = true;
        saveSystem = FindObjectOfType<SaveSystem>();
        saveSystem.Load();

        LoadMap();
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

    private void LoadMap()
    {
        LoadMapElements(FindObjectsOfType<Checkpoint>());
        LoadMapElements(FindObjectsOfType<MapFragment>());
        LoadMapElements(FindObjectsOfType<Chest>());
        LoadMapElements(FindObjectsOfType<ResetBook>());
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

    public void StartBossFight()
    {
        foreach (var lavaPit in FindObjectsOfType<LavaPit>())
            lavaPit.StartLights();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
