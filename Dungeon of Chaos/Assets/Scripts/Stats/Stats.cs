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

    private int level;
    private float xp;

    private IBars bars;

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
