using UnityEngine;

/// <summary>
/// Scriptable Object for Stomp Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Stomp Attack Configuration")]
public class StompAttackConfiguration : MeleeAttackConfiguration {
	// How high the weapon is lifted during the weapon animation
	public float lift = 2f;
	// How low the weapon falls during the weapon animation
	public float fall = 0.8f;
	public GameObject impact;
	// By what factor the scale of the weapon increased during the weapon animation
	public float scaleMultiplier = 1.5f;

	// Major damage radius that deals more damage
	public float damageRadiusMajor = 4f;
	// Minor damage radius that deals less damage
	public float damageMajor = 4f;
}
