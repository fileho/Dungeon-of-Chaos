using UnityEngine;

/// <summary>
/// Data shown in the UI about the skill
/// </summary>
[System.Serializable]
public class SkillData
{
    [SerializeField] private string name;
    [TextArea()]
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }
}

/// <summary>
/// Abstract class for one level of a skill
/// </summary>
public abstract class ISkill : ScriptableObject
{
    [SerializeField] protected SkillData skillData;

    public SkillData GetSkillData()
    {
        return skillData;
    }


    /// <summary>
    /// Returns description of the skill behavior with inserted values based on stats
    /// </summary>
    public abstract string GetSkillDescription();

    public abstract string GetCostDescription();
}
