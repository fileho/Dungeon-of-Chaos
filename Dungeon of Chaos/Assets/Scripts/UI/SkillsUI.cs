using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resetPoints;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI skillTabSkillPoints;
    [SerializeField] private TextMeshProUGUI skillPoints;


    private SkillSlotDash skillSlotDash;
    private SkillSlotSecondary skillSlotSecondary;
    private ActivatedSkillSlots activatedSkillSlots;

    private SkillButtonActive[] skillButtonsActive;
    private SkillButtonDash[] skillButtonsDash;
    private SkillButtonSecondary[] skillButtonsSecondary;
    private SkillButtonPassive[] skillButtonsPassive;

    private SkillSystem skillSystem;
    private Stats stats;

    public static SkillsUI instance;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (instance != null)
            return;
        instance = this;
        skillSystem = Character.instance.SkillSystem;
        stats = Character.instance.stats;
    }
    public void UpdateSkillsUI()
    {
        Init();
        UpdateSkillButtons();
        UpdateSkillSlots();
        UpdateSkillPoints();
        UpdateResetPoints();
    }

    private void UpdateSkillButtons()
    {
        skillButtonsActive = FindObjectsOfType<SkillButtonActive>();
        foreach (SkillButtonActive skillBtn in skillButtonsActive)
            skillBtn.Init();

        skillButtonsDash = FindObjectsOfType<SkillButtonDash>();
        foreach (SkillButtonDash skillBtn in skillButtonsDash)
            skillBtn.Init();

        skillButtonsSecondary = FindObjectsOfType<SkillButtonSecondary>();
        foreach (SkillButtonSecondary skillBtn in skillButtonsSecondary)
            skillBtn.Init();

        skillButtonsPassive = FindObjectsOfType<SkillButtonPassive>();
        foreach (SkillButtonPassive skillBtn in skillButtonsPassive)
            skillBtn.Init();
    }

    private void UpdateSkillSlots()
    {
        activatedSkillSlots = FindObjectOfType<ActivatedSkillSlots>();
        activatedSkillSlots.Init();

        skillSlotDash = FindObjectOfType<SkillSlotDash>();
        skillSlotDash.Init();

        skillSlotSecondary = FindObjectOfType<SkillSlotSecondary>();
        skillSlotSecondary.Init();
    }


    public void ResetSkills()
    {
        skillSystem.ResetSkills();
        UpdateSkillsUI();
    }

    public void UpdateSkillPoints()
    {
        skillPoints.text = stats.GetLevellingData().skillPoints.ToString();
        skillTabSkillPoints.text = stats.GetLevellingData().skillPoints.ToString();
    }

    private void UpdateResetPoints()
    {
        resetPoints.text = stats.GetResetAmount().ToString();
        resetButton.interactable = stats.GetResetAmount() > 0;
    }


}
