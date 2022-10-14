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

    private Character character;

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            SaveProgress();
        if (Input.GetKeyDown(KeyCode.L))
            Load();
    }

    public void SaveProgress(SaveAttributes attributes)
    {
        saveData = new SaveData(attributes);
        Save();
    }

    public void SaveProgress()
    {
        saveData =
            new SaveData(new SaveAttributes(1, character.transform.position, character.stats, character.SkillSystem));
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
        if (!character)
            character = FindObjectOfType<Character>();

        string path = CreatePath();
        saveData = LoadData(path);

        Assert.IsNotNull(saveData);
        character.stats.Load(saveData.savedStats);
        character.SkillSystem.Load(saveData.savedSkillSystem);
        character.transform.position = saveData.characterPosition.ToV3();
    }

    private SaveData LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new SaveData(new SaveAttributes(1, Vector3.zero, character.stats, character.SkillSystem));
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open) { Position = 0 };

        var data = formatter.Deserialize(stream) as SaveData;
        stream.Close();
        return data;
    }

    public SaveData GetSavedData(int saveSlotIndex)
    {
        var path = CreatePath(saveSlotIndex);
        return !File.Exists(path) ? null : LoadData(CreatePath(saveSlotIndex));
    }

    public void SetSaveSlot(int index)
    {
        saveSlot.SaveActiveSlot(index);
    }
}
