using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;
    [SerializeField] private float dmg;

    [SerializeField] private GameObject prefab;


    public override void Use(Unit unit)
    {
        Instantiate(prefab, unit.transform.position, Quaternion.identity);
        var a =unit.stats.GetPhysicalDamage() + dmg;

    }
}
