using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Tooltip : MonoBehaviour
{
    [SerializeField] protected int wrapLimit;
    
    protected LayoutElement layoutElement;
    private RectTransform rectTransform;

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
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
