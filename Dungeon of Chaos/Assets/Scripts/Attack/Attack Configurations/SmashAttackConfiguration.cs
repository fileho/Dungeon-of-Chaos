using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Smash Attack Configuration")]
public class SmashAttackConfiguration : AttackConfiguration {
    public float lift = 2f;
    public float fall = 1.5f;
    public float scaleMultiplier = 1.5f;
    public float damageRadius = 2f;
}