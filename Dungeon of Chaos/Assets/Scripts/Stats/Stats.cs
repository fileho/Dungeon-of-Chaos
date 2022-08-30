using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stats")]
public class Stats : ScriptableObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float staminaRegen;
   
    [SerializeReference] private PrimaryStats primaryStats = new PrimaryStats();
    
    private RegenerableStat health = new RegenerableStat();
    private RegenerableStat mana = new RegenerableStat();
    private RegenerableStat stamina = new RegenerableStat();
    private float physicalDamage;
    private float spellPower;

    [SerializeField] private int level;
    private float xp;

    private IBars bars;

    public int GetLevel()
    {
        return level;
    }
    public void ConsumeHealth(float value)
    {
        health.Consume(value);
        bars.UpdateHpBar(health.Ratio());
    }

    public void RegenerateHealth(float value)
    {
        health.Regenerate(value);
        bars.UpdateHpBar(health.Ratio());
    }

    public bool IsDead()
    {
        return health.IsDepleted();
    }

    public bool HasMana(float value)
    {
        return mana.HasEnough(value);
    }

    public void ConsumeMana(float value)
    {
        mana.Consume(value);
        bars.UpdateManaBar(mana.Ratio());
    }

    public void RegenerateMana(float value)
    {
        mana.Regenerate(value);
        bars.UpdateManaBar(mana.Ratio());
    }

    public bool HasStamina(float value)
    {
        return stamina.HasEnough(value);
    }

    public void ConsumeStamina(float value)
    {
        stamina.Consume(value);
        bars.UpdateStaminaBar(stamina.Ratio());
    }

    public void RegenerateStamina(float value)
    {
        stamina.Regenerate(value);
        bars.UpdateStaminaBar(stamina.Ratio());
    }

    public float MovementSpeed()
    {
        return movementSpeed;
    }

    public float GetPhysicalDamage()
    {
        return physicalDamage;
    }

    public float GetSpellPower()
    {
        return spellPower;
    }

    public float GetStaminaRegen()
    {
        return staminaRegen;
    }

    public void ChangeStaminaRegen(float value)
    {
        staminaRegen += value;
    }

    public void ChangeMaxHealth(float value)
    {
        health.ChangeMaxValue(value);
        bars.UpdateHpBar(health.Ratio());
    }

    public void ChangeMaxStamina(float value)
    {
        stamina.ChangeMaxValue(value);
        bars.UpdateStaminaBar(stamina.Ratio());
    }

    public void ChangeMaxMana(float value)
    {
        mana.ChangeMaxValue(value);
        bars.UpdateManaBar(mana.Ratio());
    }

    public void ChangePhysicalDamage(float value)
    {
        physicalDamage += value;
    }

    public void ChangeSpellPower(float value)
    {
        spellPower += value;
    }

    public Stats ResetStats(IBars bars = null)
    {
        if (bars != null)
            this.bars = bars;

        UpdateStats();

        health.Reset();
        mana.Reset();
        stamina.Reset();

        if (this.bars) 
            this.bars.FillAllBars();

        return this;
    }

    public void UpdateStats()
    {
        physicalDamage = primaryStats.GetDamage(level);
        spellPower = primaryStats.GetSpellPower(level);
        health.maxValue = primaryStats.GetMaxHP(level);
        stamina.maxValue = primaryStats.GetMaxStamina(level);
        mana.maxValue = primaryStats.GetMaxMana(level);
    }
}
