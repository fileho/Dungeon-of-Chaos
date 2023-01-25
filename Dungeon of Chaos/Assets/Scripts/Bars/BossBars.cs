using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/BossBars")]
public class BossBars : IBars
{
    public override IBars Init(Transform transform)
    {
        return this;
    }

    public override void UpdateHpBar(float value)
    {
        if (value <= 0)
        {
            InGameUIManager.instance.EndBossFight();
            return;
        }
        InGameUIManager.instance.SetBossHPbar(value);
    }

    public override void UpdateManaBar(float value)
    {
    }

    public override void UpdateStaminaBar(float value)
    {
    }

    public override void UpdateArmorBar(float value)
    {
    }

    public override void UpdateXpBar(float value)
    {
    }
}
