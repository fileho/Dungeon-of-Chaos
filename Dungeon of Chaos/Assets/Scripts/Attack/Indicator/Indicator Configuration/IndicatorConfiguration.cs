using UnityEngine;

public abstract class IndicatorConfiguration: ScriptableObject {
    public float duration;
    public Sprite sprite;
    public Color color = Color.white;
}