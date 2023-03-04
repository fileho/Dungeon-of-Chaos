using UnityEngine;

/// <summary>
/// Visual effects on taking damage
/// </summary>
public abstract class IEffects : ScriptableObject
{
    public abstract IEffects Init(Transform transform);
    public abstract void TakeDamage();
}
