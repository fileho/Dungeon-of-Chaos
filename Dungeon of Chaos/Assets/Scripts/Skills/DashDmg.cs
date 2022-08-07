using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dash/Dash dmg")]
public class DashDmg : Dash
{
    [SerializeField] protected float damage;
    protected override void CustomInit()
    {
        trail.startColor = Color.red;
        trail.endColor = Color.red;
    }

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
