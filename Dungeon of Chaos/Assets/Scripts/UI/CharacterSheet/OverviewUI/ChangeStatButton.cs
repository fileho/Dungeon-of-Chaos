using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ChangeStatButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    private enum Stat
    {
        Strength,
        Intelligence,
        Constitution, 
        Endurance,
        Wisdom
    }

    [SerializeField] private UnityEvent changeStat;
    [SerializeField] private Stat stat;

    [SerializeField] private Color idle;
    [SerializeField] private Color hover;
    [SerializeField] private Color selected;

    [SerializeField] private SoundSettings buttonHover;
    [SerializeField] private SoundSettings buttonClick;

    void Start()
    {
        gameObject.GetComponent<Image>().color = idle;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = selected;
        SoundManager.instance.PlaySound(buttonClick);
        changeStat.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = hover;
        SoundManager.instance.PlaySound(buttonHover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = idle;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = idle;
    }

    public void ChangeStrength(int mod)
    {
        Character.instance.stats.ChangeStrength(mod);
    }

    public void ChangeIntelligence(int mod)
    {
        Character.instance.stats.ChangeIntelligence(mod);
    }

    public void ChangeConstitution(int mod)
    {
        Character.instance.stats.ChangeConstitution(mod);
    }

    public void ChangeEndurance(int mod)
    {
        Character.instance.stats.ChangeEndurance(mod);
    }

    public void ChangeWisdom(int mod)
    {
        Character.instance.stats.ChangeWisdom(mod);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        gameObject.GetComponent<Image>().color = selected;
    }

    public bool CanUpgrade()
    {
        switch (stat)
        {
            case Stat.Strength:
                return Character.instance.stats.CanUpgradeStrength();
            case Stat.Intelligence:
                return Character.instance.stats.CanUpgradeIntelligence();
            case Stat.Constitution:
                return Character.instance.stats.CanUpgradeConstitution();
            case Stat.Endurance:
                return Character.instance.stats.CanUpgradeEndurance();
            case Stat.Wisdom:
                return Character.instance.stats.CanUpgradeWisdom();
            default:
                return true;
        }
    }
    public bool CanDowngrade()
    {
        switch (stat)
        {
            case Stat.Strength:
                return Character.instance.stats.CanDowngradeStrength();
            case Stat.Intelligence:
                return Character.instance.stats.CanDowngradeIntelligence();
            case Stat.Constitution:
                return Character.instance.stats.CanDowngradeConstitution();
            case Stat.Endurance:
                return Character.instance.stats.CanDowngradeEndurance();
            case Stat.Wisdom:
                return Character.instance.stats.CanDowngradeWisdom();
            default:
                return true;
        }
    }


}

