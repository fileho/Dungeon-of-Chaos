using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class SavedSkillSystem
{
    public List<SavedSkillInfo> backlogActive;
    public List<SavedSkillInfo> backlogPassive;
    public List<SavedSkillInfo> backlogDash;
    public List<SavedSkillInfo> backlogSecondary;

    public List<int> activated;
    public int activatedDash;
    public int activatedSecondary;

    public SavedSkillSystem(List<SavedSkillInfo> backlogActive, List<SavedSkillInfo> backlogPassive, List<SavedSkillInfo> backlogDash, List<SavedSkillInfo> backlogSecondary,
                            List<int> activated, int activatedDash, int activatedSecondary)
    {
        this.backlogActive = backlogActive;
        this.backlogPassive = backlogPassive;
        this.backlogDash = backlogDash;
        this.backlogSecondary = backlogSecondary;
        this.activated = activated;
        this.activatedDash = activatedDash;
        this.activatedSecondary = activatedSecondary;
    }
}

public class SkillSystem : MonoBehaviour
{
    [SerializeField]
    private List<SkillInfoActive> activeSkills;
    [SerializeField]
    private List<SkillInfoPassive> passiveSkills;
    [SerializeField]
    private List<SkillInfoDash> dashSkills;
    [SerializeField]
    private List<SkillInfoSecondaryAttack> secondaryAttacks;

    private List<int> activated;

    private int activatedDash;
    private int activatedSecondary;

    [SerializeField]
    private int activeSkillsSlots;

    [SerializeField]
    private List<int> skillPointsRequired;

    private Unit owner;
    private Levelling levelling;

    private void Awake()
    {
        activated = new List<int>();
        for (int i = 0; i < activeSkillsSlots; i++)
            activated.Add(-1);
        foreach (SkillInfoActive skill in activeSkills)
            skill.ResetLevel();
        activatedDash = 0;
        activatedSecondary = -1;
    }

    public void Init(Unit owner)
    {
        this.owner = owner;
        dashSkills[activatedDash].GetCurrentSkill().Init(owner);
        if (IsValidSecondary(activatedSecondary))
            secondaryAttacks[activatedSecondary].GetCurrentSkill().Init(owner);
        levelling = owner.stats.GetLevellingData();
    }

    public void UpdateCooldowns()
    {
        foreach (int index in activated)
        {
            if (IsValidActive(index))
                activeSkills[index].GetCurrentSkill().UpdateCooldown();
        }

        dashSkills[activatedDash].GetCurrentSkill().UpdateCooldown();
        if (IsValidSecondary(activatedSecondary))
            secondaryAttacks[activatedSecondary].GetCurrentSkill().UpdateCooldown();
    }

    #region ActiveSkills
    public int GetActiveSkillSlots()
    {
        return activeSkillsSlots;
    }
    private bool IsValidActive(int index)
    {
        return index >= 0 && index < activeSkills.Count;
    }

    public void UseSkill(int index)
    {
        if (IsValidActive(index) && IsValidActive(activated[index]))
        {
            SkillInfoActive skill = activeSkills[activated[index]];
            skill.GetCurrentSkill().Use(owner);
        }
    }
    public bool CanUpgradeActive(int index)
    {
        return IsValidActive(index) && levelling.skillPoints >= skillPointsRequired[activeSkills[index].GetLevel()] && activeSkills[index].CanUpgrade();
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

    public bool IsUnlockedActive(int index)
    {
        return IsValidActive(index) && activeSkills[index].IsUnlocked();
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

    public SkillInfoActive GetSkillInfoActive(int index)
    {
        return IsValidActive(index) ? activeSkills[index] : null;
    }

    public SkillInfoActive GetActivatedSkill(int index)
    {
        return GetSkillInfoActive(activated[index]);
    }
    #endregion

    #region PassiveSkills
    private bool IsValidPassive(int index)
    {
        return index >= 0 && index < passiveSkills.Count;
    }

    public bool CanUpgradePassive(int index)
    {
        return IsValidPassive(index) && levelling.skillPoints >= skillPointsRequired[passiveSkills[index].GetLevel()] && passiveSkills[index].CanUpgrade();
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

    public bool IsUnlockedPassive(int index)
    {
        return IsValidPassive(index) && passiveSkills[index].IsUnlocked();
    }

    public SkillInfoPassive GetSkillInfoPassive(int index)
    {
        return IsValidPassive(index) ? passiveSkills[index] : null;
    }
    #endregion

    #region Dash
    private bool IsValidDash(int index)
    {
        return index >= 0 && index < dashSkills.Count;
    }

    public void Dash(Vector2 dir)
    {
        dashSkills[activatedDash].GetCurrentSkill().Use(owner, null, new List<Vector2>() { dir });
    }

    public bool IsDashing()
    {
        return dashSkills[activatedDash].GetCurrentSkill().IsDashing();
    }

    public void DashCollision(Collision2D col)
    {
        dashSkills[activatedDash].GetCurrentSkill().TriggerCollision(col);
    }

    public bool CanUpgradeDash(int index)
    {
        return IsValidDash(index) && levelling.skillPoints >= skillPointsRequired[dashSkills[index].GetLevel()] && dashSkills[index].CanUpgrade();
    }

    public void UpgradeDash(int index)
    {
        if (!IsValidDash(index))
            return;
        SkillInfoDash skill = dashSkills[index];
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
        skill.Upgrade();
    }

    public void ActivateDash(int index)
    {
        activatedDash = index;
    }

    public bool IsUnlockedDash(int index)
    {
        return IsValidDash(index) && dashSkills[index].IsUnlocked();
    }

    public SkillInfoDash GetSkillInfoDash(int index)
    {
        return IsValidDash(index) ? dashSkills[index] : null;
    }

    public SkillInfoDash GetActivatedDash()
    {
        return GetSkillInfoDash(activatedDash);
    }

    #endregion

    #region SecondaryAttack
    private bool IsValidSecondary(int index)
    {
        return index >= 0 && index < secondaryAttacks.Count;
    }

    public void SecondaryAttack()
    {
        if (IsValidSecondary(activatedSecondary))
            secondaryAttacks[activatedSecondary].GetCurrentSkill().Use(owner, null, null);
    }

    public bool IsAttacking()
    {
        return secondaryAttacks[activatedSecondary].GetCurrentSkill().IsAttacking();
    }

    public bool CanUpgradeSecondaryAttack(int index)
    {
        return IsValidSecondary(index) && levelling.skillPoints >= skillPointsRequired[secondaryAttacks[index].GetLevel()] && secondaryAttacks[index].CanUpgrade();
    }

    public void UpgradeSecondary(int index)
    {
        if (!IsValidSecondary(index))
            return;
        SkillInfoSecondaryAttack skill = secondaryAttacks[index];
        levelling.skillPoints -= skillPointsRequired[skill.GetLevel()];
        skill.Unlock();
        skill.Upgrade();
    }

    private void DeactivateSecondary()
    {
        if (IsValidSecondary(activatedSecondary))
        {
            secondaryAttacks[activatedSecondary].GetCurrentSkill().Deactivate();
            activatedSecondary = -1;
        }
    }

    public void ActivateSecondaryAttack(int index)
    {
        DeactivateSecondary();
        activatedSecondary = index;
        secondaryAttacks[activatedSecondary].GetCurrentSkill().Init(owner);
    }

    public bool IsUnlockedSecondary(int index)
    {
        return IsValidSecondary(index) && secondaryAttacks[index].IsUnlocked();
    }

    public SkillInfoSecondaryAttack GetSkillInfoSecondary(int index)
    {
        return IsValidSecondary(index) ? secondaryAttacks[index] : null;
    }

    public SkillInfoSecondaryAttack GetActivatedSecondary()
    {
        return GetSkillInfoSecondary(activatedSecondary);
    }
    #endregion

    #region SkillSaving
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

    private List<SavedSkillInfo> GetSavedInfo(List<SkillInfoDash> skillInfos)
    {
        List<SavedSkillInfo> ret = new List<SavedSkillInfo>(skillInfos.Count);
        foreach (var skillInfo in skillInfos)
        {
            ret.Add(skillInfo.Save());
        }
        return ret;
    }

    private List<SavedSkillInfo> GetSavedInfo(List<SkillInfoSecondaryAttack> skillInfos)
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
        return new SavedSkillSystem(GetSavedInfo(activeSkills), GetSavedInfo(passiveSkills), GetSavedInfo(dashSkills), GetSavedInfo(secondaryAttacks),
            activated, activatedDash, activatedSecondary);
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

        Assert.AreEqual(dashSkills.Count, saved.backlogDash.Count);
        for (int i = 0; i < saved.backlogDash.Count; i++)
        {
            dashSkills[i].Load(saved.backlogDash[i]);
        }

        Assert.AreEqual(secondaryAttacks.Count, saved.backlogSecondary.Count);
        for (int i = 0; i < saved.backlogSecondary.Count; i++)
        {
            secondaryAttacks[i].Load(saved.backlogSecondary[i]);
        }

        LoadList(activated, saved.activated);
        activatedDash = saved.activatedDash;
        activatedSecondary = saved.activatedSecondary;
    }
    #endregion
}
