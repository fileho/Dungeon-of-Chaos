using UnityEngine;


/// <summary>
/// Scriptable Object for Follow Projectile Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Projectile/Follow Projectile Configuration")]
public class FollowProjectileConfiguration : ProjectileConfiguration {

	// Variable to determine the curve of the homing projectile while following the target
	public float maxSteerForce = 3f;

	// Variable that determines how far in the future the position of the target needs to be calculated
	public float lookAhead = 3f;
}
