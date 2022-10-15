using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField]
    private GameObject child;
    [SerializeField] private ActivatedSkillSlots activatedSkillSlots;
    private SkillButtonActive[] skillButtonsActive;

    private GameController gameController;

    private void Start()
    {
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
        activatedSkillSlots.InitSkillSlots();
        skillButtonsActive = FindObjectsOfType<SkillButtonActive>();
        foreach (SkillButtonActive skillBtn in skillButtonsActive)
            skillBtn.Init();
    }

    public void Close()
    {
        gameController.SaveAndReload();
    }
}
