using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TutorialState
{
    Movement = 0,
    Dash,
    Attack,
    Skills,
    EnemyAttack,
    Checkpoint,
    MapUnlock,
    Boss,
    Default = 999,
}

public class TutorialManager : MonoBehaviour
{
    private GameObject bg;
    private Animator animator;
    private TutorialState currentState = TutorialState.Default;
    private GameObject currentTutorial;

    private bool breakCorutine;

    private SaveSystem saveSystem;

    void Start()
    {
        animator = GetComponent<Animator>();
        saveSystem = FindObjectOfType<SaveSystem>();
        bg = transform.Find("BG").gameObject;
    }

    public void Show(TutorialState state)
    {
        StartCoroutine(ShowTutorial(state));
    }

    public void Hide()
    {
        currentState = TutorialState.Default;
        breakCorutine = true;
        EnableDisableTutorialScreen(false);
    }

    void EnableDisableTutorialScreen(bool state)
    {
        bg.SetActive(state);
        if (currentTutorial)
            currentTutorial.SetActive(state);

        if (state)
            animator.Play(currentState.ToString());
        else
            animator.StopPlayback();
    }

    public bool AlreadyUsed(TutorialState state)
    {
        return saveSystem.TutorialData.HasState((int)state);
    }

    /// <summary>
    /// Shows the given tutorial
    /// </summary>
    /// <param name="state">tutorial to show</param>
    IEnumerator ShowTutorial(TutorialState state)
    {
        currentState = state;
        // Check if it was already shown
        if (currentState != TutorialState.Default && !saveSystem.TutorialData.HasState((int)state))
        {
            Time.timeScale = 0.7f;
            currentTutorial = transform.Find(currentState.ToString()).gameObject;
            EnableDisableTutorialScreen(true);
            breakCorutine = false;

            while (!breakCorutine)
            {
                yield return null;
            }

            saveSystem.TutorialData.SaveState((int)state);

            EnableDisableTutorialScreen(false);
            Time.timeScale = 1f;
        }
        yield return null;
    }
}
