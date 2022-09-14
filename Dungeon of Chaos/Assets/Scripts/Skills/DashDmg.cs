using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dash/Dash dmg")]
public class DashDmg : Dash
{
    [SerializeField] protected float damage;

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (!dashing)
            return;

        stopDash = true;
        if (col.transform.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
