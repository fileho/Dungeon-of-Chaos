using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Player can equip skills by placing them into the slots
/// </summary>
public abstract class SkillSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] protected int index;
    [SerializeField] protected Image image;
    [SerializeField] protected Image highlight;

    [SerializeField] protected SoundSettings drop;

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
            image.color = new Color(0.83f, 0.75f, 0.55f, 1f);
        else
            image.color = new Color(1f, 1f, 1f, 1f);
    }
}

