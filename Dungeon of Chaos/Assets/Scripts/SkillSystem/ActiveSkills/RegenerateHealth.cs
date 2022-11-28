using UnityEngine;

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
}

