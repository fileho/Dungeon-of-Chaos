
using UnityEngine;

public abstract class AttackConfiguration : ScriptableObject
{
    public float range = 9f;
    public float damage = 18f;
    public float staminaCost = 0f;
    public float cooldown = 1f;
    public float attackAnimationDuration = 1.5f;
    public GameObject indicator;
    public SkillEffectType type;

    public SoundSettings swingSFX;
    public SoundSettings impactSFX;
}
