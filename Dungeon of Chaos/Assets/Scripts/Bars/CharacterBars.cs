using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/CharacterBars")]
public class CharacterBars : IBars
{
    private HealthIndicator healthIndicator;
    public override IBars Init(Transform transform)
    {
        healthIndicator = FindObjectOfType<HealthIndicator>();
        return this;
    }

    public override void UpdateArmorBar(float value)
    {
        InGameUIManager.instance.SetArmorBar(value);
    }

    public override void UpdateHpBar(float value)
    {
        // Damage effects
        if (healthIndicator != null)
            healthIndicator.Change(Mathf.Clamp01(1 - value * 2));
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
