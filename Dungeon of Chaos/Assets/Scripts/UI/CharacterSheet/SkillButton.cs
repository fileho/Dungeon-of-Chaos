using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    [SerializeField] protected GameObject dragDrop;

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
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);
}
