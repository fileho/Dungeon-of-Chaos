using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class OneTimeTemporalEffect : TemporalEffect
{
    public override bool Update()
    {
        if (UpdateTime())
            return false;
        CancelEffect();
        return true;
    }

    protected abstract void CancelEffect();
}
