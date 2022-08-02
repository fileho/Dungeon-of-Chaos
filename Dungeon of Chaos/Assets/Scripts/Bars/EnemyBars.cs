using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Bars/EnemyBars")]
public class EnemyBars : IBars
{
    private Stats stats;
    private Slider hpbar;

    public override IBars Init(Transform transform, Stats stats)
    {
        this.stats = stats;

        Transform bars = transform.parent.Find("Bars");

        hpbar = bars.Find("HPbar").GetComponent<Slider>();

        return this;
    }

    public override void UpdateHpBar()
    {
        hpbar.value = stats.HpRatio();
    }

    public override void UpdateManaBar()
    {
        
    }

    public override void UpdateStaminaBar()
    {
        
    }
}