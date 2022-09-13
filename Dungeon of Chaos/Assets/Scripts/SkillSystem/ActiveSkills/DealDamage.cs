using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/DealDamage")]
public class DealDamage : ISkillEffect
{
    [SerializeField] private float damage;
    [SerializeField] private SkillEffectType skillEffectType;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        float dmg = skillEffectType == SkillEffectType.physical
            ? unit.stats.GetPhysicalDamage() * damage
            : unit.stats.GetSpellPower() * damage;
        foreach (Unit t in targets)
        {
            t.TakeDamage(dmg);
        }
    }
}
