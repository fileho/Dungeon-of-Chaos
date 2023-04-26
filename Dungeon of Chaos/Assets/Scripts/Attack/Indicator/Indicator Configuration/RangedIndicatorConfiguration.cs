using UnityEngine;

/// <summary>
/// Scriptable Object that defines Ranged Attack Indicator Configurations
/// </summary>
[CreateAssetMenu (menuName = "SO/Indicator/Ranged Indicator Configuration")]
public class RangedIndicatorConfiguration : IndicatorConfiguration {
	public Color color = Color.white;
	public Vector2 initialScale = Vector2.zero;
	public Vector2 finalScale = Vector2.one;
}
