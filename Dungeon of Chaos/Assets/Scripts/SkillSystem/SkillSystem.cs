using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] private List<SkillInfoActive> activeSkills;
    [SerializeField] private List<SkillInfoPassive> passiveSkills;
    
    private List<SkillInfoActive> activeSkillsUnlocked = new List<SkillInfoActive>();
    private List<SkillInfoPassive> passiveSkillsUnlocked = new List<SkillInfoPassive>();

    [SerializeField] private List<SkillInfoActive> activated;
    private List<SkillInfoPassive> equipped;
    // TODO add SkillInfoDash?? - probably yes
    [SerializeField] private SkillInfoActive activatedDash;
    private SkillInfoActive activatedSecondary;

    [SerializeField] private int activeSkillsSlots;
    [SerializeField] private int passiveSkillsSlots;

    [SerializeField] private List<int> skillPointsRequired;

    private List<SkillSlotActive> skillSlots = new List<SkillSlotActive>();

    private Unit owner;
    private Levelling levelling;

    private void Awake()
    {
        activated = new List<SkillInfoActive>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(null);
        equipped = new List<SkillInfoPassive>();
        for (int i = 0; i < passiveSkillsSlots; i++)
            equipped.Add(null);
        foreach (SkillInfoActive skill in activeSkills)
            skill.ResetLevel();
    }

    public void InitSkillSlots()
    {
        if (skillSlots.Count == activeSkillsSlots)
            return;
        SkillSlotActive[] slots = FindObjectsOfType<SkillSlotActive>();
        for (int i = 0; i < activeSkillsSlots; i++)
            skillSlots.Add(null);
        foreach (SkillSlotActive slot in slots)
        {
            skillSlots[slot.GetIndex()] = slot;
        }
    }

    public void Init(Unit owner)
    {
        this.owner = owner;
        ((IDashSkill)activatedDash.GetCurrentSkill()).Init(owner);
        levelling = owner.stats.GetLevellingData();
    }



    public void UpdateCooldowns()
    {
        foreach (var skill in activated)
        {
            if (skill)
                skill.GetCurrentSkill().UpdateCooldown();
        }

        activatedDash.GetCurrentSkill().UpdateCooldown();
        if (activatedSecondary)
            activatedSecondary.GetCurrentSkill().UpdateCooldown();
    }

    public void Dash(Vector2 dir)
    {
        ((IDashSkill)activatedDash.GetCurrentSkill()).Use(owner, null, new List<Vector2>() { dir });
    }

    public void UseSkill(int index)
    {
        SkillInfoActive skill = activated[index];
        if (skill)
            skill.GetCurrentSkill().Use(owner);
    }

    public bool IsDashing()
    {
        return ((IDashSkill) activatedDash.GetCurrentSkill()).IsDashing();
    }

    public void DashCollision(Collision2D col)
    {
        ((IDashSkill)activatedDash.GetCurrentSkill()).TriggerCollision(col);
    }

    public void Upgrade(SkillInfoActive skill)
    {
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        if (!activeSkillsUnlocked.Contains(skill))
            activeSkillsUnlocked.Add(skill);
        skill.Upgrade();
    }

    public void Upgrade(SkillInfoPassive skill)
    {
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        if (!passiveSkillsUnlocked.Contains(skill))
            passiveSkillsUnlocked.Add(skill);
        skill.Upgrade();
    }

    public void Activate(SkillInfoActive skill, int slot)
    {
        if (activated.Contains(skill))
        {
            int i = activated.IndexOf(skill);
            if (activated[slot] != null)
            {
                activated[i] = activated[slot];
                skillSlots[i].SetImage(activated[i].GetSkillData().GetIcon());
            }
            else
            {
                activated[i] = null;
                skillSlots[i].SetImage(null);
            }
        }
        activated[slot] = skill;
        skillSlots[slot].SetImage(skill.GetSkillData().GetIcon());
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
        return levelling.skillPoints >= skillPointsRequired[skill.GetLevel()];
    }
    public bool CanUpgrade(SkillInfoPassive skill)
    {
        return levelling.skillPoints >= skillPointsRequired[skill.GetLevel()] && skill.CanUpgrade();
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

    public bool IsActivated(SkillInfoActive skillInfo)
    {
        return activated.Contains(skillInfo);
    }

    public bool IsEquipped(SkillInfoPassive skillInfo)
    {
        return equipped.Contains(skillInfo);
    }
}
