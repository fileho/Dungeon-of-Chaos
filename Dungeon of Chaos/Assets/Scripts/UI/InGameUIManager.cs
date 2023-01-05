using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Slider manaBar;
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private Slider xpBar;
    [SerializeField]
    private GameObject armorBar;

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
    [SerializeField]
    private Slider bossHPbar;
    [SerializeField]
    private TMP_Text bossName;

    public static InGameUIManager instance;

    [SerializeField]
    private GameObject settings;
    private SkillSystem skillSystem;

    // Delay after which it is possible to open the settings UI so it cannot be opened accidentally
    private float openUIstartDelay = 0.25f;

    private Color baseColor = new Color(0.2f, 0.2f, 0.2f);

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
        bossHPbar.value = 1;
        bossHPbar.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateSkills();

        if (openUIstartDelay > 0)
        {
            openUIstartDelay -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleSettings();
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

    public void SetXpBar(float value)
    {
        xpBar.value = value;
    }

    public void SetBossHPbar(float value)
    {
        bossHPbar.value = value;
    }

    public void StartBossFight(string bName)
    {
        bossHPbar.gameObject.SetActive(true);
        bossName.gameObject.SetActive(true);
        bossName.text = bName;
    }

    public void SetArmorBar(float value)
    {
        armorBar.GetComponent<Slider>().value = value;
        if (value > 0)
        {
            armorBar.SetActive(true);
            return;
        }

        armorBar.SetActive(false);
    }

    private void ResetIcon(Image image)
    {
        image.sprite = null;
        image.color = baseColor;
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
            {
                ResetIcon(activeSkills[i]);
                continue;
            }
            activeSkills[i].sprite = skill.GetSkillData().GetIcon();
            activeSkills[i].color = Color.white;
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
        {
            ResetIcon(secondarySkill);
            return;
        }
        secondarySkill.sprite = secondary.GetSkillData().GetIcon();
        secondarySkill.color = Color.white;
        secondaryCooldown.fillAmount = secondary.GetCurrentSkill().GetCooldownRatio();
    }

    public void ToggleSettings()
    {
        bool isActive = settings.activeSelf;

        if (!isActive)
        {
            Character.instance.BlockInput();
        }
        else
        {
            Character.instance.UnblockInput();
        }

        settings.SetActive(!isActive);
    }
}
