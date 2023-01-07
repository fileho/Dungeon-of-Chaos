using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/WeaponEffect")]
public class ApplyEffectOnWeapon : OneTimeTemporalEffect
{
    [SerializeField] private ISkillEffect effect;

    public override string[] GetEffectsValues(Unit owner)
    {
        string[] effects = new string[] {duration.ToString(), Math.Round(GetValue(owner),2).ToString() };
        string[] e = effect.GetEffectsValues(owner);
        effects.CopyTo(e, 1);
        return e;
    }

    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
        {
            target.gameObject.GetComponentInChildren<Weapon>().SetEffect(effect);
            target.gameObject.GetComponentInChildren<Weapon>().ChangeDamageBoost(val);
        }
    }

    protected override void CancelEffect()
    {
        foreach (Unit target in targets)
        {
            if (target != null)
            {
                target.gameObject.GetComponentInChildren<Weapon>().SetEffect(null);
                target.gameObject.GetComponentInChildren<Weapon>().ChangeDamageBoost(-val);
            }
        }
    }
    protected override void Init()
    {
        InitStatusIcon(StatusEffectType.FlamingSword);
    }
}
