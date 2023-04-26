using UnityEngine;

/// <summary>
/// Skill effect that temporarily buffs spell power
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/SpellPowerBoost")]
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

    protected override void Init()
    {
        InitStatusIcon(StatusEffectType.SpellPowerBoost);
    }
}
