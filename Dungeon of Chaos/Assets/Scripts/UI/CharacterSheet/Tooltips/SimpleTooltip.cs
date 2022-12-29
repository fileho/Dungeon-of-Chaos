using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleTooltip : Tooltip
{
    [SerializeField]
    private TextMeshProUGUI headerText;
    [SerializeField]
    private TextMeshProUGUI contentText;
    [SerializeField]
    private CanvasGroup canvasGroup;

    public void SetText(string header, string content, bool followMouse = true)
    {
        headerText.text = header;
        contentText.text = content;
        this.followMouse = followMouse;

        layoutElement.enabled = (header.Length > wrapLimit || content.Length > wrapLimit);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position * new Vector2(Screen.width, Screen.height);
    }

    public void SetAlpha(float value)
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = value;
    }
}
