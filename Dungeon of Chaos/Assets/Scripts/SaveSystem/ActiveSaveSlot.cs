using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Stores the active save index
/// </summary>
public class ActiveSaveSlot
{
    [System.Serializable]
    public class SaveSlotData
    {
        public int index;

        public SaveSlotData(int index = 0)
        {
            this.index = index;
        }
    }

    private SaveSlotData data;
    private const string fileName = "/saveSlot.bin";

    public void SaveActiveSlot(int index)
    {
        data = new SaveSlotData(index);
        Save();
    }

    public int GetSaveSlot()
    {
        if (data == null)
            Load();

        return data.index;
    }

    /// <summary>
    /// Save the index of active save from the disk
    /// </summary>
    private void Save()
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + fileName;
        FileStream stream = new FileStream(path, FileMode.Create) { Position = 0 };

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Load the index of active save from the disk
    /// </summary>
    private void Load()
    {
        string path = Application.persistentDataPath + fileName;
        if (!File.Exists(path))
        {
            data = new SaveSlotData();
            return;
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open) { Position = 0 };

        data = formatter.Deserialize(stream) as SaveSlotData;
        stream.Close();
    }
}
