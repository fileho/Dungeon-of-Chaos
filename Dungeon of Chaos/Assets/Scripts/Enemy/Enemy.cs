using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SocialPlatforms;

public class Enemy : Unit {
    [SerializeField] private ILoot loot;
    private IAttack attack;
    private float distanceFromTarget = Mathf.Infinity;

    protected override void Init() {
        loot = Instantiate(loot).Init(transform);
        attack = GetComponentInChildren<IAttack>();
    }


    private void Update() {
        if (IsAttacking())
            return;

        FlipSprite();
        Attack();

        if (IsCharacterInRange())
            RotateWeapon();
        else
            ResetWeapon();
    }

    private void FixedUpdate() {

        if (IsCharacterInRange() || attack.IsAttacking()) {
            return;
        }
        Move();
    }


    private bool IsCharacterInRange() {
        distanceFromTarget = ((Vector2)Character.instance.transform.position - (Vector2)transform.position).magnitude;
        return distanceFromTarget <= attack.GetAttackRange();

    }


    private bool IsAttacking() {
        return attack.IsAttacking();
    }


    private void RotateWeapon() {
        if (weapon != null) {
            weapon.RotateWeapon(Character.instance.transform.position);
        }
    }


    private void ResetWeapon() {
        if (weapon != null) {
            weapon.ResetWeapon();
        }
    }

    private void Attack() {
        if (IsCharacterInRange() && !IsAttacking() && attack.CanAttack())
            attack.Attack();
    }

    private void FlipSprite() {
        if (weapon.IsAttacking())
            return;

        Vector2 dir = Character.instance.transform.position - transform.position;

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }


    private void Move() {

        movement.Move();
    }

    protected override void Die() {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }
}
