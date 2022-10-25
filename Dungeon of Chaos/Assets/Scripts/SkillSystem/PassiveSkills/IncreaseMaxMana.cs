using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/Mana")]
public class IncreaseMaxMana : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeMaxMana(val);
    }
}
