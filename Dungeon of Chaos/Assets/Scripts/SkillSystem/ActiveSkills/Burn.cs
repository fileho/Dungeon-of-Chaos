using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill effect that deals damage over time
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Burn")]
public class Burn : RepeatedTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.TakeDamage(val);
    }

    protected override void Init()
    {
        InitStatusIcon(StatusEffectType.Burn);
    }
}
