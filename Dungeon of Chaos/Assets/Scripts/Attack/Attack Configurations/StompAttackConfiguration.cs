using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Stomp Attack Configuration")]
public class StompAttackConfiguration : MeleeAttackConfiguration
{
    public float lift = 2f;
    public float fall = 0.8f;
    public GameObject impact;
    public float scaleMultiplier = 1.5f;
    public float damageRadiusMajor = 4f;
    public float damageMajor = 4f;
}
