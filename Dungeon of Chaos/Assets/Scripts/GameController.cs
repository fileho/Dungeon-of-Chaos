using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private string bossName;

    private SaveSystem saveSystem;
    private bool loaded = false;
    private Soundtrack soundtrack;

    private void Start()
    {
        soundtrack = FindObjectOfType<Soundtrack>();
        soundtrack.StopBossMusic();
        CultureInfo.CurrentCulture = new CultureInfo("en-us");
    }

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
        /*if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Level complete - cheat");
            LevelComplete();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Cheat + 1000xp");
            Character.instance.stats.GetLevellingData().ModifyCurrentXP(1000);
        }*/
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
        FindObjectOfType<GameoverUI>().ShowGameover();
        Invoke(nameof(ReloadScene), 2f);
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

        int level = Math.Max(0, SceneManager.GetActiveScene().buildIndex - 2);
        FindObjectOfType<Soundtrack>().PlayBossMusic(level);
        InGameUIManager.instance.StartBossFight(bossName);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
