using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill effect that temporarily increases attack
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/AttackBoost")]
public class AttackBoost : OneTimeTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangePhysicalDamage(val);
    }

    protected override void CancelEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangePhysicalDamage(-val);
    }

    protected override void Init()
    {
        InitStatusIcon(StatusEffectType.AttackBoost);
    }
}
