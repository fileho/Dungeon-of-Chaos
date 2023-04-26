using System;
using UnityEngine;

/// <summary>
/// Skill effect (such as burn) applied to weapon
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/WeaponEffect")]
public class ApplyEffectOnWeapon : OneTimeTemporalEffect
{
    [SerializeField] private ISkillEffect effect;

    public override string[] GetEffectsValues(Unit owner)
    {
        string[] effects = new string[] {duration.ToString(), Math.Round(GetValue(owner),2).ToString(), value.ToString() };
        string[] e = effect.GetEffectsValues(owner);
        string[] result = new string[effects.Length + 2];
        Array.Copy(effects, result, effects.Length);
        Array.Copy(e, 0, result, effects.Length, 2);
        return result;
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
