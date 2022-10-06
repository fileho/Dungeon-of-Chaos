using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(AttackManager))]
public class Enemy : Unit {

    private enum State {
        Idle,
        Patrol,
        Chase,
        Attack,
    }


    [SerializeField] private ILoot loot;
    public LootModifiers lootModifiers;
    private AttackManager attackManager;
    private IAttack currentAttack;
    private State state;


    protected override void Init() {
        loot = Instantiate(loot).Init(this);
        lootModifiers = Instantiate(lootModifiers);
        attackManager = GetComponent<AttackManager>();
        attacks = GetComponentsInChildren<IAttack>().ToList();
        Target = Character.instance;
        state = State.Patrol;
        SortAttacks();
    }


    private bool IsTargetInChaseRange() {
        return GetTargetDistance() < stats.ChaseDistance();
    }

    private void FixedUpdate() {
        SwitchEnemyStates();
    }


    private bool IsAttacking() {
        return currentAttack != null && currentAttack.IsAttacking();
    }


    private void RotateWeapon() {
        if (weapon != null) {
            weapon.RotateWeapon(GetTargetPosition());
        }
    }


    private void ResetWeapon() {
        if (weapon != null) {
            weapon.ResetWeapon();
        }
    }


    private void FlipSprite() {
        Vector2 dir = GetTargetDirection();

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }

    private bool Attack() {
        if (IsAttacking()) return true;
            currentAttack = GetBestAvailableAttack();
        if (currentAttack != null) {
            state = State.Attack;
            currentAttack.Attack();
            return true;
        }
        return false;
    }

    private void Move() {
        movement.Move(footstepsSFX);
    }

    private bool Chase() {
        if (IsTargetInChaseRange()) {
            state = State.Chase;
            if (GetTargetDistance() > GetMinimumAttackRange()) {
                Move();
            }
            FlipSprite();
            RotateWeapon();
            return true;
        }
        return false;
    }


    private bool Patrol() {
        //TODO: Write the patrol logic
        state = State.Patrol;
        ResetWeapon();
        return false;
    }

    private void SwitchEnemyStates() {
        if (dead) return;
        if(Attack());
        else if (Chase());
        else Patrol();
        //print("Test: " + state.ToString());
    }


    public bool IsAwake() {
        return state == State.Attack || state == State.Chase;
    }
   

    protected override void CleanUp() {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }



    //============== TODO: This code to be moved to AttackManager ====================================
    List<IAttack> attacks;
    private float minimumAttackRange = 0;


    private void SortAttacks() {
        List<IAttack> rangeSortedAttack = attacks.OrderBy(x => x.GetAttackRange()).ToList();
        minimumAttackRange = rangeSortedAttack[0].GetAttackRange();
        List<IAttack> sortedAttacks = attacks.OrderByDescending(x => GetAttacKWeight(x)).ToList();
        attacks = sortedAttacks;
    }


    private float GetAttacKWeight(IAttack attack) {
        // TODO: add weights for each factor
        // weightRange * attack.GetAttackRange()
        // weightDamage * attack.GetDamage()
        // weightStamina * attack.GetStaminaCost()
        return attack.GetAttackRange() + attack.GetDamage() - attack.GetStaminaCost();
    }


    private float GetMinimumAttackRange() {
        return minimumAttackRange;
    }

    private IAttack GetBestAvailableAttack() {
        for (int i = 0; i < attacks.Count; ++i) {
            if (attacks[i].CanAttack())
                return attacks[i];
        }
        return null;
    }
    //===================================================================================================
}
