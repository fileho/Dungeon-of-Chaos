using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/IncreaseVisibility")]
public class IncreaseVisibility : IPassiveSkill
{
    [SerializeField] private float amount;

    public override void Equip(Stats stats)
    {
        Character.instance.GetComponentInParent<Light2D>().pointLightOuterRadius += amount;
    }

    public override string GetEffectDescription()
    {
        return "";
    }

    public override void Unequip(Stats stats)
    {
        Character.instance.GetComponentInParent<Light2D>().pointLightOuterRadius -= amount;
    }
}
