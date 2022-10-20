using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Weapon))]
public abstract class IAttack : MonoBehaviour {

    [SerializeField] protected AttackConfiguration attackConfiguration;

    public Weapon Weapon { get; private set; }
    
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

    protected Vector3 weaponOriginalPosition;
    protected Vector3 weaponAssetOriginalPosition;
    protected Quaternion weaponOriginalRotation;
    protected Quaternion weaponAssetOriginalRotation;


    public abstract void Attack();

    public virtual bool CanAttack() {
        return (IsTargetInAttackRange() && !isAttacking && cooldownLeft <= 0);
    }

    public float GetAttackRange() {
        return range;
    }


    private bool IsTargetInAttackRange() {
        return owner.GetTargetDistance() <= GetAttackRange();
    }


    public float GetDamage()
    {
        if (owner == null)
            return 0;

        return type == SkillEffectType.physical
            ? damage * owner.stats.GetPhysicalDamage()
            : damage * owner.stats.GetSpellPower();
    }


    public float GetCoolDownTime() {
        return cooldown;
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


    protected virtual void SetIndicatorTransform() {}



    protected virtual void ActivateIndicator() {
        if (indicator == null) return;
        //SoundManager.instance.PlaySound(indicatorSFX);
        GameObject _indicator = Instantiate(indicator, indicatorTransform.position, indicatorTransform.rotation, indicatorTransform);
        _indicator.transform.up = Weapon.GetForwardDirectionRotated();
        IndicatorDuration = _indicator.GetComponent<IIndicator>().Duration;
    }


    protected virtual void PrepareWeapon() {
        Weapon.SetDamage(GetDamage());
        Weapon.SetImpactSound(impactSFX);
        Weapon.ResetHitUnits();
        Weapon.EnableDisableTrail(true);
    }


    protected virtual void ResetWeapon() {
        Weapon.EnableDisableTrail(false);
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
        if (attackConfiguration == null)
            return;
        Weapon = GetComponent<Weapon>();
        owner = GetComponentInParent<Unit>();
        SetIndicatorTransform();
        ApplyConfigurations();
    }

    public IAttack Init(Unit owner, Weapon weapon, AttackConfiguration configuration)
    {
        this.owner = owner;
        Weapon = weapon;
        attackConfiguration = configuration;
        ApplyConfigurations();
        isAttacking = false;
        return this;
    }


    protected virtual void Update() {
        if (!isAttacking && cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

}
