using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{ 
    [SerializeField] protected GameObject dragDrop;
    [SerializeField] protected Text level;
    [SerializeField] protected GameObject locked;
    [SerializeField] protected GameObject load;

    protected bool rightClick = false;
    protected float time = 0f;
    float upgradeTime = 2.5f;

    public abstract void OnBeginDrag(PointerEventData eventData);
    public void OnDrag(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
        dragDrop.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragDrop.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //TODO: Hide Tooltip
        TooltipSystem.instance.Hide();
        Debug.Log("OnPointerDown");
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left Button");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Button");
            rightClick = true;
            time = 0f;
            load.GetComponent<Image>().fillAmount = 0;
            load.SetActive(true);
        }
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        rightClick = false;
        time = 0f;
    }

    void Start()
    {
        SetLevel();
        SetIcon();
    }

    void Update()
    {
        if (rightClick)
        {
            time += Time.deltaTime;
            load.GetComponent<Image>().fillAmount = time / upgradeTime;
        }
        if (time >= upgradeTime)
        {
            load.SetActive(false);
            Upgrade();
        }
    }

    public abstract void Upgrade();

    public abstract void SetIcon();

    public abstract void SetLevel();
}
