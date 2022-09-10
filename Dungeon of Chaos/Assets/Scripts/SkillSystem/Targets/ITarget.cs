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

    public abstract List<Unit> GetTargetUnits();

    public abstract List<Vector2> GetTargetPositions();

    public int GetEnemyLayer(int ownerLayer)
    {
        return ownerLayer == LayerMask.NameToLayer("Enemy")
            ? LayerMask.NameToLayer("Player")
            : LayerMask.NameToLayer("Enemy");
    }
}
