using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Tooltip used for stats and other parts of character sheet beside skills
/// </summary>
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

        contentText.gameObject.SetActive(content != "");

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
