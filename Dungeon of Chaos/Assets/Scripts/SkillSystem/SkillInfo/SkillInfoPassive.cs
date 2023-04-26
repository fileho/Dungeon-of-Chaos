using UnityEngine;

/// <summary>
/// Class that represents all levels of a passive skill and provides leveling info
/// </summary>
[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoPassive")]
public class SkillInfoPassive : SkillInfo<IPassiveSkill>
{
    public void Upgrade()
    {
        Unequip(Character.instance.stats);
        level++;
        Equip(Character.instance.stats);
    }
    public void Equip(Stats stats)
    {
        if (level - 1 < 0)
            return;
        skills[level-1].Equip(stats);
    }

    public void Unequip(Stats stats)
    {
        if (level - 1 < 0)
            return;
        skills[level-1].Unequip(stats);
    }
}
