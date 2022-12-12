
using UnityEngine;

public abstract class AttackConfiguration : ScriptableObject
{
    public float range = 9f;
    public float damage = 18f;
    public float staminaCost = 0f;
    public float manaCost = 0f;

    public float rangeWeight = 1f;
    public float damageWeight = 1f;
    public float staminaCostWeight = 1f;
    public float manaCostWeight = 1f;

    public float cooldown = 1f;
    public float attackAnimationDuration = 1.5f;
    public GameObject indicator;
    public IndicatorConfiguration indicatorConfiguration;
    public SkillEffectType type;

    public SoundSettings swingSFX;
    public SoundSettings impactSFX;
    public SoundSettings indicatorSFX;
}
