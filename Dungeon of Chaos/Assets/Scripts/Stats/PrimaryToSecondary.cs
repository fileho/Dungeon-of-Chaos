using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stats/StatsMultipliers")]
public class PrimaryToSecondary : ScriptableObject
{
    [Header("Primary to Secondary Multipliers")]
    public float damageMultiplier;
    public float spellPowerMultiplier;
    public float hpMultiplier;
    public float staminaMultiplier;
    public float manaMultiplier;
}
