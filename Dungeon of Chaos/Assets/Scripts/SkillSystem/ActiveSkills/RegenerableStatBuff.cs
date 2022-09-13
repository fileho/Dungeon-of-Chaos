using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RegenerableStatBuff")]
public abstract class RegenerableStatBuff : ISkillEffect
{
    [SerializeField] private float value;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        float val = value * unit.stats.GetSpellPower();
        foreach (Unit t in targets)
            ApplyEffect(t, val);
    }

    protected abstract void ApplyEffect(Unit target, float val);
}
