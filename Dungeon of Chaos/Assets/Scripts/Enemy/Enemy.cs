using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(AttackManager))]
public class Enemy : Unit
{
    private const float CHASE_HEAT = 3f;
    private const float RAYCAST_TIME_INTERVAL = 1f;

    private enum State
    {
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
    private Animator animator;
    private Rigidbody2D rb;

    protected override void Init()
    {
        loot = Instantiate(loot).Init(this);
        lootModifiers = Instantiate(lootModifiers);
        Target = Character.instance;
        state = State.Patrol;
        attackManager = GetComponent<AttackManager>();
        animator = GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    private float lastLosTime = Mathf.NegativeInfinity;
    private float lastRayCastCheck = 0f;

    private bool IsTargetInChaseRange()
    {
        //Vector2 direction = transform.lossyScale.x > 0 ? -transform.right : transform.right;
        Vector2 direction = GetTargetDirection();

        bool playerHit = false;

        if (Time.time - lastRayCastCheck > RAYCAST_TIME_INTERVAL)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 dir = Quaternion.AngleAxis((-5 + (1 * i)), Vector3.forward) * direction;
                RaycastHit2D hit = Physics2D.Linecast(transform.position, (Vector2)transform.position + (dir * stats.ChaseDistance()), ~(1 << LayerMask.NameToLayer("Enemy")));

                //Debug.DrawLine(transform.position, (Vector2)transform.position + (dir * stats.ChaseDistance()), Color.red, 0.1f);

                if (hit.collider && hit.collider.CompareTag("Player"))
                {
                    playerHit = true;
                    lastLosTime = Time.time;
                    break;
                }
            }
            lastRayCastCheck = Time.time;
        }
        return playerHit || (!playerHit && Time.time - lastLosTime < CHASE_HEAT);
    }

    private void FixedUpdate()
    {
        animator.SetBool("isMoving", rb.velocity.magnitude > 0.01f);
        SwitchEnemyStates();
    }


    private bool IsAttacking()
    {
        return currentAttack != null && currentAttack.IsAttacking();
    }


    private void RotateWeapon()
    {
        if (weapon != null)
        {
            weapon.RotateWeapon(GetTargetPosition());
        }
    }


    private void ResetWeapon()
    {
        if (weapon != null)
        {
            weapon.ResetWeapon();
        }
    }


    private void FlipSprite()
    {
        Vector2 dir = GetTargetDirection();

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }

    private bool Attack()
    {
        if (IsAttacking()) return true;
        currentAttack = attackManager.GetBestAvailableAttack();
        FlipSprite();
        RotateWeapon();
        if (currentAttack != null)
        {
            state = State.Attack;
            currentAttack.Attack();
            return true;
        }
        return false;
    }

    private void Move()
    {
        movement.Move(footstepsSFX);
    }

    private bool Chase()
    {
        if (IsTargetInChaseRange())
        {
            state = State.Chase;

            if (GetTargetDistance() >= attackManager.GetMinimumAttackRange())
            {
                Move();
            }
            FlipSprite();
            RotateWeapon();
            return true;
        }
        return false;
    }


    private bool Patrol()
    {
        //TODO: Write the patrol logic
        state = State.Patrol;
        ResetWeapon();
        return false;
    }

    private void SwitchEnemyStates()
    {
        if (dead) return;
        if (Attack()) { }
        else if (Chase()) { }
        else Patrol();
        //print("Test: " + state.ToString());
    }


    public bool IsAwake()
    {
        return state == State.Attack || state == State.Chase;
    }


    protected override void CleanUp()
    {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }

}
