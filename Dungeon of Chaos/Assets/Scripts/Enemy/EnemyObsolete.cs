using UnityEngine;

public class EnemyObsolete : Unit
{
    [SerializeField] private ILoot loot;

    private EnemyAttack attack;

    //private bool attacking = false;

    protected override void Init()
    {
        attack = GetComponentInChildren<EnemyAttack>();
        //loot = Instantiate(loot).Init(this);
    }


    //private void Update()
    //{
    //    if (IsAttacking())
    //        return;

    //    FlipSprite();
    //    Attack();
    //    RotateWeapon();
    //}

    //private void FixedUpdate()
    //{
        //if (IsAttacking())
        //{
        //    return;
        //}
      
        //Move();
    //}


    //private bool IsAttacking()
    //{
    //    return attacking || weapon.IsAttacking();
    //}


    public void AttackWeapon(float swipe, float dmg, float range)
    {
        //weapon.Attack(swipe, dmg, range);
    }

    private void RotateWeapon()
    {
        if (weapon != null)
        {
            weapon.RotateWeapon(Character.instance.transform.position);
        }
    }

    private void Attack()
    {
        if (!attack.CanUse(transform.position))
            return;


        //attacking = true;
        attack.Use();

        Invoke(nameof(ReadyAttack), attack.GetDelay());
    }

    private void FlipSprite()
    {
        //if (weapon.IsAttacking())
        //    return;

        Vector2 dir = Character.instance.transform.position - transform.position;

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }

    private void ReadyAttack()
    {
        //attacking = false;
    }

    private void Move()
    {
        movement.Move(footstepsSFX);
    }

    protected override void CleanUp()
    {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }
}
