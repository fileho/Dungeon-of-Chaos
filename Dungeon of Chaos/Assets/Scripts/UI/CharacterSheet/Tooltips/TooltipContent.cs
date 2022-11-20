using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipContent : MonoBehaviour
{
    [SerializeField] public GameObject contentHeader;
    [SerializeField] public GameObject description;
    [SerializeField] public GameObject cost;
    [SerializeField] public GameObject requirements;

    public void Fill(SkillDescription skillDes)
    {
        bool head = FillField(skillDes.header, contentHeader);
        bool d = FillField(skillDes.description, description);
        bool c = FillField(skillDes.cost, this.cost);
        bool r = FillField(skillDes.requirements, requirements);

        this.enabled = (head || d || c || r);
    }

    private bool FillField(string s, GameObject obj)
    {
        if (s.Length == 0)
        {
            obj.SetActive(false);
            return false;
        }
        obj.GetComponent<TextMeshProUGUI>().text = s;
        obj.SetActive(true);
        return true;
    }
}
