using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporalEffect : ISkillEffect
{
    [SerializeField] protected float duration;
    [SerializeField] protected float value;

    protected float timeLeft;
    protected float val;

    protected List<Unit> targets;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        this.targets = targets;
        val = value * unit.stats.GetSpellPower();
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
