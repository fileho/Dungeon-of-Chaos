using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/StaminaRegen")]
public class IncreaseStaminaRegen : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeStaminaRegen(val);
    }
}
