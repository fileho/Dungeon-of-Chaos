using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for objects to be saved in the dungeon
/// </summary>
public interface IMapSavable
{
    void SetUniqueId(int uid);
    int GetUniqueId();

    void Load();

    Object GetAttachedComponent();
}
