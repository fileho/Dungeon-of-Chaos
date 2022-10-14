using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField]
    private GameObject child;
    private SkillSystem skillSystem;

    private GameController gameController;

    private void Start()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
        gameController = FindObjectOfType<GameController>();
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
        gameController.SaveAndReload();
    }
}
