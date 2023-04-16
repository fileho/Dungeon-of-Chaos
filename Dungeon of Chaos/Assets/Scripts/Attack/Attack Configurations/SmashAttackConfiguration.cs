using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Smash Attack Configuration")]
public class SmashAttackConfiguration : MeleeAttackConfiguration
{
    public float lift = 2f;
    public float fall = 0f;
    public GameObject impact;
    public float scaleMultiplier = 1.5f;
    public float damageRadius = 2f;
}
