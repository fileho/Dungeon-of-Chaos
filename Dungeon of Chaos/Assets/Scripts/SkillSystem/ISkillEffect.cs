using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillEffectType
{
    spell,
    physical
}
public abstract class ISkillEffect : ScriptableObject
{
    [SerializeReference] protected ITarget target;
    [SerializeField] protected float range = float.MaxValue;
    [SerializeField] private float angle = 360;

    public void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    {
        if (targets == null && targetPositions == null)
        {
            Apply(unit);
            return;
        }
        if (targets != null)
        {
            ApplyOnTargets(unit, targets);
            return;
        }

        ApplyOnPositions(unit, targetPositions);
    }

    protected virtual void ApplyOnTargets(Unit unit, List<Unit> targets) { ; }

    protected virtual void ApplyOnPositions(Unit unit, List<Vector2> targetPositions) { ; }

    protected virtual void Apply(Unit unit)
    {
        target.InitTargettingData(unit, range, unit.transform.position, angle);
        var targets = target.GetTargetUnits();
        if (targets != null)
        {
            ApplyOnTargets(unit, targets);
            return;
        }
        var targetPositions = target.GetTargetPositions();
        ApplyOnPositions(unit, targetPositions);        
    }

    public abstract string[] GetEffectsValues(Unit owner);
}
