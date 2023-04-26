using UnityEngine;

/// <summary>
/// Scriptable Object for Ranged Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Ranged Attack Configuration")]
public class RangedAttackConfiguration : AttackConfiguration {
	public GameObject projectile;
	public ProjectileConfiguration projectileConfiguration;

	// How far the wand moves during the wand animation
	public float wandReach = 1f;
}
