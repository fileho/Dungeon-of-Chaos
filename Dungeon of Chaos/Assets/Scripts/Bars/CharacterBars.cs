using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/CharacterBars")]
public class CharacterBars : IBars
{
    public override IBars Init(Transform transform)
    {
        return this;
    }

    public override void UpdateHpBar(float value)
    {
        UIManager.instance.SetHealthBar(value);
    }

    public override void UpdateManaBar(float value)
    {
        UIManager.instance.SetManaBar(value);
    }

    public override void UpdateStaminaBar(float value)
    {
        UIManager.instance.SetStaminaBar(value);
    }
}
