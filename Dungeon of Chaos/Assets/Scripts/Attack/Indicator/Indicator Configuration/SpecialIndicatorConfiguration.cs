using UnityEngine;

/// <summary>
/// Scriptable Object that defines Special Attack Indicator Configurations
/// </summary>
[CreateAssetMenu (menuName = "SO/Indicator/Special Indicator Configuration")]
public class SpecialIndicatorConfiguration : IndicatorConfiguration {
	public float range = 10f;
	public float factor = 1f;
}
