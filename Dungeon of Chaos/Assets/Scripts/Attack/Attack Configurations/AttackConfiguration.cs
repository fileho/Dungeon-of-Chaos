
using UnityEngine;

public abstract class AttackConfiguration : ScriptableObject
{
    public float range = 3f;
    public float damage = 10f;
    public float staminaCost = 0f;
    public float cooldown = 1f;
    public float delayAfterIndicator = 0f;
    public GameObject indicator;
}
