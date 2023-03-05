using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;

    protected Weapon weapon;

    [SerializeField]
    protected IMovement movement;
    [SerializeField]
    protected IEffects effects;
    [SerializeField]
    protected IBars bars;

    [SerializeField]
    protected SoundSettings takeDmgSFX;
    [SerializeField]
    protected SoundSettings deathSFX;

    protected bool dead = false;

    protected System.Action unitHit;

    protected void Start()
    {
        weapon = GetComponentInChildren<Weapon>();

        bars = Instantiate(bars).Init(transform);
        stats = Instantiate(stats).ResetStats(bars);

        movement = Instantiate(movement).Init(transform, stats);
        effects = Instantiate(effects).Init(transform);

        bars.FillAllBars();

        Init();
    }

    protected virtual void Init()
    {
    }

    // It can be either the position of the Unit or Mouse Position [In case of the character]
    public virtual Vector2 GetTargetPosition()
    {
        return Target == null ? Vector2.positiveInfinity : (Vector2)Target.transform.position;
    }

    public virtual Vector2 GetTargetDirection()
    {
        return Target == null ? Vector2.positiveInfinity
                              : (GetTargetPosition() - (Vector2)transform.position).normalized;
    }

    public float GetTargetDistance()
    {
        return (GetTargetPosition() - (Vector2)transform.position).magnitude;
    }

    // Character in case of the enemy.
    // It can be changed at runtime.
    // Null in the case of player
    public Unit Target { get; protected set; }

    public void TakeDamage(float value, bool playSfx = true)
    {
        unitHit?.Invoke();
        float rest = value - stats.GetArmor();
        if (stats.HasArmor())
            stats.SetArmor(-value);
        if (rest <= 0)
            return;

        stats.ConsumeHealth(rest);
        effects.TakeDamage();
        if (playSfx)
            SoundManager.instance.PlaySound(takeDmgSFX);
        if (stats.IsDead())
            Die();
    }

    protected virtual void Die()
    {
        dead = true;
        GetComponent<Collider2D>().enabled = false;
        var vfx = transform.Find("DeathVFX");
        if (vfx)
            vfx.GetComponent<ParticleSystem>().Play();
        SoundManager.instance.PlaySound(deathSFX);
        Invoke(nameof(CleanUp), 1);
    }

    protected virtual void CleanUp()
    {
        Destroy(gameObject);
    }
}
