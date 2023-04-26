using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object assignable to skill effects. Skill effect then targets all allies in range from a specified point
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/Targets/AlliesInRange")]
public class AlliesInRangeTargets : ITarget
{ 
    public override List<Unit> GetTargetUnits()
    {
        LayerMask layer = GetAllyLayer(targettingData.owner.gameObject.layer);
        if (layer == -1)
            return null;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targettingData.position, targettingData.range, layer);
        List<Unit> targets = new List<Unit>();
        foreach (var collider in hitColliders)
        {
            if (targettingData.owner.gameObject.layer == LayerMask.NameToLayer("Enemy") || targettingData.owner.gameObject.layer == LayerMask.NameToLayer("EnemyAttack"))
                targets.Add(collider.gameObject.GetComponent<Enemy>());
            else
                targets.Add(collider.gameObject.GetComponent<Character>());
        }
        return targets;
    }
}
