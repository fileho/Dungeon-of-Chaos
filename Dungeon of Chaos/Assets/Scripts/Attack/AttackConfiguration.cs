
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Attack/Attack Configuration")]
public class AttackConfiguration : ScriptableObject
{
    public float swing = 0f;
    public float reach = 0f;
    public float range = 3f;
    public float damage = 10f;
    public float staminaCost = 0f;
    public float cooldown = 1f;
    public float delayAfterIndicator = 0f;
    public GameObject indicator;
}
