using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/Armor")]
public class IncreaseArmor : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.SetArmor(val);
    }
}
