using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/EnemiesInRange")]
public class EnemiesInRangeTarget : ITarget
{
    public override List<Vector2> GetTargetPositions()
    {
        return null;
    }

    public override List<Unit> GetTargetUnits()
    {
        int ownerLayer = targettingData.owner.gameObject.layer;
        int enemyLayer = GetEnemyLayer(ownerLayer);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targettingData.position, targettingData.range, enemyLayer);
        List<Unit> targets = new List<Unit>();
        foreach (var collider in hitColliders)
        {
            if (ownerLayer == LayerMask.NameToLayer("Enemy"))
                targets.Add(collider.gameObject.GetComponent<Enemy>());
            else
                targets.Add(collider.gameObject.GetComponent<Character>());
        }
        return targets;
    }
}
