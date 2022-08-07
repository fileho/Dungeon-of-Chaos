using UnityEngine;

[CreateAssetMenu(menuName = "SO/Movement/MeleeEnemy")]
public class EnemyMovement : IMovement
{
    private Rigidbody2D rb;
    private Stats stats;

    public override IMovement Init(Transform transform, Stats stats)
    {
        rb = transform.GetComponent<Rigidbody2D>();
        this.stats = stats;

        return this;
    }

    public override void Move()
    {
        // TODO Add pathfinding
        Vector2 dir = (Character.instance.transform.position - rb.transform.position).normalized;

        rb.AddForce(stats.MovementSpeed() * Time.fixedDeltaTime * 1000 * dir);
    }

    public override Vector2 GetMoveDir()
    {
        return Vector2.zero;
    }

}
