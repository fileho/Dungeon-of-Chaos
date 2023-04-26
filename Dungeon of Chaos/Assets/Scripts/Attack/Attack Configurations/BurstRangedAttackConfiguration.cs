using UnityEngine;

/// <summary>
/// Scriptable Object for Burst Ranged Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Burst Ranged Attack Configuration")]
public class BurstRangedAttackConfiguration : RangedAttackConfiguration {
	// Number of projectiles launched in the burst attack
	public int projectileNumber = 5;
}
