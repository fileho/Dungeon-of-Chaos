using System.Collections;
using UnityEngine;

public class EnemyAoe : MonoBehaviour, IEnemy
{
    [SerializeField] private float damage;
    [SerializeField] private float delay;
    [SerializeField] private float distance;

    private Enemy enemy;
    private SpriteRenderer sprite;
    private new Collider2D collider;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        var p = transform.position;
        p.z = 5;
        transform.position = p;

        Use();
    }

    private void Use()
    {
        StartCoroutine(ExecuteAttack());
    }

    private IEnumerator ExecuteAttack()
    {
        float time = 0f;

        while (time < delay)
        {
            time += Time.deltaTime;
            float t = time / delay;
            t *= 0.5f;
            sprite.color = Color.Lerp(Color.black, Color.white, t);
            yield return null;
        }

        sprite.color = Color.white;
        collider.enabled = true;

        Invoke(nameof(CleanUp), 0.25f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Character.instance.TakeDamage(damage);
        Destroy(gameObject);
    }

    public void SetEnemy(Enemy e)
    {
        enemy = e;
        Weapon w = enemy.GetComponentInChildren<Weapon>();
        if (w == null)
            return;

        transform.Translate(distance * w.GetForwardDirection());
        transform.parent = e.transform;

        //w.HammerAttack(delay);
    }
}
