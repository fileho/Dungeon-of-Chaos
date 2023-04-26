using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object assignable to skill effects. Skill effect then targets the user of the skill
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/Targets/SelfTarget")]
public class SelfTarget : ITarget
{
    public override List<Unit> GetTargetUnits()
    {
        return new List<Unit>() {targettingData.owner};
    }
}
