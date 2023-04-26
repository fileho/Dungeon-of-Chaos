using UnityEngine;

/// <summary>
/// Class that represents all levels of a secondary attack and provides leveling info
/// </summary>
[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoSecondary")]
public class SkillInfoSecondaryAttack : SkillInfo<ISecondaryAttack>
{
    public void Upgrade()
    {
        level++;
    }
}
