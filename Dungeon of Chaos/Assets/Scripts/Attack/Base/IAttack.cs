using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Weapon))]
public abstract class IAttack : MonoBehaviour
{

    [SerializeField] protected AttackConfiguration attackConfiguration;

    public Weapon Weapon { get; private set; }

    // The distance from the unit at which the attack can be used
    protected float range;
    // Damage dealt by the attack
    protected float damage;
    // The stamina cost of the attack
    protected float staminaCost;
    // The mana cost of the attack
    protected float manaCost;

    // Weights of the respective parameters to calculate priority
    protected float rangeWeight;
    protected float damageWeight;
    protected float staminaCostWeight;
    protected float manaCostWeight;

    // Time after which the attack can be used again
    protected float cooldown;
    // The type (physical/magical) of the attack
    protected SkillEffectType type;

    protected SoundSettings swingSFX;
    protected SoundSettings impactSFX;
    // The duration of the attack animation
    public float AttackAnimationDuration { get; private set; }

    protected float cooldownLeft = 0f;
    protected bool isAttacking = false;
    protected bool isEnemyInRange = false;
    public Vector3 IndicatorLocalPosition { get; protected set; } = Vector3.zero;

    protected Unit owner;
    protected Rigidbody2D ownerRB;
    protected GameObject indicatorPrefab;
    protected IIndicator indicator;

    protected Vector3 weaponOriginalPosition;
    protected Vector3 weaponAssetOriginalPosition;
    protected Quaternion weaponOriginalRotation;
    protected Quaternion weaponAssetOriginalRotation;
    protected IndicatorConfiguration indicatorConfiguration;

    public virtual void Attack()
    {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;
        StartCoroutine(StartAttackAnimation());
    }

    protected abstract IEnumerator StartAttackAnimation();

    public virtual bool CanAttack()
    {
        return (IsTargetInAttackRange() && !isAttacking && cooldownLeft <= 0);
    }

    public float GetAttackRange()
    {
        return range;
    }

    public float GetAttackRangeWeighted()
    {
        return GetAttackRange() * rangeWeight;
    }

    private bool IsTargetInAttackRange()
    {
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

    public float GetDamageWeighted()
    {
        return GetDamage() * damageWeight;
    }

    public float GetCoolDownTime()
    {
        return cooldown;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }


    public float GetStaminaCost()
    {
        return staminaCost;
    }


    public float GetStaminaCostWeighted()
    {
        return GetStaminaCost() * staminaCostWeight;
    }

    public float GetManaCost()
    {
        return manaCost;
    }

    public float GetManaCostWeighted()
    {
        return GetManaCost() * manaCostWeight;
    }

    public float GetCost()
    {
        return GetStaminaCostWeighted() + GetManaCostWeighted();
    }


    public Unit GetTarget()
    {
        return owner.Target;
    }


    public Vector2 GetTargetPosition()
    {
        return owner.GetTargetPosition();
    }


    protected virtual IIndicator CreateIndicator(Transform parent = null)
    {
        if (indicatorPrefab == null) return null;

        if (parent == null)
            parent = transform.parent;

        GameObject _indicator = Instantiate(indicatorPrefab, parent);
        IIndicator indicator = _indicator.GetComponent<IIndicator>();
        indicator.Init(indicatorConfiguration);
        return indicator;
    }

    protected virtual void PrepareWeapon()
    {
        Weapon.SetDamage(GetDamage());
        Weapon.SetImpactSound(impactSFX);
        Weapon.ResetHitUnits();
        Weapon.EnableDisableTrail(true);
    }


    protected virtual void ResetWeapon()
    {
        Weapon.EnableDisableTrail(false);
    }

    protected virtual void ApplyConfigurations()
    {
        range = attackConfiguration.range;
        damage = attackConfiguration.damage;
        staminaCost = attackConfiguration.staminaCost;
        manaCost = attackConfiguration.manaCost;

        rangeWeight = attackConfiguration.rangeWeight;
        damageWeight = attackConfiguration.damageWeight;
        staminaCostWeight = attackConfiguration.staminaCostWeight;
        manaCostWeight = attackConfiguration.manaCostWeight;

        cooldown = attackConfiguration.cooldown;
        indicatorPrefab = attackConfiguration.indicator;
        indicatorConfiguration = attackConfiguration.indicatorConfiguration;
        AttackAnimationDuration = attackConfiguration.attackAnimationDuration;
        type = attackConfiguration.type;
        swingSFX = attackConfiguration.swingSFX;
        impactSFX = attackConfiguration.impactSFX;
    }


    protected virtual void Awake()
    {
        if (attackConfiguration == null)
            return;
        Weapon = GetComponent<Weapon>();
        owner = GetComponentInParent<Unit>();
        ownerRB = owner.GetComponent<Rigidbody2D>();
        ApplyConfigurations();
    }

    public IAttack Init(Unit owner, Weapon weapon, AttackConfiguration configuration)
    {
        this.owner = owner;
        ownerRB = this.owner.GetComponent<Rigidbody2D>();
        Weapon = weapon;
        attackConfiguration = configuration;
        ApplyConfigurations();
        isAttacking = false;
        return this;
    }


    protected virtual void Update()
    {
        if (!isAttacking && cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

}
