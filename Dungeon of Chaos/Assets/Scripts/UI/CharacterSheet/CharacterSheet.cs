using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField] private GameObject child;
    private SkillSystem skillSystem;

    private void Start()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }
    public void Open()
    {
        child.SetActive(true);
        skillSystem.InitSkillSlots();
    }

    public void Close()
    {
        SaveSystem.instance.save.LoadLevel();
        child.SetActive(false);
    }

}
