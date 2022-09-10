using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RegenerableStatBuff")]
public class RegenerableStatBuff : ISkillEffect
{
    [SerializeField] private float value;
    [SerializeField] private UnityEvent<float> regenerateStat;
    public override void Use(Unit unit)
    {
        regenerateStat.Invoke(value*unit.stats.GetSpellPower());
    }
}
