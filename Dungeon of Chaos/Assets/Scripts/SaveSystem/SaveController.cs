using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;

public class SaveController : MonoBehaviour
{
    public SaveData saveData;
    private string saveName = "save1";

    public Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        saveData = new SaveData(stats);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveStats();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }

    public void SaveStats()
    {
        saveData.SaveStats(stats);
        Save();
    }

    private void Save()
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + saveName + ".bin";

        FileStream stream = new FileStream(path, FileMode.Create) {Position = 0};


        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/" + saveName + ".bin";

        if (!File.Exists(path))
        {
            saveData = new SaveData(stats);
            return;
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open) {Position = 0};

        saveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        Assert.IsNotNull(saveData);
        stats.Load(saveData.savedStats);

        Debug.Log(stats.GetStrength());
    }
}
