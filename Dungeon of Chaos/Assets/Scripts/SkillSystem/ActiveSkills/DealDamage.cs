using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/DealDamage")]
public class DealDamage : ISkillEffect
{
    [SerializeField] private float damage;
    [SerializeField] private SkillEffectType skillEffectType;
    [SerializeField] private SoundSettings dmgSFX;

    private float GetValue(Unit unit)
    {
        return skillEffectType == SkillEffectType.physical
            ? unit.stats.GetPhysicalDamage() * damage
            : unit.stats.GetSpellPower() * damage;
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        float dmg = skillEffectType == SkillEffectType.physical
            ? unit.stats.GetPhysicalDamage() * damage
            : unit.stats.GetSpellPower() * damage;
        
        foreach (Unit t in targets)
        {
            t.TakeDamage(dmg);
        }

        SoundManager.instance.PlaySound(dmgSFX);
    }

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { GetValue(owner).ToString() };
    }
}
