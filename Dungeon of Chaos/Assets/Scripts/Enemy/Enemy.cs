using UnityEngine;


public class Enemy : Unit {
    [SerializeField] private ILoot loot;
    public LootModifiers lootModifiers;
    private IAttack attack;
    private float distanceFromTarget = Mathf.Infinity;

    protected override void Init() {
        loot = Instantiate(loot).Init(this);
        lootModifiers = Instantiate(lootModifiers);
        attack = GetComponentInChildren<IAttack>();
        Target = Character.instance;
    }


    private void Update() {
        if (dead)
            return;
        if (IsAttacking())
            return;

        FlipSprite();
        Attack();

        if (IsTargetInRange())
            RotateWeapon();
        else
            ResetWeapon();
    }

    private void FixedUpdate() {
        if (dead)
            return;
        if (IsTargetInRange() || attack.IsAttacking()) {
            return;
        }
        Move();
    }


    public override Vector2 GetTargetPosition() {
        return (Vector2)Target.transform.position;
    }


    private bool IsTargetInRange() {
        distanceFromTarget = (GetTargetPosition() - (Vector2)transform.position).magnitude;
        return distanceFromTarget <= attack.GetAttackRange();

    }


    private bool IsAttacking() {
        return attack.IsAttacking();
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

    private void Attack() {
        if (IsTargetInRange() && !IsAttacking() && attack.CanAttack())
            attack.Attack();
    }

    private void FlipSprite() {
        if (IsAttacking())
            return;

        Vector2 dir = GetTargetPosition() - (Vector2)transform.position;

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }


    private void Move() {

        movement.Move(footstepsSFX);
    }

    protected override void CleanUp() {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }
}
