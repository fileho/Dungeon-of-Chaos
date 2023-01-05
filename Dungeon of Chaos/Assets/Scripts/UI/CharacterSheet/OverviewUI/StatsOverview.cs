using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsOverview : MonoBehaviour
{
    [Header("Levelling UI")]
    [SerializeField] private TextMeshProUGUI xp;
    [SerializeField] private TextMeshProUGUI nextLevelXP;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private GameObject levelUpButton;
    [SerializeField] private TextMeshProUGUI statsPoints;
    [SerializeField] private List<GameObject> statsIncreaseButtons;

    [Header("Primary Stats Text")]
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI constitutionText;
    [SerializeField] private TextMeshProUGUI enduranceText;
    [SerializeField] private TextMeshProUGUI wisdomText;

    [Header("Secondary Stats Text")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI manaText;

    [Header("Tertiary Stats Text")]
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI staminaRegenText;

    public static StatsOverview instance;

    private Stats stats;

    private void Start()
    {
        instance = this;
        stats = Character.instance.stats;
        stats.UpdateStatsUI();
    }

    public void SetXP(float value)
    {
        SetStat(value, xp);
    }

    public void SetNextLevelXP(float value)
    {
        SetStat(value, nextLevelXP);
    }

    public void SetLevel(float value)
    {
        SetStat(value, level);
    }

    public void ShowLevelUpButton(bool show)
    {
        levelUpButton.SetActive(show);
    }

    public void ShowStatsIncreaseButtons(bool show)
    {
        foreach (GameObject btn in statsIncreaseButtons)
        {
            btn.SetActive(show && btn.GetComponent<UpgradeStatButton>().CanUpgrade()); 
        }            
    }

    public void SetStatsPoints(float value)
    {
        SetStat(value, statsPoints);
    }

    public void LevelUp()
    {
        stats.GetLevellingData().LevelUp();
    }

    private void SetStat(float value, TextMeshProUGUI textGUI)
    {
        textGUI.text = Math.Round(value,1).ToString();
    }

    public void SetStrength(float value)
    {
        SetStat(value, strengthText);
    }

    public void SetIntelligence(float value)
    {
        SetStat(value, intelligenceText);
    }

    public void SetConstitution(float value)
    {
        SetStat(value, constitutionText);
    }

    public void SetEndurance(float value)
    {
        SetStat(value, enduranceText);
    }

    public void SetWisdom(float value)
    {
        SetStat(value, wisdomText);
    }

    public void SetDamage(float value)
    {
        SetStat(value, damageText);
    }

    public void SetPower(float value)
    {
        SetStat(value, powerText);
    }

    public void SetHP(float value)
    {
        SetStat(value, hpText);
    }

    public void SetMana(float value)
    {
        SetStat(value, manaText);
    }
    public void SetStamina(float value)
    {
        SetStat(value, staminaText);
    }

    public void SetArmor(float value)
    {
        SetStat(value, armorText);
    }

    public void SetStaminaRegen(float value)
    {
        SetStat(value, staminaRegenText);
    }
}
