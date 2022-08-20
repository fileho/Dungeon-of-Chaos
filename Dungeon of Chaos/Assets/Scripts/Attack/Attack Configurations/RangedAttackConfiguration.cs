
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Ranged Attack Configuration")]
public class RangedAttackConfiguration : AttackConfiguration {
    public GameObject projectile;
    public float wandReach = 1f;
    public float wandWave = 10f;
}
