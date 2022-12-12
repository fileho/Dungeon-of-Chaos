using UnityEngine;

// Raw data for saves
[System.Serializable]
public class SavedStats
{
    public PrimaryStats.SavedPrimaryStats savedPrimary;
    public Levelling.SavedLevelling savedLevelling;
    public int resetBooks;

    public SavedStats(PrimaryStats.SavedPrimaryStats savedPrimary, Levelling.SavedLevelling savedLevelling, int resetBooks)
    {
        this.savedPrimary = savedPrimary;
        this.savedLevelling = savedLevelling;
        this.resetBooks = resetBooks;
    }
}

[CreateAssetMenu(menuName = "SO/Stats/Stats")]
public class Stats : ScriptableObject
{
    #region Stats Variables

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float chaseDistance;
    [SerializeField]
    private float staminaRegen;

    private float manaRegen = 0f;
    private float xpGainModifier = 1f;
    private float cooldownModifier = 1f;
    private float staminaCostModifier = 1f;
    private float manaCostModifier = 1f;

    [SerializeReference]
    private PrimaryStats primaryStats = new PrimaryStats();

    private RegenerableStat health = new RegenerableStat();
    private RegenerableStat mana = new RegenerableStat();
    private RegenerableStat stamina = new RegenerableStat();
    private float physicalDamage;
    private float spellPower;

    private float armor = 0;

    [SerializeField]
    private Levelling XP = new Levelling();
    #endregion

    private IBars bars;

    public int GetLevel()
    {
        return XP.GetLevel();
    }

    public Levelling GetLevellingData()
    {
        return XP;
    }

    public void ModifyCurrentXP(int value)
    {
        XP.ModifyCurrentXP(value);
        float ratio = (float)XP.GetCurrentXP() / (float)XP.GetNextLevelXP();
        bars.UpdateXpBar(ratio);
    }

    #region Skills Reset
    private int resetAmount = 0;

    public void ColectReset()
    {
        resetAmount++;
    }

    public void ConsumeReset()
    {
        resetAmount--;
    }

    public bool HasReset()
    {
        return resetAmount > 0;
    }

    public int GetResetAmount()
    {
        return resetAmount; 
    }
    #endregion

    #region Utils
    public float GetCooldownModifier()
    {
        return cooldownModifier;
    }

    public void ChangeCooldownModifier(float value)
    {
        cooldownModifier += value;
    }

    public float GetXPModifier()
    {
        return xpGainModifier;
    }

    public void ChangeXPModifier(float value)
    {
        xpGainModifier += value;
    }
    #endregion

    #region Health
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

    public float GetMaxHealth()
    {
        return health.maxValue;
    }

    public void ChangeMaxHealth(float value)
    {
        health.ChangeMaxValue(value);
        bars.UpdateHpBar(health.Ratio());
    }
    #endregion

    #region Mana
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

    public float GetMaxMana()
    {
        return mana.maxValue;
    }
    public void ChangeMaxMana(float value)
    {
        mana.ChangeMaxValue(value);
        bars.UpdateManaBar(mana.Ratio());
    }

    public float GetManaRegen()
    {
        return manaRegen;
    }

    public void ChangeManaRegen(float value)
    {
        manaRegen += value;
    }

    public float GetManaCostMod()
    {
        return manaCostModifier;
    }

    public void SetManaCostMod(float value)
    {
        manaCostModifier = value;
    }

    #endregion

    #region Stamina
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

    public float GetMaxStamina()
    {
        return stamina.maxValue;
    }
    public void ChangeMaxStamina(float value)
    {
        stamina.ChangeMaxValue(value);
        bars.UpdateStaminaBar(stamina.Ratio());
    }

    public float GetStaminaRegen()
    {
        return staminaRegen;
    }

    public void ChangeStaminaRegen(float value)
    {
        staminaRegen += value;
    }

    public float GetStaminaCostMod()
    {
        return staminaCostModifier;
    }

    public void SetStaminaCostMod(float value)
    {
        staminaCostModifier = value;
    }
    #endregion

    #region Movement
    public float MovementSpeed()
    {
        return movementSpeed;
    }

    public float ChaseDistance()
    {
        return chaseDistance;
    }
    #endregion

    #region Physical Damage
    public float GetPhysicalDamage()
    {
        return physicalDamage;
    }

    public void ChangePhysicalDamage(float value)
    {
        physicalDamage += value;
    }
    #endregion

    #region Spell Power
    public float GetSpellPower()
    {
        return spellPower;
    }  

    public void ChangeSpellPower(float value)
    {
        spellPower += value;
    }
    #endregion

    #region Armor
    public bool HasArmor()
    {
        return armor > 0;
    }

    public float GetArmor()
    {
        return armor;
    }

    public void SetArmor(float value)
    {
        armor += value;
        armor = Mathf.Max(armor, 0);
        // TODO Display/Hide/Update UI
    }
    #endregion

    #region Stats Updating
    public Stats ResetStats(IBars bars = null)
    {
        if (bars != null)
            this.bars = bars;

        UpdateStats();
        XP.SetNextLevelXP();

        health.Reset();
        mana.Reset();
        stamina.Reset();

        if (this.bars)
        {
            this.bars.FillAllBars();
            this.bars.UpdateXpBar((float)XP.GetCurrentXP() / XP.GetNextLevelXP());
        }
        return this;
    }

    public void UpdateStats()
    {
        physicalDamage = primaryStats.GetDamage(XP.GetLevel());
        spellPower = primaryStats.GetSpellPower(XP.GetLevel());
        health.maxValue = primaryStats.GetMaxHP(XP.GetLevel());
        stamina.maxValue = primaryStats.GetMaxStamina(XP.GetLevel());
        mana.maxValue = primaryStats.GetMaxMana(XP.GetLevel());
        staminaRegen = primaryStats.GetStaminaRegen();
        armor = primaryStats.GetArmor();
    }

    public void UpdateStatsUI()
    {
        if (StatsOverview.instance == null || SkillsUI.instance == null)
            return;
        XP.UpdateLevellingUI();
        StatsOverview.instance.SetStrength(primaryStats.strength);
        StatsOverview.instance.SetDamage(physicalDamage);
        StatsOverview.instance.SetIntelligence(primaryStats.intelligence);
        StatsOverview.instance.SetPower(spellPower);
        StatsOverview.instance.SetConstitution(primaryStats.constitution);
        StatsOverview.instance.SetHP(health.maxValue);
        StatsOverview.instance.SetEndurance(primaryStats.endurance);
        StatsOverview.instance.SetStamina(stamina.maxValue);
        StatsOverview.instance.SetWisdom(primaryStats.wisdom);
        StatsOverview.instance.SetMana(mana.maxValue);
        StatsOverview.instance.SetArmor(armor);
        StatsOverview.instance.SetStaminaRegen(staminaRegen);
    }
    #endregion

    #region Primary Stats
    public void IncreaseStrength()
    {
        primaryStats.strength++;
        XP.ConsumeStatsPoint();
        physicalDamage = primaryStats.GetDamage(XP.GetLevel());
        StatsOverview.instance.SetStrength(primaryStats.strength);
        StatsOverview.instance.SetDamage(physicalDamage);
    }

    public float GetStrength()
    {
        return primaryStats.strength;
    }

    public void IncreaseIntelligence()
    {
        primaryStats.intelligence++;
        XP.ConsumeStatsPoint();
        spellPower = primaryStats.GetSpellPower(XP.GetLevel());
        StatsOverview.instance.SetIntelligence(primaryStats.intelligence);
        StatsOverview.instance.SetPower(spellPower);
    }

    public float GetIntelligence()
    {
        return primaryStats.intelligence;
    }

    public void IncreaseConstitution()
    {
        primaryStats.constitution++;
        XP.ConsumeStatsPoint();
        health.maxValue = primaryStats.GetMaxHP(XP.GetLevel());
        armor = primaryStats.GetArmor();
        StatsOverview.instance.SetConstitution(primaryStats.constitution);
        StatsOverview.instance.SetHP(health.maxValue);
        StatsOverview.instance.SetArmor(armor);
    }

    public float GetConstitution()
    {
        return primaryStats.constitution;
    }

    public void IncreaseEndurance()
    {
        primaryStats.endurance++;
        stamina.maxValue = primaryStats.GetMaxStamina(XP.GetLevel());
        staminaRegen = primaryStats.GetStaminaRegen();
        XP.ConsumeStatsPoint();
        StatsOverview.instance.SetEndurance(primaryStats.endurance);
        StatsOverview.instance.SetStamina(stamina.maxValue);
        StatsOverview.instance.SetStaminaRegen(staminaRegen);
    }

    public float GetEndurance()
    {
        return primaryStats.endurance;
    }

    public void IncreaseWisdom()
    {
        primaryStats.wisdom++;
        XP.ConsumeStatsPoint();
        mana.maxValue = primaryStats.GetMaxMana(XP.GetLevel());
        StatsOverview.instance.SetWisdom(primaryStats.wisdom);
        StatsOverview.instance.SetMana(mana.maxValue);
    }

    public float GetWisdom()
    {
        return primaryStats.wisdom;
    }
    #endregion

    public SavedStats Save()
    {
        return new SavedStats(primaryStats.Save(), XP.Save(), resetAmount);
    }

    public void Load(SavedStats saved)
    {
        primaryStats.Load(saved.savedPrimary);
        XP.Load(saved.savedLevelling);
        resetAmount = saved.resetBooks;
        ResetStats();
    }
}
