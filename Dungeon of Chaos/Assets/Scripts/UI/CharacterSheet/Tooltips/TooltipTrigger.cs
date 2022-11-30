using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [SerializeField] private string subheader;
    [SerializeField] private string content;

    //private TooltipSystem tooltip = TooltipSystem.instance;

    private const float delay = 0.35f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(nameof(ShowTooltip));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(nameof(ShowTooltip));
        TooltipSystem.instance.Hide();
    }

    private IEnumerator ShowTooltip()
    {
        yield return new WaitForSecondsRealtime(delay);
        TooltipSystem.instance.Show(header, subheader, "", content);
        yield return null;
    }
}*/
