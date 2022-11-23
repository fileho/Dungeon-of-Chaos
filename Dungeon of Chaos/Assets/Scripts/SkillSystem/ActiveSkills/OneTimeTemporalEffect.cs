using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
