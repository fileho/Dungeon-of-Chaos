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
    
    public void Show(string header, string subheader, SkillDescription current, SkillDescription next = new SkillDescription())
    {
        skillTooltip.gameObject.SetActive(true);
        skillTooltip.SetText(header, subheader, current, next);        
    }

    public void Show(string header, string content)
    {
        simpleTooltip.gameObject.SetActive(true);
        simpleTooltip.SetText(header, content);
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
}
