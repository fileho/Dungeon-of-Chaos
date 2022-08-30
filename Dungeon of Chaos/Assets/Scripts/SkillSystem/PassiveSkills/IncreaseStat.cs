using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/ChangeStatPassive")]
public class IncreaseStat : IPassiveSkill
{
    [SerializeField] private float amount;
    [SerializeField] private UnityEvent<float> changeStat;

    private float value;
    public override void Equip(Stats stats)
    {
        value = amount * stats.GetSpellPower();
        changeStat.Invoke(value);
    }

    public override void Unequip(Stats stats)
    {
        changeStat.Invoke(-value);
    }
}
