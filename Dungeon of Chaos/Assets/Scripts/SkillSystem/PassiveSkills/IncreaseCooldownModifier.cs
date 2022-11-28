using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/CooldownModifier")]
public class IncreaseCooldownModifier : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeCooldownModifier(val);
    }
}
