using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Projectile")]
public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;
    [SerializeField] private float dmg;

    [SerializeField] private SkillEffectType skillEffectType;

    [SerializeField] private Projectile prefab;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        return;
    }

    protected override void Apply(Unit unit)
    {
        var projectile = Instantiate(prefab, unit.transform.position, Quaternion.identity);
        float damage = skillEffectType == SkillEffectType.physical ? unit.stats.GetPhysicalDamage() * dmg 
            : unit.stats.GetSpellPower()*dmg;
        projectile.SetStats(damage, speed, unit.transform);
    }
}
