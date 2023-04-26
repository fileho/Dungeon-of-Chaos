using UnityEngine;

/// <summary>
/// Base class defining values for attacks
/// </summary>
public abstract class AttackConfiguration : ScriptableObject {
	public float range = 9f;
	public float damage = 18f;
	public float staminaCost = 0f;
	public float manaCost = 0f;


	// These values give weight to the above parameters which can then be used to influence attack selection
	public float rangeWeight = 1f;
	public float damageWeight = 1f;
	public float staminaCostWeight = 1f;
	public float manaCostWeight = 1f;

	public float cooldown = 1f;
	public float attackAnimationDuration = 1.5f;
	public GameObject indicator;
	public IndicatorConfiguration indicatorConfiguration;
	public SkillEffectType type;

	public ISkillEffect attackEffect = null;

	public SoundSettings swingSFX;
	public SoundSettings impactSFX;
}
