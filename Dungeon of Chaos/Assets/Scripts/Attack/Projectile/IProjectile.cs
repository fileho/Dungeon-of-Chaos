using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfiguration projectileConfiguration;
    protected float speed = 200f;
    protected float delay = 0.5f;
    protected float offset = 0f;
    protected float destroyTime = 5f;


    protected Weapon weapon;
    protected IAttack attack;
    protected SpriteRenderer sprite;
    protected new Collider2D collider;
    protected Rigidbody2D rb;


    public void SetAttack(IAttack att) {
        attack = att;
    }

    protected Unit GetTarget() {
        return attack.GetTarget();
    }


    protected Vector2 GetTargetPosition() {
        return attack.GetTargetPosition();
    }


    protected virtual void ApplyConfigurations() {
        sprite.sprite = projectileConfiguration.sprite;
        speed = projectileConfiguration.speed;
        delay = projectileConfiguration.delay;
        offset = projectileConfiguration.offset;
        destroyTime = projectileConfiguration.destroyTime;
    }

    protected virtual void Start() {
        weapon = GetComponentInParent<Weapon>();
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ApplyConfigurations();
        Launch();
        transform.parent = null;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        var unit = col.GetComponent<Unit>();
        if (unit != null) {
            weapon.InflictDamage(unit);
        }
        Destroy(gameObject);
    }


    protected virtual void Launch() {
        StartCoroutine(LaunchAttack());
    }

    protected virtual IEnumerator LaunchAttack() {
        collider.enabled = true;

        Vector2 goalPos = GetTarget().transform.position;
        Vector2 dir = goalPos - (Vector2)transform.position;
        dir.Normalize();
        dir += offset * Random.insideUnitCircle;
        dir.Normalize();

        rb.AddForce(100 * speed * dir);

        yield return new WaitForSeconds(destroyTime);
        CleanUp();
    }

    protected virtual void CleanUp() {
        Destroy(gameObject);
    }
}
