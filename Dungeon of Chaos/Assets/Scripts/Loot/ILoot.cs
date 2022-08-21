using UnityEngine;

public abstract class ILoot : ScriptableObject
{
    public abstract ILoot Init(Enemy enemy);
    public abstract void Drop();
}

[System.Serializable]
public class LootItem
{
    public GameObject prefab;
    public float weight = 1f;
}
