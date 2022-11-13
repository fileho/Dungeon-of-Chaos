using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapSavable
{
    void SetUniqueId(int uid);
    int GetUniqueId();

    void Load();

    Object GetAttachedComponent();
}
