using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;
    public Tooltip tooltip;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    
    public void Show(string header, string subheader, SkillDescription current, SkillDescription next = new SkillDescription())
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetText(header, subheader, current, next);        
    }

    public void DisplayMessage(string message)
    {
        tooltip.DisplayMessage(message);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
        tooltip.DeleteMessage();
    }
}
