using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleTooltip : Tooltip
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI contentText;


    public void SetText(string header, string content)
    {
        headerText.text = header;
        contentText.text = content;

        layoutElement.enabled = (header.Length > wrapLimit || content.Length > wrapLimit);
    }
}
