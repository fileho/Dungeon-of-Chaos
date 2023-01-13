using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
{ 
    [SerializeField] protected GameObject dragDrop;
    [SerializeField] protected Text level;
    [SerializeField] protected GameObject lockObj;
    [SerializeField] protected GameObject requirementsNotMet;
    [SerializeField] protected GameObject highlight;

    
    [SerializeField] protected SoundSettings hover;
    [SerializeField] protected SoundSettings requirements;

    protected SkillSystem skillSystem;

    protected bool rightClick = false;
    protected float time = 0f;
    float upgradeTime = 0.5f;

    public abstract void OnBeginDrag(PointerEventData eventData);
    public void OnDrag(PointerEventData eventData)
    {
        TooltipSystem.instance.HideSkillTooltip();
        dragDrop.GetComponent<RectTransform>().position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        dragDrop.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightMouseDown();
        }
    }

    public virtual void RightMouseDown()
    {
        rightClick = true;
        time = 0f;
        lockObj.GetComponent<Image>().fillAmount = 1;
        highlight.GetComponent<Image>().fillAmount = 1;
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        rightClick = false;
        time = 0f;
        lockObj.GetComponent<Image>().fillAmount = 1;
    }

    public virtual void Init()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
    }

    void Update()
    {
        if (rightClick)
        {
            time += Time.unscaledDeltaTime;
            if (lockObj.activeInHierarchy)
                lockObj.GetComponent<Image>().fillAmount = 1 - (time / upgradeTime);
            else
                highlight.GetComponent<Image>().fillAmount = 1 - (time / upgradeTime);
        }
        if (time >= upgradeTime)
        {
            lockObj.SetActive(false);
            highlight.SetActive(false);
            Upgrade();
        }
    }

    public abstract void Upgrade();

    public abstract void SetIcon();

    public abstract void SetLevel();

    public abstract void SetRequirementsOverlay();

    public abstract void SetHighlightOverlay();
}
