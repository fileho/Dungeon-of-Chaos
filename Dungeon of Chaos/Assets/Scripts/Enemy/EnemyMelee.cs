using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyMelee : MonoBehaviour, IEnemy
{
    [SerializeField] private float swipe = 20f;
    [SerializeField] private float damage = 10;
    [SerializeField] private float range = 5;

    [SerializeField] private float delay = 0.8f;



    public void SetEnemy(Enemy e)
    {
        enemy = e;
        transform.up = enemy.GetComponentInChildren<Weapon>().transform.up;
    }

    private Enemy enemy;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        var p = transform.position;
        p.z += 1;
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

        enemy.GetComponentInChildren<Weapon>().Attack(swipe, damage, range);

        Invoke(nameof(CleanUp), 0.25f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
