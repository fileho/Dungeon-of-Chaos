using System.Collections;
using UnityEngine;

public abstract class IProjectile : MonoBehaviour
{
    protected ProjectileConfiguration projectileConfiguration;
    protected float speed = 1f;
    protected float delay = 0.5f;
    protected float offset = 0f;
    protected float destroyTime = 5f;

    protected IAttack attack;
    protected SpriteRenderer sprite;
    protected new Collider2D collider;
    protected Rigidbody2D rb;
    protected GameObject mainPs;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetAttack(IAttack att)
    {
        attack = att;
    }

    protected Unit GetTarget()
    {
        return attack.GetTarget();
    }

    protected Vector2 GetTargetPosition()
    {
        return attack.GetTargetPosition();
    }

    protected virtual void ApplyConfigurations()
    {
        speed = projectileConfiguration.speed;
        delay = projectileConfiguration.delay;
        offset = projectileConfiguration.offset;
        destroyTime = projectileConfiguration.destroyTime;
        mainPs = Instantiate(projectileConfiguration.mainPS, transform);
        transform.localScale = projectileConfiguration.scale;
    }

    public virtual void Init(IAttack att, ProjectileConfiguration pc)
    {
        projectileConfiguration = pc;
        SetAttack(att);
        ApplyConfigurations();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // Don't destroy the projectile over triggers
        if (col.isTrigger)
            return;

        // Don't destroy projectile with essences
        if (col.transform.GetComponent<Essence>())
            return;

        if (col.GetComponent<Unit>())
        {
            attack.Weapon.InflictDamage(col.GetComponent<Unit>());
        }
        CleanUp();
    }

    protected void EnableImpact()
    {
        GameObject impactPs = Instantiate(projectileConfiguration.impactPS, transform.position, Quaternion.identity);
        Destroy(impactPs, 1f);
    }

    public virtual void Launch(Vector2 direction)
    {
        StartCoroutine(LaunchAttack(direction));
    }

    protected virtual IEnumerator LaunchAttack(Vector2 direction)
    {
        collider.enabled = true;
        rb.AddForce(100 * speed * direction);

        yield return new WaitForSeconds(destroyTime);
        CleanUp();
    }

    protected virtual void CleanUp()
    {
        EnableImpact();
        Destroy(gameObject);
    }
}
