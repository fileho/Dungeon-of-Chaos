using UnityEngine;

public abstract class IBars : ScriptableObject
{
    public abstract IBars Init(Transform transform);
    public abstract void UpdateHpBar(float value);
    public abstract void UpdateManaBar(float value);
    public abstract void UpdateStaminaBar(float value);

    public abstract void UpdateArmorBar(float value);

    public abstract void UpdateXpBar(float value);

    public void FillAllBars()
    {
        UpdateHpBar(1);
        UpdateManaBar(1);
        UpdateStaminaBar(1);
    }


}
