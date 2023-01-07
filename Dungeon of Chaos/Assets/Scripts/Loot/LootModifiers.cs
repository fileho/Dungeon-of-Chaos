using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loot/LootModifiers")]
public class LootModifiers : ScriptableObject
{
    private const int maxLevel = 35;
    [Header("XP Modifiers")]
    [SerializeField] private float xpMultiplier;
    [SerializeField] private float baseXP;
    private const float lvlXPMultiplier = 1.5f;

    [Header("Essence Drop Chance")]
    [SerializeField] private float essenceChance;
    [SerializeField] private float manaEssenceChanceMultiplier;
    private const float lvlEssenceChanceMultiplier = 1.08f;

    [Header("Essence Values")]
    [SerializeField] private float healthEssence;  
    [SerializeField] private float staminaEssence;
    [SerializeField] private float manaEssence;
    private const float essenceLvlMultiplier = 1.25f;

    private float LvlMultiplier(float multiplier, int lvl)
    {
        return Mathf.Pow(multiplier-lvl/((maxLevel/(multiplier-1))*2+1), lvl-1);
    }
    
    public float GetXPValue(int lvl)
    {
        return Mathf.Ceil(baseXP*xpMultiplier*LvlMultiplier(lvlXPMultiplier, lvl));
    }
    
    public float GetEssenceChance(int lvl)
    {
        return (essenceChance/100)*LvlMultiplier(lvlEssenceChanceMultiplier, lvl);
    }

    public float GetManaEssenceChance(int lvl)
    {
        return GetEssenceChance(lvl) * manaEssenceChanceMultiplier;
    }

    private float GetEssenceValue(int lvl, float baseEssence)
    {
        return Mathf.Ceil(baseEssence*LvlMultiplier(essenceLvlMultiplier, lvl));
    }

    public float GetHealthEssence(int lvl)
    {
        return GetEssenceValue(lvl, healthEssence);
    }

    public float GetStaminaEssence(int lvl)
    {
        return GetEssenceValue(lvl, staminaEssence);
    }

    public float GetManaEssence(int lvl)
    {
        return GetEssenceValue(lvl, manaEssence);
    }    
}
