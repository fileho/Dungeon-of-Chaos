using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SO/Bars/EnemyBars")]
public class EnemyBars : IBars
{
    private Slider hpbar;

    public override IBars Init(Transform transform)
    {
        Transform bars = transform.parent.Find("Bars");

        hpbar = bars.Find("HPbar").GetComponent<Slider>();

        return this;
    }

    public override void UpdateHpBar(float value)
    {
        hpbar.value = value;
    }

    public override void UpdateManaBar(float value)
    {
        
    }

    public override void UpdateStaminaBar(float value)
    {
        
    }
}