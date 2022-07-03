using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName = "SO/Stats")]
public class Stats : ScriptableObject
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;

    [SerializeField] private float movementSpeed;
    
    private float hp;
    private float mana;
    private float stamina;

    public void ConsumeHealth(float value)
    {
        hp = Consume(hp, value);
    }

    public void RegenerateHealth(float value)
    {
        hp = Regenerate(hp, maxHP, value);
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    public bool HasMana(float value)
    {
        return mana >= value;
    }

    public void ConsumeMana(float value)
    {
        mana = Consume(mana, value);
    }

    public void RegenerateMana(float value)
    {
        mana = Regenerate(mana, maxMana, value);
    }

    public bool HasStamina(float value)
    {
        return stamina >= value;
    }

    public void ConsumeStamina(float value)
    {
        stamina = Consume(stamina, value);
    }

    public void RegenerateStamina(float value)
    {
        stamina = Regenerate(stamina, maxStamina, value);
    }

    private static float Consume(float stat, float value)
    {
        stat -= value;
        stat = Mathf.Max(stat, 0);
        return stat;
    }

    private static float Regenerate(float stat, float maxStat, float value)
    {
        stat += value;
        stat = Mathf.Min(stat, maxStat);
        return stat;
    }

    public float MovementSpeed()
    {
        return movementSpeed;
    }


    public float HpRatio()
    {
        return hp / maxHP;
    }

    public float StaminaRatio()
    {
        return stamina / maxStamina;
    }

    public float ManaRatio()
    {
        return mana / maxMana;
    }


    public void ResetStats()
    {
        hp = maxHP;
        mana = maxMana;
        stamina = maxStamina;
    }
}
