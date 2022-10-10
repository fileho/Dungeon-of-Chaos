[System.Serializable]
public class SaveData
{
    public int dungeon;
    public SavedStats savedStats;

    public SaveData(Stats stats)
    {
        dungeon = 1;
        savedStats = stats.Save();
    }

    public void SaveStats(Stats stats)
    {
        savedStats = stats.Save();
    }
}
