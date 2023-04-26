using UnityEngine;
using TMPro;

/// <summary>
/// Part of the skill tooltip
/// </summary>
public class TooltipContent : MonoBehaviour
{
    [SerializeField] public GameObject contentHeader;
    [SerializeField] public GameObject description;
    [SerializeField] public GameObject cost;
    [SerializeField] public GameObject requirements;

    [SerializeField] private GameObject separator;

    public void Fill(SkillDescription skillDes)
    {
        bool head = FillField(skillDes.header, contentHeader);
        bool d = FillField(skillDes.description, description);
        bool c = FillField(skillDes.cost, this.cost);
        bool r = FillField(skillDes.requirements, requirements);

        enabled = (head || d || c || r);
        separator.SetActive(this.enabled);
        separator.GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().rect.width, 3);
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
