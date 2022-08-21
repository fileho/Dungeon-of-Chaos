using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryStats
{
    [Header("Primary Stats")]
    public float strength;
    public float intelligence;
    public float constitution;
    public float endurance;
    public float wisdom;

    [Header("Primary to Secondary Multipliers")]
    public float damageMultiplier;
    public float spellPowerMultiplier;
    public float hpMultiplier;
    public float staminaMultiplier;
    public float manaMultiplier;

    [Header("Stats Modifier Scriptable Object")]
    [Tooltip("Only for enemies, leave empty for character")]
    [SerializeField] private StatsModifiers statsModifiers;

    private float ModifiedStat(int l, float value)
    {
        if (statsModifiers != null)
            value += statsModifiers.GetStatsBonus() + (l - 1) * statsModifiers.GetLvlBonus();
        return value;
    }
    public float GetDamage(int l)
    {
        float s = ModifiedStat(l, strength);
        return s * damageMultiplier;
    }

    public float GetSpellPower(int l)
    {
        float i = ModifiedStat(l, intelligence);
        return i * spellPowerMultiplier;
    }

    public float GetMaxHP(int l)
    {
        float c = ModifiedStat(l, constitution);
        return c * hpMultiplier;
    }

    public float GetMaxStamina(int l)
    {
        float e = ModifiedStat(l, endurance);
        return e * staminaMultiplier;
    }

    public float GetMaxMana(int l)
    {
        float w = ModifiedStat(l, wisdom);
        return w * manaMultiplier;
    }
}
