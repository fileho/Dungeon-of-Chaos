public abstract class IPassiveSkill : ISkill
{
    public abstract void Equip(Stats stats);
    public abstract void Unequip(Stats stats);

    public override string GetCostDescription()
    {
        return "";
    }
}
