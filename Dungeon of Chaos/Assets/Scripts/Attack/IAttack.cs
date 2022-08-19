using UnityEngine;

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
    // The delay between attack and the attack indicator [0 in case of the player]
    protected float delayAfterIndicator;

    protected float cooldownLeft = 0f;
    protected bool isAttacking = false;
    protected bool isEnemyInRange = false;

    //protected Unit target;
    protected GameObject indicator;
    
    
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


    public virtual void ApplyConfigurations() {
        weapon = GetComponent<Weapon>();
        range = attackConfiguration.range;
        damage = attackConfiguration.damage;
        staminaCost = attackConfiguration.staminaCost;
        cooldown = attackConfiguration.cooldown;
        indicator = attackConfiguration.indicator;
        delayAfterIndicator = attackConfiguration.delayAfterIndicator;
    }

    protected virtual void Start() {
        ApplyConfigurations();
    }


    protected virtual void Update() {
        if (!isAttacking && cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

}
