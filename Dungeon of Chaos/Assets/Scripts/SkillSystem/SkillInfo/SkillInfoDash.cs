using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoDash")]
public class SkillInfoDash : SkillInfo<IDashSkill>
{
    public void Upgrade()
    {
        level++;
    }
}
