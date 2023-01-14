using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Handles all saving, uses the BinaryFormatter
/// </summary>
public class SaveSystem : MonoBehaviour
{
    public SaveData SaveData { get; private set; }
    public DungeonData DungeonData { get; private set; }

    [SerializeField]
    [NotNull]
    private DefaultCharacterPositions defaultCharacterPositions;

    [Tooltip("Index of the first dungeon in build settings")]
    public const int SceneOffset = 1;

    // Which save file should be used
    private readonly ActiveSaveSlot saveSlot = new ActiveSaveSlot();
    private Character character;

    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    /// <summary>
    /// Move to the next dungeon - reset all dungeon data, keep character data
    /// </summary>
    public void LevelComplete()
    {
        int dungeon = SaveData.dungeonData.dungeon + 1 - SceneOffset;
        var charPos = defaultCharacterPositions.positions[dungeon];
        SaveData = new SaveData(
            new SaveAttributes(charPos, character.stats, character.SkillSystem, new DungeonData(dungeon + 1)));

        Save();
    }

    /// <summary>
    /// Save the progress in character and dungeonData
    /// </summary>
    public void SaveProgress()
    {
        SaveData = new SaveData(
            new SaveAttributes(character.transform.position, character.stats, character.SkillSystem, DungeonData));
        Save();
    }

    /// <summary>
    /// Load progress, called when the scene loads by the GameController
    /// </summary>
    public void Load()
    {
        if (!character)
            character = FindObjectOfType<Character>();

        string path = CreatePath();
        SaveData = LoadData(path);

        Assert.IsNotNull(SaveData);
        DungeonData = SaveData.dungeonData;

        // Save slot screen, we don't want to load character data
        if (character == null)
            return;
        character.stats.Load(SaveData.savedStats);
        character.SkillSystem.Load(SaveData.savedSkillSystem);
        character.transform.position = SaveData.characterPosition.ToV3();
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

        formatter.Serialize(stream, SaveData);
        stream.Close();
    }

    private SaveData LoadData(string filePath)
    {
        if (!File.Exists(filePath))
        {
            // Default save
            return new SaveData(new SaveAttributes(defaultCharacterPositions.positions[0], character.stats,
                                                   character.SkillSystem, new DungeonData()));
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(filePath, FileMode.Open) { Position = 0 };

        var data = formatter.Deserialize(stream) as SaveData;
        stream.Close();
        return data;
    }

    // Public API functions for the save slot
    public SaveData GetSavedData(int saveSlotIndex)
    {
        var path = CreatePath(saveSlotIndex);
        return !File.Exists(path) ? null : LoadData(CreatePath(saveSlotIndex));
    }

    public void SetSaveSlot(int index)
    {
        saveSlot.SaveActiveSlot(index);
    }

    public void RemoveSave()
    {
        string path = CreatePath();

        if (File.Exists(path))
            File.Delete(path);
    }

    public void RemoveSave(int saveIndex)
    {
        string path = CreatePath(saveIndex);
        if (File.Exists(path))
            File.Delete(path);
    }

    public void RemoveAllSaves()
    {
        const int saveSlots = 3;
        for (int i = 0; i < saveSlots; i++)
        {
            RemoveSave(i);
        }
    }
}
