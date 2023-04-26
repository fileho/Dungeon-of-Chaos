using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill effect that heals the target units
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Heal")]
public class HealSkill : RegenerableStatBuff
{
    protected override void ApplyEffect(Unit target, float val)
    {
        target.stats.RegenerateHealth(val);
    }
}
