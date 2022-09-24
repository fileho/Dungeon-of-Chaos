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
    
    public void Show(string content, string header, string subheader)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetText(content, header, subheader);        
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
