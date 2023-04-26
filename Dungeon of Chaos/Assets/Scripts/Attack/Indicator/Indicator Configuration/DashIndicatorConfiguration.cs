using UnityEngine;


/// <summary>
/// Scriptable Object for Dash Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Indicator/Dash Indicator Configuration")]
public class DashIndicatorConfiguration : MeleeIndicatorConfiguration {
	// Variable to define the distance covered while dashing
	public float distance = 10f;
}
