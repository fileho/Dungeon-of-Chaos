using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/EnemiesInRangedArea")]
public class EnemiesInRangeTarget : ITarget
{
    public override List<Unit> GetTargetUnits()
    {
        int ownerLayer = targettingData.owner.gameObject.layer;
        LayerMask enemyLayer = GetEnemyLayer(ownerLayer);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(targettingData.position, targettingData.range, enemyLayer);
        List<Unit> targets = new List<Unit>();
        Vector2 direction;
        if (ownerLayer == LayerMask.NameToLayer("Enemy") || ownerLayer == LayerMask.NameToLayer("EnemyAttack"))
            direction = ((Vector2)Character.instance.transform.position - targettingData.position).normalized;
        else
            direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - targettingData.position).normalized;
        foreach (var collider in hitColliders)
        {
            if (IsInCone(collider.gameObject.transform.position, direction))
            {
                if (ownerLayer == LayerMask.NameToLayer("Enemy") || ownerLayer == LayerMask.NameToLayer("EnemyAttack"))
                    targets.Add(collider.gameObject.GetComponent<Character>());
                else
                {
                    if (collider.gameObject.GetComponent<Enemy>() != null)
                        targets.Add(collider.gameObject.GetComponent<Enemy>());
                }
            }
        }
        return targets;
    }

    private bool IsInCone(Vector2 pos, Vector2 aimDir)
    {
        Vector2 posDir = (pos - targettingData.position).normalized;
        float angle = Vector2.Angle(aimDir, posDir);
        return angle <= targettingData.angle / 2;
    }
}
