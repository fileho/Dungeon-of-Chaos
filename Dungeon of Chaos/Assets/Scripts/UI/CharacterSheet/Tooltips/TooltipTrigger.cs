using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string header;
    [SerializeField] private string content;

    private const float delay = 0.35f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(nameof(ShowTooltip));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(nameof(ShowTooltip));
        TooltipSystem.instance.HideSimpleTooltip();
    }

    private IEnumerator ShowTooltip()
    {
        yield return new WaitForSecondsRealtime(delay);
        TooltipSystem.instance.Show(header, content);
        yield return null;
    }
}
