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
    [SerializeField] protected Image frame;

    protected SkillSystem skillSystem;

    protected bool rightClick = false;
    protected float time = 0f;
    float upgradeTime = 1f;

    public abstract void OnBeginDrag(PointerEventData eventData);
    public void OnDrag(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
        dragDrop.GetComponent<RectTransform>().position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        dragDrop.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            Debug.Log("Left Button");
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightMouseDown();
        }
    }

    public virtual void RightMouseDown()
    {
        rightClick = true;
        time = 0f;
        load.GetComponent<Image>().fillAmount = 0;
        load.SetActive(true);
    }

    public abstract void OnPointerEnter(PointerEventData eventData);

    public abstract void OnPointerExit(PointerEventData eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        rightClick = false;
        time = 0f;
        load.GetComponent<Image>().fillAmount = 0;
        load.SetActive(false);
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
