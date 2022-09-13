using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RepeatedTemporalEffect")]
public abstract class RepeatedTemporalEffect : TemporalEffect
{
    [SerializeField] private float frequency;

    public override bool DestroyEffect()
    {
        if (!UpdateTime())
            return true;
        if (ShouldApplyEffect())
            ApplyEffect();
        return false;
    }

    protected bool ShouldApplyEffect()
    {
        return (duration - timeLeft) % frequency == 0f;
    }
}
