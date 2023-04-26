using UnityEngine;

/// <summary>
/// Skill effect that regenerates health over time
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RegenerateHealth")]
public class RegenerateHealth : RepeatedTemporalEffect
{
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
        {
            target.stats.RegenerateHealth(val);
        }
    }

    protected override void Init()
    {
        InitStatusIcon(StatusEffectType.Regeneration);
    }
}

