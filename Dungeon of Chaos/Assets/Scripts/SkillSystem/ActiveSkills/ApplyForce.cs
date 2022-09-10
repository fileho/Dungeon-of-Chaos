using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/ApplyForce")]
public class ApplyForce : ISkillEffect
{
    [SerializeField] private float force;
    [SerializeField] private float range;

    public override void Use(Unit unit)
    {
        target.InitTargettingData(unit, range, unit.transform.position);
        foreach (var t in target.GetTargetUnits())
        {
            var direction = (t.transform.position - unit.transform.position).normalized;
            t.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }
}
