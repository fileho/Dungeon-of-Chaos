using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attributes
{
    None,
    Strength,
    Intelligence,
    Constitution,
    Endurance,
    Wisdom
};

[System.Serializable]
public class UnlockingRequirements
{
    [System.Serializable]
    public class AttributeRequirement
    {
        public Attributes attribute = Attributes.None;
        public float value = 0;

        public AttributeRequirement()
        {
            attribute = Attributes.None;
            value = 0;
        }
        public AttributeRequirement(Attributes att, float val)
        {
            attribute = att;
            value = val;
        }
    }

    [SerializeField] private int skillPointsCost = 1;
    [SerializeField] private int level = 2;
    [SerializeField] private AttributeRequirement primaryAttribute;
    [SerializeField] private AttributeRequirement secondaryAttribute;
    [SerializeField] private string skillKey = "";

    public UnlockingRequirements(int cost, int level, AttributeRequirement primary, AttributeRequirement secondary, string skill)
    {
        skillPointsCost = cost;
        this.level = level;
        primaryAttribute = primary;
        secondaryAttribute = secondary;
        skillKey = skill;
    }

    public UnlockingRequirements(UnlockingRequirements req, int skillLvl)
    {
        skillPointsCost = 1;
        level = req.GetLevelRequirement() + skillLvl;
        primaryAttribute = new AttributeRequirement(req.GetPrimaryAttributeType(), req.GetPrimaryAttribute() + skillLvl);
        secondaryAttribute = new AttributeRequirement(req.GetSecondaryAttributeType(), req.GetSecondaryAttribute() + skillLvl);
        skillKey = "";
    }

    public UnlockingRequirements()
    {
        skillPointsCost = 1;
        level = 2;
        primaryAttribute = new AttributeRequirement();
        secondaryAttribute = new AttributeRequirement();
        skillKey = "";
    }

    public int GetCost() { return skillPointsCost; }
    public int GetLevelRequirement() { return level; }

    public Attributes GetPrimaryAttributeType() { return primaryAttribute.attribute; }
    public Attributes GetSecondaryAttributeType() { return secondaryAttribute.attribute; }

    public float GetPrimaryAttribute() { return primaryAttribute.value; }
    public float GetSecondaryAttribute() { return secondaryAttribute.value; }
    public string GetSkillKey() { return skillKey; }

    public string GetRequirementsDescription()
    {
        string cost = "Skill Pts: " + skillPointsCost.ToString();
        string lvl = "Lvl: " + level.ToString();
        string prim = GetAttributeDescription(primaryAttribute);
        string sec = GetAttributeDescription(secondaryAttribute);

        return "Requirements: " + "\n" + cost + " " + lvl + " " + prim + " " + sec + " " + skillKey;
    }

    private string GetAttributeDescription(AttributeRequirement attReq)
    {
        switch (attReq.attribute)
        {
            case Attributes.None:
                return "";
            case Attributes.Strength:
                return attReq.value > 0
                    ? "Str: " + attReq.value.ToString()
                    : "";
            case Attributes.Intelligence:
                return attReq.value > 0
                    ? "Int: " + attReq.value.ToString()
                    : "";
            case Attributes.Constitution:
                return attReq.value > 0
                    ? "Con: " + attReq.value.ToString()
                    : "";
            case Attributes.Endurance:
                return attReq.value > 0
                    ? "End: " + attReq.value.ToString()
                    : "";
            case Attributes.Wisdom:
                return attReq.value > 0
                    ? "Wis: " + attReq.value.ToString()
                    : "";
            default:
                return "";
        }
    }
}
