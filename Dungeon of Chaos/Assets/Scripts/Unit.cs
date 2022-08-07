using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;

    protected Weapon weapon;

    [SerializeField] protected IMovement movement;
    [SerializeField] protected Ieffects effects;
    [SerializeField] protected IBars bars;


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

    public void TakeDamage(float value)
    {
        stats.ConsumeHealth(value);
        effects.TakeDamage();
        if (stats.IsDead())
            Die();
    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
