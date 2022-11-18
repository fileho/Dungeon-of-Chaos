using System.Collections.Generic;
using System.Runtime.InteropServices;

[System.Serializable]
public class DungeonData
{
    public int dungeon = 1;
    private readonly List<int> mapElements = new List<int>();

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
