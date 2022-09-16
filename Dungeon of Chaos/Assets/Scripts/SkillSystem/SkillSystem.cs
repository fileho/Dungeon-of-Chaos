using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private List<SkillInfoActive> activeSkills;
    [SerializeField] private List<SkillInfoPassive> passiveSkills;
    
    private List<SkillInfoActive> activeSkillsUnlocked;
    private List<SkillInfoPassive> passiveSkillsUnlocked;

    [SerializeField] private List<SkillInfoActive> activated;
    private List<SkillInfoPassive> equipped;

    [SerializeField] private SkillInfoActive activatedDash;
    private SkillInfoActive activatedSecondary;

    public static SkillSystem instance;

    [SerializeField] private int activeSkillsSlots;
    [SerializeField] private int passiveSkillsSlots;

    [SerializeField] private List<int> skillPointsRequired;

    public int skillPoints = 0;

    private void Awake()
    {
        //Debug.Log("Instance Skill System");
        instance = this;
        /*activated = new List<SkillInfoActive>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(null);*/
        equipped = new List<SkillInfoPassive>();
        for (int i = 0; i < passiveSkillsSlots; i++)
            equipped.Add(null);
    }

    public List<SkillInfoActive> GetActivatedSkills()
    {
        return activated;
    }

    public List<SkillInfoPassive> GetEquippedSkills()
    {
        return equipped;
    }

    public SkillInfoActive GetDash()
    {
        return activatedDash;
    }

    public SkillInfoActive GetSecondary()
    {
        return activatedSecondary;
    }

    public void Upgrade(SkillInfoActive skill)
    {
        skillPoints -= skillPointsRequired[skill.GetLevel()];
        if (!activeSkillsUnlocked.Contains(skill))
            activeSkillsUnlocked.Add(skill);
        skill.Upgrade();
    }

    public void Upgrade(SkillInfoPassive skill)
    {
        skillPoints -= skillPointsRequired[skill.GetLevel()];
        if (!passiveSkillsUnlocked.Contains(skill))
            passiveSkillsUnlocked.Add(skill);
        skill.Upgrade();
    }

    public void Activate(SkillInfoActive skill, int slot)
    {
        if (activated.Contains(skill))
        {
            int i = activated.IndexOf(skill);
            activated[i] = null;
        }
        activated[slot] = skill;
    }

    public void Equip(SkillInfoPassive skill, int slot)
    { 
        if (passiveSkillsUnlocked[slot] != null)
            passiveSkillsUnlocked[slot].Unequip(Character.instance.stats);
        passiveSkillsUnlocked[slot] = skill;
        skill.Equip(Character.instance.stats);
    }

    public bool IsUnlocked(SkillInfoActive skill)
    {
        return activeSkillsUnlocked.Contains(skill);
    }

    public bool IsUnlocked(SkillInfoPassive skill)
    {
        return passiveSkillsUnlocked.Contains(skill);
    }

    public bool CanUpgrade(SkillInfoActive skill)
    {
        return skillPoints >= skillPointsRequired[skill.GetLevel()];
    }
    public bool CanUpgrade(SkillInfoPassive skill)
    {
        return skillPoints >= skillPointsRequired[skill.GetLevel()] && skill.CanUpgrade();
    }

    public bool HasActivatedSkill()
    {
        foreach (var skill in activated)
        {
            if (skill != null)
                return true;
        }

        return false;
    }
}
