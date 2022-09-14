using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/PushBack")]
public class PushBack : ISkillEffect
{
    [SerializeField] private float force;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        foreach (var t in targets)
        {
            var direction = (t.transform.position - unit.transform.position).normalized;
            t.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }
}
