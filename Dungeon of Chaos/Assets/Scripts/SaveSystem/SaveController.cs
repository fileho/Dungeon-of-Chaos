using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;

public class SaveController : MonoBehaviour
{
    public SaveData saveData;
    private readonly ActiveSaveSlot saveSlot = new ActiveSaveSlot();

    public Stats stats;
    public SkillSystem skillSystem;

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

    private string CreatePath()
    {
        return CreatePath(saveSlot.GetSaveSlot());
    }

    private string CreatePath(int saveIndex)
    {
        return Application.persistentDataPath + "/save" + saveIndex + ".bin";
    }

    private void Save()
    {
        var formatter = new BinaryFormatter();
        string path = CreatePath();
        FileStream stream = new FileStream(path, FileMode.Create) { Position = 0 };

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/" + saveSlot + ".bin";

        saveData = LoadData(path);

        Assert.IsNotNull(saveData);
        stats.Load(saveData.savedStats);
        skillSystem.Load(saveData.savedSkillSystem);
    }

    private SaveData LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("new save created");
            return new SaveData(stats, skillSystem);
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open) { Position = 0 };

        saveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();
        return saveData;
    }

    public SaveData GetSavedData(int index)
    {
        var path = CreatePath(index);
        return !File.Exists(path) ? null : LoadData(CreatePath(index));
    }

    public void SetSaveSlot(int index)
    {
        saveSlot.SaveActiveSlot(index);
    }
}
