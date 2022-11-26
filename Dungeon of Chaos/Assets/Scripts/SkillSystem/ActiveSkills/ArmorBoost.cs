using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/ArmorBoost")]
public class ArmorBoost : OneTimeTemporalEffect
{
    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { GetValue(owner).ToString()};
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
}
