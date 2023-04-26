using UnityEngine;

/// <summary>
/// Scriptable Object for Swing Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Swing Attack Configuration")]
public class SwingAttackConfiguration : MeleeAttackConfiguration {
	// Angle sweeped by the weapon during the weapon swing animation
	public float swing = 30f;
}
