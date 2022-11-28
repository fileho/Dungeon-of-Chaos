 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI subheaderText;

    [SerializeField] private TooltipContent content1;
    [SerializeField] private TooltipContent content2;

    [SerializeField] private GameObject message;

    [SerializeField] private int wrapLimit;   
    
    private LayoutElement layoutElement;
    private RectTransform rectTransform;

    private const float headerSizeModifier = 2.5f;

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string header, string subheader, SkillDescription current, SkillDescription next = new SkillDescription())
    {
        headerText.text = header;
        subheaderText.text = subheader;

        int headerLength = Mathf.RoundToInt(headerText.text.Length * headerSizeModifier);
        int subheaderLength = subheaderText.text.Length;

        int currentLength = current.GetLongestLength();
        int nextLength = next.GetLongestLength();

        layoutElement.enabled = (headerLength > wrapLimit || subheaderLength > wrapLimit
            || currentLength > wrapLimit || nextLength > wrapLimit);

        content1.Fill(current);
        content2.Fill(next);
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


    private void Update()
    {
        Vector2 position = Input.mousePosition;
        float pivotX = GetCoordinate(position.x / Screen.width);
        float pivotY = GetCoordinate(position.y / Screen.height);

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }

    private float GetCoordinate(float coord)
    {
        return coord < 0.5
            ? 0
            : 1;
    }
}
