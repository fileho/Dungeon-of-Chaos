using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ILoot : ScriptableObject
{
    public abstract ILoot Init(Transform transform);
    public abstract void Drop();
}

[System.Serializable]
public class LootItem
{
    public GameObject prefab;
    public float weight = 1f;
}
