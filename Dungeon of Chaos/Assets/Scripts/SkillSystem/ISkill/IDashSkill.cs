using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Skills/Dash")]
public class IDashSkill : IActiveSkill
{
    [SerializeField] private Dash dash;
    [SerializeField] private float dashSpeed;
    [SerializeField] private Color trailColor;

    /// <summary>
    /// Applies all skill effects of the skill on given targets
    /// </summary>
    /// <param name="unit">unit using the skill</param>
    /// <param name="targets">list of target units (optional)</param>
    /// <param name="targetPositions">list with direction of the dash (required)</param>
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
        dash = dash.Init(dashSpeed, effects, trailColor, owner); 
        
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
