using UnityEngine;

[CreateAssetMenu(menuName = "SO/Movement/Character")]
public class CharacterMovement : IMovement
{
    private Stats stats;
    private Vector2 moveDir;

    private Rigidbody2D rb;

    public override IMovement Init(Transform transform, Stats stats)
    {
        this.stats = stats;
        rb = transform.GetComponent<Rigidbody2D>();

        return this;
    }

    public override void Move(SoundSettings footstepsSFX)
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            dir += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            dir += Vector2.right;
        if (Input.GetKey(KeyCode.W))
            dir += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            dir += Vector2.down;

        dir = dir.normalized;
        if (dir != Vector2.zero)
        {
            moveDir = dir;
            // TODO ADD this with animations
            // SoundManager.instance.PlaySound(footstepsSFX);
        }

        rb.AddForce(stats.MovementSpeed() * Time.fixedDeltaTime * 1000 * dir);
    }

    public override Vector2 GetMoveDir()
    {
        return moveDir;
    }
}
