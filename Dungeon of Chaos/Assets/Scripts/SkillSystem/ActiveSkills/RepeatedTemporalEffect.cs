using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RepeatedTemporalEffect : TemporalEffect
{
    [SerializeField] protected float frequency;
    protected float time = 0f;

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { GetValue(owner).ToString(), frequency.ToString(), duration.ToString() };
    }

    public override bool Update()
    {
        if (!UpdateTime())
            return false;
        if (ShouldApplyEffect())
        {
            ApplyEffect();
            time -= frequency;
        }
        return true;
    }

    protected bool ShouldApplyEffect()
    {
        time += Time.deltaTime;
        return time >= frequency;
    }
}
