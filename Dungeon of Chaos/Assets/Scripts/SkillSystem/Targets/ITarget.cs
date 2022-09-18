using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TargettingData
{
    public Unit owner;
    public float range;
    public Vector2 position;
}

public abstract class ITarget : ScriptableObject
{
    protected TargettingData targettingData;

    public void InitTargettingData(Unit owner, float range = float.MaxValue, Vector2 position = default(Vector2))
    {
        targettingData.owner = owner;
        targettingData.range = range;
        targettingData.position = position;
    }

    public virtual List<Unit> GetTargetUnits() { return null; }

    public virtual List<Vector2> GetTargetPositions() { return null; }

    public LayerMask GetEnemyLayer(int ownerLayer)
    {
        return (ownerLayer == LayerMask.NameToLayer("Enemy") || ownerLayer == LayerMask.NameToLayer("EnemyAttack"))
            ? LayerMask.GetMask("Player")
            : LayerMask.GetMask("Enemy");
    }
}
