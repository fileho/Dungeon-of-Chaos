using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Burn")]
public class Burn : RepeatedTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.TakeDamage(val);
    }
}
