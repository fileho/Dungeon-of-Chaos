using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/IncreaseXP")]
public class IncreaseXPIncome : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeXPModifier(val);
    }
}
