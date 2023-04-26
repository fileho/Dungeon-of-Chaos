using UnityEngine;

/// <summary>
/// Scriptable Object for Heal Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Heal Attack Configuration")]
public class HealAttackConfiguration : AttackConfiguration {
	public float healAmount = 2f;
	public float healRadius = 10;
}
