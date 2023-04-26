using System;
using UnityEngine;

/// <summary>
/// Abstract class for effects that are applied over time (such as burn or regeneration)
/// </summary>
public abstract class RepeatedTemporalEffect : TemporalEffect
{
    [SerializeField] protected float frequency;
    protected float time = 0f;

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { Math.Round(GetValue(owner),2).ToString(), value.ToString(), frequency.ToString(), duration.ToString() };
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
