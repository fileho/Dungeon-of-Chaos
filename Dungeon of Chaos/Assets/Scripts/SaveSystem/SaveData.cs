[System.Serializable]
public class SaveData
{
    public int dungeon;
    public SavedStats savedStats;
    public SavedSkillSystem savedSkillSystem;
    // Skillsystem - id, level
    // 
    // timestamp


    public SaveData(Stats stats, SkillSystem skillSystem)
    {
        dungeon = 1;
        savedStats = stats.Save();
        savedSkillSystem = skillSystem.Save();
    }

    public void SaveStats(Stats stats)
    {
        savedStats = stats.Save();
    }

    public void SaveSkillSystem(SkillSystem skillSystem)
    {
        savedSkillSystem = skillSystem.Save();
    }
}
