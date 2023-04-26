using UnityEngine.UI;
using TMPro;
using UnityEngine;

/// <summary>
/// Class that handles the skills tab of the character sheet
/// </summary>
public class SkillsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resetPoints;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI skillTabSkillPoints;
    [SerializeField] private TextMeshProUGUI skillPoints;

    [SerializeField] private GameObject notification;


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
        int skillPts = stats.GetLevellingData().skillPoints;
        skillPoints.text = skillPts.ToString();
        skillTabSkillPoints.text = skillPts.ToString();
        notification.SetActive(skillPts > 0);
        UpdateSkillButtons();
    }

    private void UpdateResetPoints()
    {
        resetPoints.text = stats.GetResetAmount().ToString();
        resetButton.interactable = stats.HasReset();
    }


}
