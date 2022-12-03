using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/WeaponEffect")]
public class ApplyEffectOnWeapon : OneTimeTemporalEffect
{
    [SerializeField] private ISkillEffect effect;

    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.gameObject.GetComponentInChildren<Weapon>().SetEffect(effect);
    }

    protected override void CancelEffect()
    {
        foreach (Unit target in targets)
        {
            if (target != null)
            target.gameObject.GetComponentInChildren<Weapon>().SetEffect(null);
        }
    }
}
