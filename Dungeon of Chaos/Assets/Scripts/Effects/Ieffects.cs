using UnityEngine;

/// <summary>
/// Visual effects on taking damage
/// It can also add small gameplay effects like a knockback
/// </summary>
public abstract class IEffects : ScriptableObject
{
    public abstract IEffects Init(Transform transform);
    public abstract void TakeDamage();
}
