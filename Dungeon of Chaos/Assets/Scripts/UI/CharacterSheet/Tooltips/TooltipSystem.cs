using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;
    public SkillTooltip skillTooltip;
    public SimpleTooltip simpleTooltip;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void Show(string header, string subheader, string status, SkillDescription current,
                     SkillDescription next = new SkillDescription())
    {
        skillTooltip.gameObject.SetActive(true);
        skillTooltip.SetText(header, subheader, status, current, next);
    }

    public void Show(string header, string content)
    {
        simpleTooltip.gameObject.SetActive(true);
        simpleTooltip.SetText(header, content);
    }

    public void ShowMessage(string text, float duration)
    {
        simpleTooltip.gameObject.SetActive(true);
        simpleTooltip.SetText(text, "", false);
        simpleTooltip.SetPosition(new Vector2(0.5f, 0.8f));
        StartCoroutine(TweenSimpleTooltip(duration / 4));
    }

    public void DisplayMessage(string message)
    {
        skillTooltip.DisplayMessage(message);
    }

    public void HideSkillTooltip()
    {
        skillTooltip.gameObject.SetActive(false);
        skillTooltip.DeleteMessage();
    }

    public void HideSimpleTooltip()
    {
        simpleTooltip.gameObject.SetActive(false);
    }

    private IEnumerator TweenSimpleTooltip(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            simpleTooltip.SetAlpha(t);
            yield return null;
        }
        yield return new WaitForSeconds(duration * 2);

        time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            simpleTooltip.SetAlpha(1 - t);
            yield return null;
        }
        simpleTooltip.SetAlpha(1);
        HideSimpleTooltip();
    }
}
