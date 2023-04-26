using UnityEngine;
/// <summary>
/// Scriptable Object for Summon Attack Configuration
/// </summary>
[CreateAssetMenu (menuName = "SO/Attack/Summon Attack Configuration")]
public class SummonAttackConfiguration : AttackConfiguration {
	// The enemy summoned by the attack
	public GameObject minion;
	public int minAmountOfMinions = 2;
	public int maxAmountOfMinions = 10;
	public float timeBetweenSpawns = 0.5f;
	// Radius within which the minions are spawned
	public float spawnRadius = 3f;
}
