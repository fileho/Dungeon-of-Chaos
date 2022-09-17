using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Heal")]
public class HealSkill : RegenerableStatBuff
{
    protected override void ApplyEffect(Unit target, float val)
    {
        target.stats.RegenerateHealth(val);
    }
}
