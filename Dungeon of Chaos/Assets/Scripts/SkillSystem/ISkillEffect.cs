using System.Collections.Generic;
using UnityEngine;

public enum SkillEffectType
{
    spell,
    physical
}
public abstract class ISkillEffect : ScriptableObject
{
    [Header("Targeting data")]
    [SerializeReference] protected ITarget target;
    [SerializeField] protected float range = float.MaxValue;
    [SerializeField] private float angle = 360;

    /// <summary>
    /// Applies skill effect on target given in parameters or specified by targeting data
    /// </summary>
    /// <param name="unit">unit using the skill</param>
    /// <param name="targets">list of target units (optional)</param>
    /// <param name="targetPositions">list of target positions (optional)</param>
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

    /// <summary>
    /// Applies skill effect on target specified by targeting data
    /// </summary>
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

    /// <summary>
    /// Returns an array of values that should be inserted into skill description
    /// </summary>
    public abstract string[] GetEffectsValues(Unit owner);
}
