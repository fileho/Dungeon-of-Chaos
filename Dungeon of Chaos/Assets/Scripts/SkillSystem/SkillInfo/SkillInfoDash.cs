using UnityEngine;

/// <summary>
/// Class that represents all levels of a dash skill and provides leveling info
/// </summary>
[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoDash")]
public class SkillInfoDash : SkillInfo<IDashSkill>
{
    public void Upgrade()
    {
        level++;
    }
}
