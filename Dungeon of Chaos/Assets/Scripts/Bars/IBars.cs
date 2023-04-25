using UnityEngine;

/// <summary>
/// Manages bars on top of enemies, character status bars, and the boss hp bar
/// </summary>
public abstract class IBars : ScriptableObject
{
    public abstract IBars Init(Transform transform);
    // values are normalized to [0,1]
    public abstract void UpdateHpBar(float value);
    public abstract void UpdateManaBar(float value);
    public abstract void UpdateStaminaBar(float value);
    public abstract void UpdateArmorBar(float value);
    public abstract void UpdateXpBar(float value);

    // We do not want to refill armor and exp bar at start
    public void FillAllBars()
    {
        UpdateHpBar(1);
        UpdateManaBar(1);
        UpdateStaminaBar(1);
    }
}
