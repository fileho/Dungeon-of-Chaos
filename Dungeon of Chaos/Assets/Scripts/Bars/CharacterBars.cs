using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/CharacterBars")]
public class CharacterBars : IBars
{
    private Stats stats;
    public override IBars Init(Transform transform, Stats stats)
    {
        this.stats = stats;

        return this;
    }

    public override void UpdateHpBar()
    {
        UIManager.instance.SetHealthBar(stats.HpRatio());
    }

    public override void UpdateManaBar()
    {
        UIManager.instance.SetManaBar(stats.ManaRatio());
    }

    public override void UpdateStaminaBar()
    {
        UIManager.instance.SetStaminaBar(stats.StaminaRatio());
    }
}
