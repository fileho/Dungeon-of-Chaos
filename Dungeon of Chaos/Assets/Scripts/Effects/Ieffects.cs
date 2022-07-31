using UnityEngine;

public abstract class Ieffects : ScriptableObject
{
    public abstract Ieffects Init(Transform transform);
    public abstract void TakeDamage();
}
