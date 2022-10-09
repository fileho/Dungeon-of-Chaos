using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TabGroup tabGroup;
    [HideInInspector]
    public Image background;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    void Awake()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }



}
