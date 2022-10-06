using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

public abstract class IProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfiguration projectileConfiguration;
    protected float speed = 200f;
    protected float delay = 0.5f;
    protected float offset = 0f;
    protected float destroyTime = 5f;


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
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ApplyConfigurations();
        Launch();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<Unit>()) {
            attack.Weapon.InflictDamage(col.GetComponent<Unit>());
        }
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
