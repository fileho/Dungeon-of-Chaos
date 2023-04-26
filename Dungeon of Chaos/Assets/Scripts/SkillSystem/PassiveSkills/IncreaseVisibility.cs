using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Passive skill that increases the range of light around the character
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/IncreaseVisibility")]
public class IncreaseVisibility : IPassiveSkill
{
    [SerializeField] private float amount;

    public override void Equip(Stats stats)
    {
        Character.instance.GetComponentInParent<Light2D>().pointLightOuterRadius += amount;
    }

    public override string GetSkillDescription()
    {
        return skillData.GetDescription();
    }

    public override void Unequip(Stats stats)
    {
        Character.instance.GetComponentInParent<Light2D>().pointLightOuterRadius -= amount;
    }
}
