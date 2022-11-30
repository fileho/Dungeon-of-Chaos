using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/ManaRegen")]
public class IncreaseManaRegen : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeManaRegen(val);
    }
}
