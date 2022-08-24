using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    private List<SkillInfoActive> activeSkills;
    private List<SkillInfoPassive> passiveSkills;
    
    private List<SkillInfoActive> activeSkillsUnlocked;
    private List<SkillInfoPassive> passiveSkillsUnlocked;

    private List<SkillInfoActive> activated;
    private List<SkillInfoPassive> equipped;
}
