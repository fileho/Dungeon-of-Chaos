using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Slider manaBar;
    [SerializeField]
    private Slider staminaBar;

    [Header("Skills")]
    [SerializeField]
    private List<Image> activeSkills;
    [SerializeField]
    private Image dashSkill;
    [SerializeField]
    private Image secondarySkill;

    [Header("Cooldowns")]
    [SerializeField]
    private List<Image> activeCooldowns;
    [SerializeField]
    private Image dashCooldown;
    [SerializeField]
    private Image secondaryCooldown;

    public static InGameUIManager instance;

    private SkillSystem skillSystem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
    }

    private void Update()
    {
        UpdateSkills();
    }

    public void SetHealthBar(float value)
    {
        healthBar.value = value;
    }
    public void SetManaBar(float value)
    {
        manaBar.value = value;
    }

    public void SetStaminaBar(float value)
    {
        staminaBar.value = value;
    }

    public void UpdateSkills()
    {
        UpdateActiveSkills();
        UpdateDash();
        UpdateSecondary();
    }

    private void UpdateActiveSkills()
    {
        for (int i = 0; i < 5; i++)
        {
            var skill = skillSystem.GetActivatedSkill(i);
            if (!skill)
                continue;
            activeSkills[i].sprite = skill.GetSkillData().GetIcon();
            activeCooldowns[i].fillAmount = skill.GetCurrentSkill().GetCooldownRatio();
        }
    }

    private void UpdateDash()
    {
        var dash = skillSystem.GetActivatedDash();

        dashSkill.sprite = dash.GetSkillData().GetIcon();
        dashCooldown.fillAmount = dash.GetCurrentSkill().GetCooldownRatio();
    }

    private void UpdateSecondary()
    {
        var secondary = skillSystem.GetActivatedSecondary();

        if (!secondary)
            return;
        secondarySkill.sprite = secondary.GetSkillData().GetIcon();
        secondaryCooldown.fillAmount = secondary.GetCurrentSkill().GetCooldownRatio();
    }
}
