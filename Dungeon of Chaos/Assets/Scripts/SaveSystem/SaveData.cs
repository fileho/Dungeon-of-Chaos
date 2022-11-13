using System;
using UnityEngine;

public struct SaveAttributes
{
    public Vector3 characterPosition;
    public Stats stats;
    public SkillSystem skillSystem;
    public DungeonData dungeonData;

    public SaveAttributes(Vector3 characterPosition, Stats stats, SkillSystem skillSystem, DungeonData dungeonData)
    {
        this.characterPosition = characterPosition;
        this.stats = stats;
        this.skillSystem = skillSystem;
        this.dungeonData = dungeonData;
    }
}

[System.Serializable]
public class SaveData
{
    public Vector3s characterPosition;
    public SavedStats savedStats;
    public SavedSkillSystem savedSkillSystem;
    public DungeonData dungeonData;
    public DateTime timestamp;

    public SaveData(SaveAttributes data)
    {
        characterPosition = Vector3s.FromV3(data.characterPosition);
        savedStats = data.stats.Save();
        savedSkillSystem = data.skillSystem.Save();
        dungeonData = data.dungeonData;
        timestamp = DateTime.Now;
    }

    // Serializable version of Vector3
    [System.Serializable]
    public struct Vector3s
    {
        public float x;
        public float y;
        public float z;

        public Vector3s(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public static Vector3s FromV3(Vector3 v)
        {
            return new Vector3s(v);
        }

        public Vector3 ToV3()
        {
            return new Vector3(x, y, z);
        }
    }
}
