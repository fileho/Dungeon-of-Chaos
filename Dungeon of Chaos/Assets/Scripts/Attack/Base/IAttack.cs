using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Weapon))]
public abstract class IAttack : MonoBehaviour {

    [SerializeField] protected AttackConfiguration attackConfiguration;

    protected Weapon weapon;
    
    // The distance from the unit at which the attack can be used
    protected float range;
    // Damage dealt by the attack
    protected float damage;
    // The stamina cost of the attack
    protected float staminaCost;
    // Time after which the attack can be used again
    protected float cooldown;
    // The type (physical/magical) of the attack
    protected SkillEffectType type;

    protected SoundSettings swingSFX;
    protected SoundSettings impactSFX;
    protected SoundSettings indicatorSFX;
    // The duration of the attack animation
    public float AttackAnimationDuration { get; private set; }

    protected float cooldownLeft = 0f;
    protected bool isAttacking = false;
    protected bool isEnemyInRange = false;
    protected float IndicatorDuration { get; private set; }

    protected Unit owner;
    protected GameObject indicator;
    protected Transform indicatorTransform;


    protected const string INDICATOR_SPAWN_POSITION = "IndicatorSpawnPosition";

    public abstract void Attack();

    public virtual bool CanAttack() {
        return cooldownLeft <= 0;
    }

    public float GetAttackRange() {
        return range;
    }

    public bool IsAttacking() {
        return isAttacking;
    }


    public float GetStaminaCost() {
        return staminaCost;
    }


    public Unit GetTarget() {
        return owner.Target;
    }


    public Vector2 GetTargetPosition() {
        return owner.GetTargetPosition();
    }


    protected virtual void SetIndicatorTransform() { }



    protected virtual void ActivateIndicator() {
        if (indicator == null) return;
        SoundManager.instance.PlaySound(indicatorSFX);
        GameObject _indicator = Instantiate(indicator, indicatorTransform.position, indicatorTransform.rotation, indicatorTransform);
        _indicator.transform.up = weapon.GetForwardDirectionRotated();
        IndicatorDuration = _indicator.GetComponent<IIndicator>().Duration;
    }

    protected virtual void PrepareWeapon() {
        weapon.EnableDisableTrail(true);
        weapon.EnableDisableCollider(true);
        float dmg = type == SkillEffectType.physical
            ? damage * owner.stats.GetPhysicalDamage()
            : damage * owner.stats.GetSpellPower();
        weapon.SetDamage(dmg);
        weapon.SetImpactSound(impactSFX);
    }


    protected virtual void ResetWeapon() {
        weapon.EnableDisableTrail(false);
        weapon.EnableDisableCollider(false);
    }

    protected virtual void ApplyConfigurations() {

        range = attackConfiguration.range;
        damage = attackConfiguration.damage;
        staminaCost = attackConfiguration.staminaCost;
        cooldown = attackConfiguration.cooldown;
        indicator = attackConfiguration.indicator;
        AttackAnimationDuration = attackConfiguration.attackAnimationDuration;
        IndicatorDuration = 0;
        type = attackConfiguration.type;
        swingSFX = attackConfiguration.swingSFX;
        impactSFX = attackConfiguration.impactSFX;
        indicatorSFX = attackConfiguration.indicatorSFX;
    }

    protected virtual void Start() {
        weapon = GetComponent<Weapon>();
        owner = GetComponentInParent<Unit>();
        SetIndicatorTransform();
        ApplyConfigurations();

    }


    protected virtual void Update() {
        if (!isAttacking && cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

}
