using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/OneTimeTemporalEffect")]
public abstract class OneTimeTemporalEffect : TemporalEffect
{
    public override bool DestroyEffect()
    {
        if (UpdateTime())
            return false;
        CancelEffect();
        return false;
    }

    protected abstract void CancelEffect();
}
