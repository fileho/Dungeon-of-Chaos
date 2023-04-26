/// <summary>
/// Abstract class handling behaviour of one level of a passive skill
/// </summary>
public abstract class IPassiveSkill : ISkill
{
    public abstract void Equip(Stats stats);
    public abstract void Unequip(Stats stats);

    public override string GetCostDescription()
    {
        return "";
    }
}
