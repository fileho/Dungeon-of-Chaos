
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Projectile/Follow Projectile Configuration")]
public class FollowProjectileConfiguration : ProjectileConfiguration
{
    public float maxSteerForce = 3f;
    public float lag = 3f;
}
