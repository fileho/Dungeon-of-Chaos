using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;

public class CharacterSheet : MonoBehaviour
{
    [SerializeField] private GameObject child;
    [SerializeField] private UIView uiView;

    private SkillsUI skillsUI;


    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if (!child.activeSelf)
            return;
        if (Input.GetKeyDown(KeyCode.Escape))
            Close();
    }
    public void Open()
    {
        child.SetActive(true);

        skillsUI = FindObjectOfType<SkillsUI>();
        skillsUI.UpdateSkillsUI();
    }

    public void Close()
    {
        uiView.Hide();
        gameController.SaveAndReload();
    }
}
