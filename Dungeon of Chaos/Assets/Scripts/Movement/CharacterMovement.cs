using UnityEngine;

[CreateAssetMenu(menuName = "SO/Movement/Character")]
public class CharacterMovement : IMovement
{
    private Stats stats;
    private Vector2 moveDir = Vector2.up;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private SoundSettings footstepsSFX;
    private SoundData sfx;

    public override IMovement Init(Transform transform, Stats stats)
    {
        this.stats = stats;
        rb = transform.GetComponent<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        return this;
    }

    public override void Move()
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
            animator.SetBool("isMoving", true);
            // TODO ADD this with animations
            if (sfx == null)
                sfx = SoundManager.instance.PlaySoundLooping(footstepsSFX);
        }
        else
        {
            animator.SetBool("isMoving", false);
            SoundManager.instance.StopLoopingSound(sfx);
            sfx = null;
        }

        rb.AddForce(stats.MovementSpeed() * Time.fixedDeltaTime * 1000 * dir);
    }

    public override void MuteSfx()
    {
        SoundManager.instance.StopLoopingSound(sfx);
    }

    public override Vector2 GetMoveDir()
    {
        return moveDir;
    }
}
