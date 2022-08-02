using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;

    protected Rigidbody2D rb;
    protected Weapon weapon;

    [SerializeField] protected IMovement movement;
    [SerializeField] protected Ieffects effects;
    [SerializeField] protected IBars bars;


    protected void Start()
    {
        stats = Instantiate(stats).ResetStats();

        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();

        movement = Instantiate(movement).Init(transform, stats);
        effects = Instantiate(effects).Init(transform);
        bars = Instantiate(bars).Init(transform, stats);

        bars.UpdateAllBars();

        Init();
    }

    protected virtual void Init() { }

    public void TakeDamage(float value)
    {
        stats.ConsumeHealth(value);
        effects.TakeDamage();
        bars.UpdateHpBar();
        if (stats.IsDead())
            Die();
    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
