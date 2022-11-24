using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/StaminaImprovement")]
public class StaminaImprovement : RegenerableStatImprovement
{
    protected override void ChangeStat(Stats stats, float reg, float c)
    {
        stats.ChangeStaminaRegen(reg);
        stats.SetStaminaCostMod(c);
    }
}
