using UnityEngine;

/// <summary>
/// Scriptable Object that defines Swing Attack Indicator Configurations
/// </summary>
[CreateAssetMenu (menuName = "SO/Indicator/Swing Indicator Configuration")]
public class SwingIndicatorConfiguration : MeleeIndicatorConfiguration {
	[Tooltip ("It's value is same as the angle sweeped by the swing attack")]
	public float sweep;
}
