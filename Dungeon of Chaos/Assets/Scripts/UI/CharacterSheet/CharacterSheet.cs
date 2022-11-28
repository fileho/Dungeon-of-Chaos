using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField] private GameObject child;

    private SkillSlotDash skillSlotDash;
    private SkillSlotSecondary skillSlotSecondary;
    private ActivatedSkillSlots activatedSkillSlots;

    private SkillButtonActive[] skillButtonsActive;
    private SkillButtonDash[] skillButtonsDash;
    private SkillButtonSecondary[] skillButtonsSecondary;
    private SkillButtonPassive[] skillButtonsPassive;


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

        activatedSkillSlots = FindObjectOfType<ActivatedSkillSlots>();
        activatedSkillSlots.Init();

        skillButtonsActive = FindObjectsOfType<SkillButtonActive>();
        foreach (SkillButtonActive skillBtn in skillButtonsActive)
            skillBtn.Init();

        skillButtonsDash = FindObjectsOfType<SkillButtonDash>();
        foreach (SkillButtonDash skillBtn in skillButtonsDash)
            skillBtn.Init();

        skillSlotDash = FindObjectOfType<SkillSlotDash>();
        skillSlotDash.Init();

        skillButtonsSecondary = FindObjectsOfType<SkillButtonSecondary>();
        foreach (SkillButtonSecondary skillBtn in skillButtonsSecondary)
            skillBtn.Init();

        skillButtonsPassive = FindObjectsOfType<SkillButtonPassive>();
        foreach (SkillButtonPassive skillBtn in skillButtonsPassive)
            skillBtn.Init();

        skillSlotSecondary = FindObjectOfType<SkillSlotSecondary>();
        skillSlotSecondary.Init();
    }

    public void Close()
    {
        gameController.SaveAndReload();
    }
}
