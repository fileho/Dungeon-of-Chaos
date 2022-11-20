using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RepeatedTemporalEffect")]
public abstract class RepeatedTemporalEffect : TemporalEffect
{
    [SerializeField] private float frequency;
    private float time = 0f;

    public override bool DestroyEffect()
    {
        if (!UpdateTime())
            return true;
        if (ShouldApplyEffect())
        {
            ApplyEffect();
            time -= frequency;
        }
        return false;
    }

    protected bool ShouldApplyEffect()
    {
        time += Time.deltaTime;
        return time >= frequency;
    }
}
