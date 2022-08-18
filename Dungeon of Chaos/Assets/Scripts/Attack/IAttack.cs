using UnityEngine;

[RequireComponent(typeof(Weapon))]
public abstract class IAttack : MonoBehaviour {

    [SerializeField] protected AttackConfiguration attackConfiguration;
    protected Weapon weapon;
    // Angle sweeped during the weapon animation
    protected float swing;
    // Reach is how far the weapon travels during the attack animation
    protected float reach;
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
    

    public abstract bool CanAttack();
    
    public abstract void Attack();


    //public bool IsEnemyInRange() {
    //    return isEnemyInRange;
    //}


    public float GetAttackRange() {
        return range;
    }

    public bool IsAttacking() {
        return isAttacking;
    }

    //public void SetTarget(Unit unit) {
    //    target = unit;
    //}

    public float GetStaminaCost() {
        return staminaCost;
    }

    protected virtual void Start() {
        weapon = GetComponent<Weapon>();
        swing = attackConfiguration.swing;
        reach = attackConfiguration.reach;
        range = attackConfiguration.range;
        damage = attackConfiguration.damage;
        staminaCost = attackConfiguration.staminaCost;
        cooldown = attackConfiguration.cooldown;
        indicator = attackConfiguration.indicator;
        delayAfterIndicator = attackConfiguration.delayAfterIndicator;
    }

}
