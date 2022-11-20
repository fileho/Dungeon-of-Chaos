using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : RepeatedTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.TakeDamage(val);
    }
}
