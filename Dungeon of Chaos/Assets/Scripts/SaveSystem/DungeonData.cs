using System.Collections.Generic;

/// <summary>
/// Raw data for the dungeon, gets the dungeons and all object that needs to be saved
/// </summary>
[System.Serializable]
public class DungeonData
{
    public int dungeon;
    private readonly List<int> mapElements = new List<int>();

    public DungeonData(int dungeon = 1)
    {
        this.dungeon = dungeon;
    }

    public void AddSavedUid(int uid)
    {
        if (!mapElements.Contains(uid))
            mapElements.Add(uid);
    }

    public bool IsSaved(int uid)
    {
        return mapElements.Contains(uid);
    }
}
