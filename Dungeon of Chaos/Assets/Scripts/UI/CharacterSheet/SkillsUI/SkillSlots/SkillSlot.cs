using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkillSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] protected int index;
    [SerializeField] protected Image image;
    [SerializeField] protected Image highlight;

    protected SkillSystem skillSystem;

    protected virtual void Start()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
    }
    public abstract void OnDrop(PointerEventData eventData);

    public void Highlight()
    {
        highlight.color = new Color(1f, 0.74f, 0f);
    }

    public void RemoveHighlight()
    {
        highlight.color = new Color(1f, 1f, 1f, 0f);
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
        if (sprite == null)
            image.color = new Color(1f, 1f, 1f, 0f);
        else
            image.color = new Color(1f, 1f, 1f, 1f);
    }
}

