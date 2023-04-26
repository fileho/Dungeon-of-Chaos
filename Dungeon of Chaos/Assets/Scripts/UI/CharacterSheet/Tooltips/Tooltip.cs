using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class for different kind of tooltips
/// </summary>

public abstract class Tooltip : MonoBehaviour
{
    [SerializeField]
    protected int wrapLimit;

    protected LayoutElement layoutElement;
    private RectTransform rectTransform;
    protected bool followMouse = true;

    private void Awake()
    {
        layoutElement = GetComponent<LayoutElement>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!followMouse)
            return;
        Vector2 position = Input.mousePosition;
        float pivotX = GetCoordinate(position.x / Screen.width);
        float pivotY = GetCoordinate(position.y / Screen.height);

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = position;
    }

    private float GetCoordinate(float coord)
    {
        return coord < 0.5 ? 0 : 1;
    }
}
