using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [SerializeField] private string subheader;
    [SerializeField] private string content;

    private float delay = 0.75f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine("ShowTooltip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine("ShowTooltip");
        TooltipSystem.instance.Hide();
    }

    private IEnumerator ShowTooltip()
    {
        yield return new WaitForSeconds(delay);
        TooltipSystem.instance.Show(content, header, subheader);
        yield return null;
    }
}
