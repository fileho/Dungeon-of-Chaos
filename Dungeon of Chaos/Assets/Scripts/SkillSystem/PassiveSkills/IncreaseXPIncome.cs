using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/XPModifier")]
public class IncreaseXPModifier : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeXPModifier(val);
    }
}
