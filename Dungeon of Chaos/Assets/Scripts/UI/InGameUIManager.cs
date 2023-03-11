using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    [Header("Bars")]
    [SerializeField]
    private Slider healthBar;
    [SerializeField] private Image healthBarPartialFillImage;
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

    [Space]
    [SerializeField]
    private Slider bossHPbar;
    [SerializeField]
    private TMP_Text bossName;

    public static InGameUIManager instance;

    [SerializeField]
    private GameObject settings;
    [SerializeField]
    private UIView settingsView;

    [SerializeField]
    private GameObject panelPopUp;
    private UIView panelPopUpView => panelPopUp.transform.GetChild(0).GetComponent<UIView>();

    [SerializeField]
    private SoundSettings lowNotificationSound;

    private SkillSystem skillSystem;

    private CanvasGroup manaCanvasGroup;
    private CanvasGroup staminaCanvasGroup;

    // Delay after which it is possible to open the settings UI so it cannot be opened accidentally
    private float openUIstartDelay = 0.25f;

    private Color baseColor = new Color(0.2f, 0.2f, 0.2f);

    private Tweener healthBarAnimationTween;

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
        manaCanvasGroup = manaBar.GetComponent<CanvasGroup>();
        staminaCanvasGroup = staminaBar.GetComponent<CanvasGroup>();
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

    private Tweener PlayBarAnimation(Image fillImage, float value, Tweener tween)
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = healthBar.value;
            if (tween != null)
                tween.Kill();
            tween.SetDelay(0.5f);
            tween = fillImage.DOFillAmount(value, 0.8f).SetEase(Ease.OutQuad);
        }
        return tween;
    }

    public void SetHealthBar(float value)
    {
        healthBarAnimationTween = PlayBarAnimation(healthBarPartialFillImage, value, healthBarAnimationTween);
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

    public void HideBossHPbar()
    {
        StartCoroutine(HideBossBar());
    }

    public void NotEnoughMana()
    {
        StartCoroutine(FlashBar(manaCanvasGroup));
    }

    public void NotEnoughStamina()
    {
        StartCoroutine(FlashBar(staminaCanvasGroup));
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

        if (!isActive)
        {
            settings.SetActive(true);
            settingsView.Show();
        }
        else
            settingsView.Hide();
    }

    private IEnumerator FlashBar(CanvasGroup cg)
    {
        const float duration = 0.4f;
        float time = 0;
        SoundManager.instance.PlaySound(lowNotificationSound);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            if (cg != null)
                cg.alpha = 1 - t * (1 - t) * 3;
            yield return null;
        }
    }

    private IEnumerator HideBossBar()
    {
        CanvasGroup cg = bossHPbar.transform.parent.GetComponent<CanvasGroup>();

        const float duration = 3f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            if (cg)
                cg.alpha = 1 - time / duration;
            yield return null;
        }

        if (cg)
            cg.alpha = 1;

        bossHPbar.gameObject.SetActive(false);
        bossName.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        panelPopUp.SetActive(true);
        panelPopUpView.Show();
    }

    public void MainMenuConfirm()
    {
        SceneManager.LoadScene(0);
    }
}
