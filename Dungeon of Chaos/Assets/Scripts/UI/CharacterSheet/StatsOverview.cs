using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class StatsOverview : MonoBehaviour
{
    [SerializeField] private GameObject xp;
    [SerializeField] private GameObject nextLevelXP;
    [SerializeField] private GameObject level;
    [SerializeField] private GameObject levelUpButton;
    [SerializeField] private GameObject statsPoints;
    [SerializeField] private GameObject skillPoints;

    [SerializeField] private List<GameObject> statsIncreaseButtons;
    [SerializeField] private List<StatUI> primaryStats;
    [SerializeField] private List<StatUI> secondaryStats;

    public static StatsOverview instance;

    private void Start()
    {
        instance = this;
        UpdateUI();
    }

    private void UpdateXP()
    {
        xp.GetComponent<TextMeshProUGUI>().text = Character.instance.stats.GetLevellingData().GetCurrentXP().ToString();
    }

    private void UpdateNextLevelXP()
    {
        nextLevelXP.GetComponent<TextMeshProUGUI>().text = Character.instance.stats.GetLevellingData().GetNextLevelXP().ToString();
    }

    private void UpdateLevel()
    {
        level.GetComponent<TextMeshProUGUI>().text = Character.instance.stats.GetLevel().ToString();
    }

    private void ShowLevelUpButton()
    {
        levelUpButton.SetActive(Character.instance.stats.GetLevellingData().CanLevelUp());
    }

    private void ShowStatsIncreaseButtons()
    {
        foreach (GameObject btn in statsIncreaseButtons)
        {
            btn.SetActive(Character.instance.stats.GetLevellingData().HasStatsPoints()); 
        }            
    }

    private void UpdateStatsPoints()
    {
       // Debug.Log("StatsPoints " + Character.instance.stats.GetLevellingData().GetStatsPoints().ToString());
        statsPoints.GetComponent<TextMeshProUGUI>().text = Character.instance.stats.GetLevellingData().GetStatsPoints().ToString();
    }

    private void UpdateSkillPoints()
    {
        if (SkillSystem.instance == null)
        {
            skillPoints.GetComponent<TextMeshProUGUI>().text = "0";
            return;
        }
        skillPoints.GetComponent<TextMeshProUGUI>().text = SkillSystem.instance.skillPoints.ToString();
    }

    public void UpdateUI()
    {
        Debug.Log("UpdateUI");
        UpdateXP();
        UpdateNextLevelXP();
        UpdateLevel();
        ShowLevelUpButton();
        ShowStatsIncreaseButtons();
        UpdateStatsPoints();
        UpdateSkillPoints();
        UpdateSecondaryStats();
        UpdatePrimaryStats();
    }

    public void LevelUp()
    {
        Character.instance.stats.GetLevellingData().LevelUp();
        UpdateUI();
    }

    public void IncreaseStat(UnityEvent changeStat, int index)
    {
        changeStat.Invoke();
        primaryStats[index].UpdateStat();
        secondaryStats[index].UpdateStat();
        UpdateUI();
    }

    private void UpdateSecondaryStats()
    {
        foreach (StatUI secondary in secondaryStats)
        {
            secondary.UpdateStat();
        }
    }

    private void UpdatePrimaryStats()
    {
        foreach (StatUI primary in primaryStats)
            primary.UpdateStat();
    }
}
