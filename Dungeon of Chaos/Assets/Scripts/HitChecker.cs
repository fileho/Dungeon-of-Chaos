using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            weapon.PlayerHit();
            return;
        }

        if (!col.CompareTag("Enemy"))
            return;
        Enemy enemy = col.GetComponent<Enemy>();
        weapon.EnemyHit(enemy);
    }
}
