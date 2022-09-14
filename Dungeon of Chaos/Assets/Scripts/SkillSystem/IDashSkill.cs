using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Skills/Dash")]
public class IDashSkill : IActiveSkill
{
    [SerializeField] private Dash dash;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Color trailColor;

    public override void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    {
        if (!CanUse(unit.stats))
            return;

        cooldownLeft = cooldown;
        Consume(unit.stats);

        dash.Use(targetPositions[0]); 
    }

    public void Init(Unit owner)
    {
        dash = Instantiate(dash).Init(dashSpeed, effects, trailColor, owner); 
        
    }

    public bool IsDashing()
    {
        return dash.IsDashing();
    }

    public void TriggerCollision(Collision2D collision)
    {
        dash.OnCollisionEnter2D(collision);
    }
}
