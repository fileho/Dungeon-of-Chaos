using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UpgradeStatButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private UnityEvent changeStat;

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
}

