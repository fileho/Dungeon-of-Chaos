using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores which tutorials were already shown to not show them again
/// </summary>
[System.Serializable]
public class TutorialData
{
    private List<int> states = new List<int>();

    public void SaveState(int state)
    {
        if (!states.Contains(state))
            states.Add(state);
    }

    public bool HasState(int state)
    {
        return states.Contains(state);
    }
}
