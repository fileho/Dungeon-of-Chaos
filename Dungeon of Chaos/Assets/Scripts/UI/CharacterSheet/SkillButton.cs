using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{ 
    [SerializeField] protected GameObject dragDrop;
    bool rightClick = false;
    float time = 0f;
    float upgradeTime = 1.5f;

    public abstract void OnBeginDrag(PointerEventData eventData);
    public void OnDrag(PointerEventData eventData)
    {
        dragDrop.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragDrop.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //TODO: Hide Tooltip
        Debug.Log("OnPointerDown");
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left Button");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Button");
            rightClick = true;
            time = 0f;
        }

    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    void Update()
    {
        if (rightClick)
            time += Time.deltaTime;
        if (time >= upgradeTime)    
            Upgrade();
    }

    public abstract void Upgrade();

    public void OnPointerUp(PointerEventData eventData)
    {
        rightClick = false;
        time = 0f;
    }
}
