using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/PhysicalDamage")]
public class IncreasePhysicalDamage : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangePhysicalDamage(val);
    }
}
