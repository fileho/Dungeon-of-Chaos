using UnityEngine;

/// <summary>
/// Scriptable Object for Smash Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Smash Attack Configuration")]
public class SmashAttackConfiguration : MeleeAttackConfiguration {

	// How high the weapon is lifted during the weapon animation
	public float lift = 2f;
	// How low the weapon falls during the weapon animation
	public float fall = 0f;
	public GameObject impact;
	// By what factor the scale of the weapon increased during the weapon animation
	public float scaleMultiplier = 1.5f;
	public float damageRadius = 2f;
}
