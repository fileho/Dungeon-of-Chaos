using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;

public class SaveController : MonoBehaviour
{
    // save slot
    // save settings
    // save game

    public SaveData saveData;
    private string saveName = "save1";

    public Stats stats;
    public SkillSystem skillSystem;

    // Start is called before the first frame update
    void Start()
    {
        saveData = new SaveData(stats, skillSystem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveProgress();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }

    public void SaveProgress()
    {
        saveData.SaveStats(stats);
        saveData.SaveSkillSystem(skillSystem);
        Save();
    }

    private void Save()
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + saveName + ".bin";

        FileStream stream = new FileStream(path, FileMode.Create) { Position = 0 };

        formatter.Serialize(stream, saveData);
        stream.Close();

        Debug.Log("saved");
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/" + saveName + ".bin";

        if (!File.Exists(path))
        {
            Debug.Log("new save created");
            saveData = new SaveData(stats, skillSystem);
            return;
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open) { Position = 0 };

        saveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        Assert.IsNotNull(saveData);
        stats.Load(saveData.savedStats);
        skillSystem.Load(saveData.savedSkillSystem);

        Debug.Log("loaded");
    }
}
