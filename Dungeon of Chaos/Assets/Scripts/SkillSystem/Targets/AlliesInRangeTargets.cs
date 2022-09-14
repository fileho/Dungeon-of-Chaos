using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/AlliesInRange")]
public class AlliesInRangeTargets : ITarget
{ 
    public override List<Unit> GetTargetUnits()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targettingData.position, targettingData.range, targettingData.owner.gameObject.layer);
        List<Unit> targets = new List<Unit>();
        foreach (var collider in hitColliders)
        {
            if (targettingData.owner.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                targets.Add(collider.gameObject.GetComponent<Enemy>());
            else
                targets.Add(collider.gameObject.GetComponent<Character>());
        }
        return targets;
    }
}
