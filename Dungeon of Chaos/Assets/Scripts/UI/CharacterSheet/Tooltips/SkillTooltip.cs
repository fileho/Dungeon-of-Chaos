using TMPro;
using UnityEngine;


/// <summary>
/// Tooltip that shows information provided by SkillInfo
/// </summary>
public class SkillTooltip : Tooltip
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI subheaderText;
    [SerializeField] private TextMeshProUGUI statusText;

    [SerializeField] private TooltipContent content1;
    [SerializeField] private TooltipContent content2;

    [SerializeField] private GameObject message;
    [SerializeField] private GameObject separator;

    private const float headerSizeModifier = 2.5f;

    public void SetText(string header, string subheader, string status, SkillDescription current, SkillDescription next = new SkillDescription())
    {
        headerText.text = header;
        subheaderText.text = subheader;
        statusText.text = status;

        int headerLength = Mathf.RoundToInt(headerText.text.Length * headerSizeModifier);
        int subheaderLength = subheaderText.text.Length;
        int statusLength = statusText.text.Length;

        int currentLength = current.GetLongestLength();
        int nextLength = next.GetLongestLength();

        layoutElement.enabled = (headerLength > wrapLimit || subheaderLength > wrapLimit || statusLength > wrapLimit
            || currentLength > wrapLimit || nextLength > wrapLimit);

        content1.Fill(current);
        content2.Fill(next);

        separator.SetActive(true);
        separator.GetComponent<RectTransform>().sizeDelta = new Vector2(content1.GetComponent<RectTransform>().rect.width, 3);
    } 

    public void DisplayMessage(string message)
    {
        this.message.GetComponent<TextMeshProUGUI>().text = message;
        this.message.GetComponent<TextMeshProUGUI>().color = Color.red;
        this.message.SetActive(true);
    }

    public void DeleteMessage()
    {
        this.message.GetComponent<TextMeshProUGUI>().text = "";
        this.message.SetActive(false);
    }
}
