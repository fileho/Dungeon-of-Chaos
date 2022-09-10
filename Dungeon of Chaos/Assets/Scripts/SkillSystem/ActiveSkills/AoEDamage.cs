using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/AoEDamage")]
public class AoEDamage : ISkillEffect
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private SkillEffectType skillEffectType;

    public override void Use(Unit unit)
    {
        target.InitTargettingData(unit, range, unit.transform.position);
        float dmg = skillEffectType == SkillEffectType.physical 
            ? unit.stats.GetPhysicalDamage() * damage
            : unit.stats.GetSpellPower() * damage;
        foreach (Unit t in target.GetTargetUnits())
        {
            t.TakeDamage(dmg);
        }
    } 
}
