using System;

[System.Serializable]
public class SaveData
{
    public int dungeon;
    public SavedStats savedStats;
    public SavedSkillSystem savedSkillSystem;
    public DateTime timestamp;

    public SaveData(Stats stats, SkillSystem skillSystem)
    {
        dungeon = 1;
        savedStats = stats.Save();
        savedSkillSystem = skillSystem.Save();
        timestamp = DateTime.Now;
    }

    public void SaveStats(Stats stats)
    {
        savedStats = stats.Save();
        timestamp = DateTime.Now;
    }

    public void SaveSkillSystem(SkillSystem skillSystem)
    {
        savedSkillSystem = skillSystem.Save();
        timestamp = DateTime.Now;
    }
}
