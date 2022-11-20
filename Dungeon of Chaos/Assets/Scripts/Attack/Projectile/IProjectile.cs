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
    protected float scale = 1f;


    protected IAttack attack;
    protected SpriteRenderer sprite;
    protected new Collider2D collider;
    protected Rigidbody2D rb;

    private void Awake() {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        transform.localScale *= scale;
    }

    private void SetAttack(IAttack att) {
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
        scale = projectileConfiguration.scale;
    }

    public virtual void Init(IAttack att) {
        SetAttack(att);
        ApplyConfigurations();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<Unit>()) {
            attack.Weapon.InflictDamage(col.GetComponent<Unit>());
        }
        Destroy(gameObject);
    }


    public virtual void Launch(Vector2 direction) {
        StartCoroutine(LaunchAttack(direction));
    }

    protected virtual IEnumerator LaunchAttack(Vector2 direction) {
        collider.enabled = true;
        rb.AddForce(100 * speed * direction);

        yield return new WaitForSeconds(destroyTime);
        CleanUp();
    }

    protected virtual void CleanUp() {
        Destroy(gameObject);
    }
}
