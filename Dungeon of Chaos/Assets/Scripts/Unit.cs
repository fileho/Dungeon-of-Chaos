using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;

    protected Weapon weapon;

    [SerializeField] protected IMovement movement;
    [SerializeField] protected Ieffects effects;
    [SerializeField] protected IBars bars;

    [SerializeField] protected SoundSettings takeDmgSFX;
    [SerializeField] protected SoundSettings deathSFX;
    [SerializeField] protected SoundSettings footstepsSFX;

    protected bool dead = false;

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

    protected virtual void Init() { }

    // It can be either the position of the Unit or Mouse Position [In case of the character]
    public virtual Vector2 GetTargetPosition() { return Target.transform.position; }

    // Character in case of the enemy.
    // It can be changed at runtime.
    // Null in the case of player
    public Unit Target { get; protected set; }

    public void TakeDamage(float value)
    {
        float rest = value - stats.GetArmor();
        if (stats.HasArmor())
            stats.SetArmor(-value);
        if (rest <= 0)
            return;
        
        stats.ConsumeHealth(rest);
        effects.TakeDamage();
        SoundManager.instance.PlaySound(takeDmgSFX);
        if (stats.IsDead())
            Die();
    }


    protected void Die()
    {
        dead = true;
        SoundManager.instance.PlaySound(deathSFX);
        Invoke(nameof(CleanUp), SoundManager.instance.GetLength(deathSFX));
        //Destroy(gameObject);
    }

    protected virtual void CleanUp()
    {
        Destroy(gameObject);
    }
}
