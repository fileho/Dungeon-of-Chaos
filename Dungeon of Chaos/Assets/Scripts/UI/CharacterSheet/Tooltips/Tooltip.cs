using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI subheaderText;
    [SerializeField] private TextMeshProUGUI contentText;

    [SerializeField] private TextMeshProUGUI messageText;

    [SerializeField] private int wrapLimit;
    
    private LayoutElement layoutElement;
    private RectTransform rectTransform;

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string content, string header, string subheader)
    {
        headerText.text = header;
        subheaderText.text = subheader;
        contentText.text = content;

        int headerLength = headerText.text.Length;
        int subheaderLength = subheaderText.text.Length;
        int contentLength = contentText.text.Length;

        layoutElement.enabled = (headerLength > wrapLimit || contentLength > wrapLimit || subheaderLength > wrapLimit);
    }

    public void DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.color = Color.red;
    }

    public void DeleteMessage()
    {
        messageText.text = "";
    }


    private void Update()
    {
        Vector2 position = Input.mousePosition;
        float pivotX = GetCoordinate(position.x / Screen.width);
        float pivotY = GetCoordinate(position.y / Screen.height);

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        //Debug.Log(rectTransform.pivot);
        transform.position = position;
    }

    private float GetCoordinate(float coord)
    {
        return coord < 0.5
            ? 0
            : 1;
    }
}
