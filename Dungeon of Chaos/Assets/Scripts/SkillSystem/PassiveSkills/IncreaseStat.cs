using UnityEngine;

public abstract class IncreaseStat : IPassiveSkill
{
    [SerializeField] private float amount;

    private float value;

    public override void Equip(Stats stats)
    {
        value = amount * (1 + stats.GetLevel() * 0.2f);
        ChangeStat(stats, value);
    }

    public override void Unequip(Stats stats)
    {
        ChangeStat(stats, -value);
    }

    protected abstract void ChangeStat(Stats stats, float val);

    public override string GetEffectDescription()
    {
        value = amount * (1 + Character.instance.stats.GetLevel() * 0.2f);
        return string.Format(skillData.GetDescription(), value.ToString());
    }
}
