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
    private List<int> activated;
    private List<int> equipped;
    // TODO add SkillInfoDash?? - probably yes
    [SerializeField]
    private SkillInfoActive activatedDash;
    private SkillInfoActive activatedSecondary;

    [SerializeField]
    private int activeSkillsSlots;
    [SerializeField]
    private int passiveSkillsSlots;

    private ActivatedSkillSlots activatedSkillSlots;

    [SerializeField]
    private List<int> skillPointsRequired;

    private Unit owner;
    private Levelling levelling;

    private void Awake()
    {
        activated = new List<int>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(-1);
        equipped = new List<int>();
        for (int i = 0; i < passiveSkillsSlots; i++)
           equipped.Add(-1);
        foreach (SkillInfoActive skill in activeSkills)
            skill.ResetLevel();
    }

    public int GetActiveSkillSlots()
    {
        return activeSkillsSlots;
    }

    public void Init(Unit owner)
    {
        this.owner = owner;
        ((IDashSkill)activatedDash.GetCurrentSkill()).Init(owner);
        levelling = owner.stats.GetLevellingData();
    }

    private bool IsValidActive(int index)
    {
        return index >= 0 && index < activeSkills.Count;
    }

    private bool IsValidPassive(int index)
    {
        return index >= 0 && index < passiveSkills.Count;
    }

    public void UpdateCooldowns()
    {
        foreach (int index in activated)
        {
            if (IsValidActive(index))
                activeSkills[index].GetCurrentSkill().UpdateCooldown();
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
        if (IsValidActive(index))
        {
            SkillInfoActive skill = activeSkills[activated[index]];
            skill.GetCurrentSkill().Use(owner);
        }
    }

    public bool IsDashing()
    {
        return ((IDashSkill)activatedDash.GetCurrentSkill()).IsDashing();
    }

    public void DashCollision(Collision2D col)
    {
        ((IDashSkill)activatedDash.GetCurrentSkill()).TriggerCollision(col);
    }

    public void UpgradeActive(int index)
    {
        if (!IsValidActive(index))
            return;
        SkillInfoActive skill = activeSkills[index];
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
        skill.Upgrade();
    }

    public void UpgradePassive(int index)
    {
        if (!IsValidPassive(index))
            return;
        SkillInfoPassive skill = passiveSkills[index];
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
        skill.Upgrade();
    }

    public void Activate(int index, int slot)
    {
        if (activated.Contains(index))
        {
            int i = activated.IndexOf(index);
            if (activated[slot] != -1)
            {
                activated[i] = activated[slot];
            }
            else
            {
                activated[i] = -1;
            }
        }
        activated[slot] = index;
    }

    public void Equip(int index, int slot)
    {
        if (equipped[slot] != -1)
            passiveSkills[equipped[slot]].Unequip(Character.instance.stats);
        equipped[slot] = index;
        passiveSkills[index].Equip(Character.instance.stats);
    }

    public bool IsUnlockedActive(int index)
    {
        return IsValidActive(index) && activeSkills[index].IsUnlocked();
    }

    public bool IsUnlockedPassive(int index)
    {
        return IsValidPassive(index) && passiveSkills[index] .IsUnlocked();
    }

    public bool CanUpgradeActive(int index)
    {
        return IsValidActive(index) && levelling.skillPoints >= skillPointsRequired[activeSkills[index].GetLevel()] && activeSkills[index].CanUpgrade();
    }
    public bool CanUpgradePassive(int index)
    {
        return IsValidPassive(index) && levelling.skillPoints >= skillPointsRequired[passiveSkills[index].GetLevel()] && passiveSkills[index].CanUpgrade();
    }

    public bool HasActivatedSkill()
    {
        foreach (var skill in activated)
        {
            if (skill != -1)
                return true;
        }
        return false;
    }

    public bool IsActivated(int index)
    {
        return activated.Contains(index);
    }

    public bool IsEquipped(int index)
    {
        return equipped.Contains(index);
    }

    public SkillInfoActive GetActivatedSkill(int index)
    {
        return GetSkillInfoActive(activated[index]);
    }

    public SkillInfoPassive GetEquippedSkill(int index)
    {
        return GetSkillInfoPassive(equipped[index]);
    }    

    public SkillInfoActive GetSkillInfoActive(int index)
    {
        return IsValidActive(index) ? activeSkills[index] : null;
    }

    public SkillInfoPassive GetSkillInfoPassive(int index)
    {
        return IsValidPassive(index) ? passiveSkills[index] : null;
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

    public SavedSkillSystem Save()
    {
        return new SavedSkillSystem(GetSavedInfo(activeSkills), GetSavedInfo(passiveSkills), activated,
                                    equipped);
    }

    private void LoadList(List<int> target, List<int> indices)
    {
        Assert.AreEqual(target.Count, indices.Count);
        for (int i = 0; i < target.Count; i++)
        {
            target[i] = indices[i];
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
