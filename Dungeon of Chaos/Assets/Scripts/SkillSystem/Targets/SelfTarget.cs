using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/SelfTarget")]
public class SelfTarget : ITarget
{
    public override List<Vector2> GetTargetPositions()
    {
        return null;
    }

    public override List<Unit> GetTargetUnits()
    {
        return new List<Unit>() {targettingData.owner};
    }
}
