using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UpgradeStatButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
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

    public void UpgradeStrength()
    {
        Character.instance.stats.IncreaseStrength();
    }

    public void UpgradeIntelligence()
    {
        Character.instance.stats.IncreaseIntelligence();
    }

    public void UpgradeConstitution()
    {
        Character.instance.stats.IncreaseConstitution();
    }

    public void UpgradeEndurance()
    {
        Character.instance.stats.IncreaseEndurance();
    }

    public void UpgradeWisdom()
    {
        Character.instance.stats.IncreaseWisdom();
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
}

