using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/ArmorBoost")]
public class ArmorBoost : OneTimeTemporalEffect
{
    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { Math.Round(GetValue(owner),2).ToString(), value.ToString()};
    }
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.stats.SetArmor(val);
    }

    protected override void CancelEffect()
    {
        return;
    }

    protected override void Init()
    {
        return;
    }
}
