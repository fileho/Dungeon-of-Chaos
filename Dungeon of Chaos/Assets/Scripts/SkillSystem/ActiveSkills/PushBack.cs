using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skill effect that pushes back the target units
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/PushBack")]
public class PushBack : ISkillEffect
{
    [SerializeField] private float force;

    public override string[] GetEffectsValues(Unit owner)
    {
        return null;
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        foreach (var t in targets)
        {
            var direction = (t.transform.position - unit.transform.position).normalized;
            t.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }
}
