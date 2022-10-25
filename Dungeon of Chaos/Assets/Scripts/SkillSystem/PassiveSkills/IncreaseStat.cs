using UnityEngine;

public abstract class IncreaseStat : IPassiveSkill
{
    [SerializeField] private float amount;

    public override void Equip(Stats stats)
    {
        //TODO: Maybe the value could be slightly increased with level of the character?
        ChangeStat(stats, amount);
    }

    public override void Unequip(Stats stats)
    {
        ChangeStat(stats, -amount);
    }

    protected abstract void ChangeStat(Stats stats, float val);
}
