using UnityEngine;

/// <summary>
/// Class that represents all levels of an active skill and provides leveling info
/// </summary>
[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoActive")]
public class SkillInfoActive : SkillInfo<IActiveSkill>
{
    public void Upgrade()
    {
        level++;
    }
}
