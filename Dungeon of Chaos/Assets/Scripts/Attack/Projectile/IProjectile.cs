using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(HitChecker))]
public abstract class IProjectile : MonoBehaviour
{
    [SerializeField] protected ProjectileConfiguration projectileConfiguration;
    protected float speed = 200f;
    protected float delay = 0.5f;
    protected float offset = 0f;
    //protected float homingStrength = 0;


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
    }

    protected virtual void Start() {
        weapon = GetComponentInParent<Weapon>();
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ApplyConfigurations();
        Launch();
    }

    protected virtual void Launch() {
        StartCoroutine(LaunchAttack());
    }

    protected virtual IEnumerator LaunchAttack() {
        float time = 0;

        while (time < delay) {
            time += Time.deltaTime;
            float t = time / delay;
            sprite.color = Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), t);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            // move it with the player
            transform.position = transform.parent.position;
            yield return null;
        }

        collider.enabled = true;

        Vector2 goalPos = GetTarget().transform.position;
        Vector2 dir = goalPos - (Vector2)transform.position;
        dir.Normalize();
        dir += offset * Random.insideUnitCircle;
        dir.Normalize();

        rb.AddForce(100 * speed * dir);
        Invoke(nameof(CleanUp), 10f);
    }

    protected virtual void CleanUp() {
        Destroy(gameObject);
    }
}
