using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Summon Attack Configuration")]
public class SummonAttackConfiguration : AttackConfiguration
{
    public GameObject minion;
    public int minAmountOfMinions = 2;
    public int maxAmountOfMinions = 10;
    public float timeBetweenSpawns = 0.5f;
    public float spawnRadius = 3f;
}
