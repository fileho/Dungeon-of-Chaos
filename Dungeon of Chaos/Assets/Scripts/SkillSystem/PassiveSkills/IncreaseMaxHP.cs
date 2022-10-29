using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/MaxHP")]
public class IncreaseMaxHP : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeMaxHealth(val);
    }
}
