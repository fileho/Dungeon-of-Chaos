using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPowerBoost : OneTimeTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangeSpellPower(val);
    }

    protected override void CancelEffect()
    {
        foreach (Unit target in targets)
            target.stats.ChangeSpellPower(-val);
    }
}
