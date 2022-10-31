using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipContent : MonoBehaviour
{
    [SerializeField] public GameObject contentHeader;
    [SerializeField] public GameObject description;

    public void Fill(string cH, string des)
    {
        bool head = FillField(cH, contentHeader);
        bool d = FillField(des, description);

        this.enabled = (head || d);
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
