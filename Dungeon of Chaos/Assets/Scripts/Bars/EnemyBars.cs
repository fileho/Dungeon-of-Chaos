using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Bars/EnemyBars")]
public class EnemyBars : IBars
{
    // TODO implement those when enemy bars are ready
    public override IBars Init(Transform transform, Stats stats)
    {
        return this;
    }

    public override void UpdateHpBar()
    {
        
    }

    public override void UpdateManaBar()
    {
        
    }

    public override void UpdateStaminaBar()
    {
        
    }
}