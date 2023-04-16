using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Resurrect")]
public class Resurrect : ISkillEffect
{
    [SerializeField] private float value;
    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { Math.Round(GetValue(owner), 1).ToString() };
    }

    private float GetValue(Unit owner)
    {
        return value * owner.stats.GetMaxHealth();
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        foreach (Unit t in targets)
        {
            float val = GetValue(t);
            t.stats.RegenerateHealth(val);
        }
    }
}
