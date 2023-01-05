using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class RegenerableStatBuff : ISkillEffect
{
    [SerializeField] private float value;

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[]{ Math.Round(GetValue(owner),2).ToString()};
    }

    private float GetValue(Unit owner)
    {
        return value * owner.stats.GetSpellPower();
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        float val = GetValue(unit);
        foreach (Unit t in targets)
            ApplyEffect(t, val);
    }

    protected abstract void ApplyEffect(Unit target, float val);
}
