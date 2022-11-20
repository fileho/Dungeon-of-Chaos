using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoost : OneTimeTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.stats.SetArmor(val);
    }

    protected override void CancelEffect()
    {
        return;
    }
}
