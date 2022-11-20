using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoost : OneTimeTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangePhysicalDamage(val);
    }

    protected override void CancelEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangePhysicalDamage(-val);
    }
}
