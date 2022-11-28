using UnityEngine;


[System.Serializable]
public class SkillData
{
    [SerializeField] private string name;
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
public abstract class ISkill : ScriptableObject
{
    [SerializeField] protected SkillData skillData;

    public SkillData GetSkillData()
    {
        return skillData;
    }

    public abstract string GetEffectDescription();

    public abstract string GetCostDescription();
}
