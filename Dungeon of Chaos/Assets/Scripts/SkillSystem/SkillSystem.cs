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

    public static SkillSystem instance;

    [SerializeField] private int activeSkillsSlots;
    [SerializeField] private int passiveSkillsSlots;

    private int skillPoints = 0;

    private void Awake()
    {
        instance = this;
        activated = new List<SkillInfoActive>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(null);
        equipped = new List<SkillInfoPassive>();
        for (int i = 0; i < passiveSkillsSlots; i++)
            equipped.Add(null);
    }

    public List<SkillInfoActive> GetActivatedSkills()
    {
        return activated;
    }

    public void Upgrade(SkillInfoActive skill)
    {
        if (skill.CanUpgrade())
            skill.Upgrade();
    }

    public void Upgrade(SkillInfoPassive skill)
    {
        if (skill.CanUpgrade())
            skill.Upgrade();
    }

    public void Activate(SkillInfoActive skill, int slot)
    {
        if (!activeSkillsUnlocked.Contains(skill))
            return;
        activated[slot] = skill;
    }

    public void Equip(SkillInfoPassive skill, int slot)
    {
        if (!passiveSkillsUnlocked.Contains(skill))
            return;
        if (passiveSkillsUnlocked[slot] != null)
            passiveSkillsUnlocked[slot].Unequip(Character.instance.stats);
        passiveSkillsUnlocked[slot] = skill;
        skill.Equip(Character.instance.stats);
    }
}
