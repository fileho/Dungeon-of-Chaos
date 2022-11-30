using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoSecondary")]
public class SkillInfoSecondaryAttack : SkillInfo<ISecondaryAttack>
{
    public void Upgrade()
    {
        level++;
    }
}
