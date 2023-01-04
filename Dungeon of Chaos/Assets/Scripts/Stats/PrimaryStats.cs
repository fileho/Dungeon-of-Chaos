using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryStats
{
    [System.Serializable]
    public class SavedPrimaryStats
    {
        public float strength;
        public float intelligence;
        public float constitution;
        public float endurance;
        public float wisdom;

        public SavedPrimaryStats(float strength, float intelligence, float constitution, float endurance, float wisdom)
        {
            this.strength = strength;
            this.intelligence = intelligence;
            this.constitution = constitution;
            this.endurance = endurance;
            this.wisdom = wisdom;
        }
    }

    [Header("Primary Stats")]
    public float strength;
    public float intelligence;
    public float constitution;
    public float endurance;
    public float wisdom;

    [Header("")]
    [SerializeField]
    PrimaryToSecondary multipliers;

    [Header("Stats Modifier Scriptable Object")]
    [Tooltip("Only for enemies, leave empty for character")]
    [SerializeField]
    private StatsModifiers statsModifiers;

    private float ModifiedStat(int l, float value)
    {
        if (statsModifiers != null)
            value += statsModifiers.GetStatsBonus() + (l - 1) * statsModifiers.GetLvlBonus();
        return value;
    }

    private float ModifiedHP(int l, float value)
    {
        if (statsModifiers == null)
            return value;
        return ModifiedStat(l, value) * statsModifiers.GetHPMultiplier();
    }
    public float GetDamage(int l)
    {
        float s = ModifiedStat(l, strength);
        return s * multipliers.damageMultiplier;
    }

    public float GetSpellPower(int l)
    {
        float i = ModifiedStat(l, intelligence);
        return i * multipliers.spellPowerMultiplier;
    }

    public float GetMaxHP(int l)
    {
        float c = ModifiedHP(l, constitution);
        return c * multipliers.hpMultiplier;
    }

    public float GetMaxStamina(int l)
    {
        float e = ModifiedStat(l, endurance);
        return e * multipliers.staminaMultiplier;
    }

    public float GetMaxMana(int l)
    {
        float w = ModifiedStat(l, wisdom);
        return w * multipliers.manaMultiplier;
    }

    public float GetStaminaRegen()
    {
        return Mathf.Floor(endurance / 3) * 6;
    }

    public float GetArmor()
    {
        return (constitution - 10) / 2 * 5; 
    }

    public void Load(SavedPrimaryStats saved)
    {
        strength = saved.strength;
        intelligence = saved.intelligence;
        constitution = saved.constitution;
        endurance = saved.endurance;
        wisdom = saved.wisdom;
    }

    public SavedPrimaryStats Save()
    {
        return new SavedPrimaryStats(strength, intelligence, constitution, endurance, wisdom);
    }
}
