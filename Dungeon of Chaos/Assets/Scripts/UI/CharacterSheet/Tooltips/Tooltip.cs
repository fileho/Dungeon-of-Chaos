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

    private TextMeshProUGUI contentHeader1;
    private TextMeshProUGUI descriptionText1;

    private TextMeshProUGUI contentHeader2;
    private TextMeshProUGUI descriptionText2;

    
    
    private LayoutElement layoutElement;
    private RectTransform rectTransform;

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();

        contentHeader1 = content1.contentHeader.GetComponent<TextMeshProUGUI>();
        descriptionText1 = content1.description.GetComponent<TextMeshProUGUI>();

        contentHeader2 = content2.contentHeader.GetComponent<TextMeshProUGUI>();
        descriptionText2 = content2.description.GetComponent<TextMeshProUGUI>();
    }
    public void SetText(string header, string subheader, string ch1, string des1, string ch2="", string des2="")
    {
        headerText.text = header;
        subheaderText.text = subheader;

        contentHeader1.text = ch1;
        descriptionText1.text = des1;

        contentHeader2.text = ch2;
        descriptionText2.text = des2;


        int headerLength = headerText.text.Length;
        int subheaderLength = subheaderText.text.Length;

        int ch1Length = contentHeader1.text.Length;
        int des1Length = descriptionText1.text.Length;

        int ch2Length = contentHeader2.text.Length;
        int des2Length = descriptionText2.text.Length;

        layoutElement.enabled = (headerLength > wrapLimit || subheaderLength > wrapLimit 
            || ch1Length > wrapLimit || des1Length > wrapLimit
            || ch2Length > wrapLimit || des2Length > wrapLimit);

        content1.enabled = true;
        content2.enabled = true;

        if (Hide(ch1Length, des1Length))
            content1.enabled = false;
        if (Hide(ch2Length, des2Length))
            content2.enabled = false;

        content2.description.gameObject.SetActive(true);
        if (des2Length == 0)
            content2.description.gameObject.SetActive(false);

    }

    public bool Hide(int chL, int dL)
    {
        return chL == 0 && dL == 0;
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
