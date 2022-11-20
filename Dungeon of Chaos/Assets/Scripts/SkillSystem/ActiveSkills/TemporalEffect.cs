using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporalEffect : ISkillEffect
{
    [SerializeField] private float duration;
    [SerializeField] private float value;

    protected float timeLeft;
    protected float val;

    protected List<Unit> targets;

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { GetValue(owner).ToString() };
    }

    private float GetValue(Unit owner)
    {
        return value * owner.stats.GetSpellPower();
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        this.targets = targets;
        val = GetValue(unit);
        timeLeft = duration;
        ApplyEffect();
    }

    protected abstract void ApplyEffect();

    public abstract bool DestroyEffect();

    protected bool UpdateTime()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            return true;
        }
        return false;
    }
}
