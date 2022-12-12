using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/CharacterBars")]
public class CharacterBars : IBars
{
    public override IBars Init(Transform transform)
    {
        return this;
    }

    public override void UpdateArmorBar(float value)
    {
        InGameUIManager.instance.SetArmorBar(value);
    }

    public override void UpdateHpBar(float value)
    {
        InGameUIManager.instance.SetHealthBar(value);
    }

    public override void UpdateManaBar(float value)
    {
        InGameUIManager.instance.SetManaBar(value);
    }

    public override void UpdateStaminaBar(float value)
    {
        InGameUIManager.instance.SetStaminaBar(value);
    }

    public override void UpdateXpBar(float value)
    {
        InGameUIManager.instance.SetXpBar(value);
    }
}
