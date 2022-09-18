using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/AlliesInRange")]
public class AlliesInRangeTargets : ITarget
{ 
    public override List<Unit> GetTargetUnits()
    {
        string layerName = LayerMask.LayerToName(targettingData.owner.gameObject.layer);
        LayerMask layer;
        if (layerName == "Enemy" || layerName == "EnemyAttack")
            layer = LayerMask.GetMask("Enemy");
        else if (layerName == "Player" || layerName == "PlayerAttack")
            layer = LayerMask.GetMask("Player");
        else
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
