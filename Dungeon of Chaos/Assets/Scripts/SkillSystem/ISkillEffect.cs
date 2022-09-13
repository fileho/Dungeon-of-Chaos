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
    [SerializeField] protected float range;
   
    public void Use(Unit unit, List<Unit> targets = null)
    {
        if (targets == null)
        {
            Apply(unit);
            return;
        }

        ApplyOnTargets(unit, targets);
    }

    protected abstract void ApplyOnTargets(Unit unit, List<Unit> targets);

    protected virtual void Apply(Unit unit)
    {
        target.InitTargettingData(unit, range, unit.transform.position);
        var targets = target.GetTargetUnits();
        ApplyOnTargets(unit, targets);
    }

    
}
