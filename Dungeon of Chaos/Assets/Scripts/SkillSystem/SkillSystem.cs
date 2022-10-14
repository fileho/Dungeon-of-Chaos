using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class SavedSkillSystem
{
    public List<SavedSkillInfo> backlogActive;
    public List<SavedSkillInfo> backlogPassive;

    public List<int> activated;
    public List<int> equipped;

    // TODO add backlog for dashes and secondary attacks

    public SavedSkillSystem(List<SavedSkillInfo> backlogActive, List<SavedSkillInfo> backlogPassive,
                            List<int> activated, List<int> equipped)
    {
        this.backlogActive = backlogActive;
        this.backlogPassive = backlogPassive;
        this.activated = activated;
        this.equipped = equipped;
    }
}

public class SkillSystem : MonoBehaviour
{
    [SerializeField]
    private List<SkillInfoActive> activeSkills;
    [SerializeField]
    private List<SkillInfoPassive> passiveSkills;

    [SerializeField]
    private List<SkillInfoActive> activated;
    private List<SkillInfoPassive> equipped;
    // TODO add SkillInfoDash?? - probably yes
    [SerializeField]
    private SkillInfoActive activatedDash;
    private SkillInfoActive activatedSecondary;

    [SerializeField]
    private int activeSkillsSlots;
    [SerializeField]
    private int passiveSkillsSlots;

    [SerializeField]
    private List<int> skillPointsRequired;

    private List<SkillSlotActive> skillSlots = new List<SkillSlotActive>();

    private Unit owner;
    private Levelling levelling;

    private void Awake()
    {
        activated = new List<SkillInfoActive>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(activeSkills[0]);
        equipped = new List<SkillInfoPassive>();
        //    for (int i = 0; i < passiveSkillsSlots; i++)
        //        equipped.Add(null);
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
        return ((IDashSkill)activatedDash.GetCurrentSkill()).IsDashing();
    }

    public void DashCollision(Collision2D col)
    {
        ((IDashSkill)activatedDash.GetCurrentSkill()).TriggerCollision(col);
    }

    public void Upgrade(SkillInfoActive skill)
    {
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
        skill.Upgrade();
    }

    public void Upgrade(SkillInfoPassive skill)
    {
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
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
        if (equipped[slot] != null)
            equipped[slot].Unequip(Character.instance.stats);
        equipped[slot] = skill;
        skill.Equip(Character.instance.stats);
    }

    public bool IsUnlocked(SkillInfoActive skill)
    {
        return skill.IsUnlocked();
    }

    public bool IsUnlocked(SkillInfoPassive skill)
    {
        return skill.IsUnlocked();
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

    // helpers for saving
    private List<SavedSkillInfo> GetSavedInfo(List<SkillInfoActive> skillInfos)
    {
        List<SavedSkillInfo> ret = new List<SavedSkillInfo>(skillInfos.Count);
        foreach (var skillInfo in skillInfos)
        {
            ret.Add(skillInfo.Save());
        }
        return ret;
    }
    private List<SavedSkillInfo> GetSavedInfo(List<SkillInfoPassive> skillInfos)
    {
        List<SavedSkillInfo> ret = new List<SavedSkillInfo>(skillInfos.Count);
        foreach (var skillInfo in skillInfos)
        {
            ret.Add(skillInfo.Save());
        }
        return ret;
    }

    private List<int> GetIndices(List<SkillInfoActive> list)
    {
        List<int> ret = new List<int>(list.Capacity);
        foreach (var elem in list)
        {
            ret.Add(activeSkills.FindIndex(active => active.GetId() == elem.GetId()));
        }
        return ret;
    }
    private List<int> GetIndices(List<SkillInfoPassive> list)
    {
        List<int> ret = new List<int>(list.Capacity);
        foreach (var elem in list)
        {
            ret.Add(passiveSkills.FindIndex(passive => passive.GetId() == elem.GetId()));
        }
        return ret;
    }

    public SavedSkillSystem Save()
    {
        return new SavedSkillSystem(GetSavedInfo(activeSkills), GetSavedInfo(passiveSkills), GetIndices(activated),
                                    GetIndices(equipped));
    }

    private void LoadList(List<SkillInfoActive> target, List<int> indices)
    {
        Assert.AreEqual(target.Count, indices.Count);
        for (int i = 0; i < target.Count; i++)
        {
            target[i] = activeSkills[indices[i]];
        }
    }
    private void LoadList(List<SkillInfoPassive> target, List<int> indices)
    {
        Assert.AreEqual(target.Count, indices.Count);
        for (int i = 0; i < target.Count; i++)
        {
            target[i] = passiveSkills[indices[i]];
        }
    }

    public void Load(SavedSkillSystem saved)
    {
        Assert.AreEqual(activeSkills.Count, saved.backlogActive.Count);
        for (int i = 0; i < saved.backlogActive.Count; i++)
        {
            activeSkills[i].Load(saved.backlogActive[i]);
        }

        Assert.AreEqual(passiveSkills.Count, saved.backlogPassive.Count);
        for (int i = 0; i < saved.backlogPassive.Count; i++)
        {
            passiveSkills[i].Load(saved.backlogPassive[i]);
        }
        LoadList(activated, saved.activated);
        LoadList(equipped, saved.equipped);
    }
}
