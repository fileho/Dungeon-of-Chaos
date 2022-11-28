
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Ranged Attack Configuration")]
public class RangedAttackConfiguration : AttackConfiguration
{
    public GameObject projectile;
    public ProjectileConfiguration projectileConfiguration;
    public float wandReach = 1f;
}
