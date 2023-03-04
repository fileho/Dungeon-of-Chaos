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
    Default = 999,
}

public class TutorialManager : MonoBehaviour
{
    private GameObject bg;
    private Animator animator;
    private TutorialState currentState = TutorialState.Default;
    private GameObject currentTutorial;

    private bool breakCorutine;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        return PlayerPrefs.GetInt(state.ToString(), 0) != 0;
    }

    IEnumerator ShowTutorial(TutorialState state)
    {
        currentState = state;
        if (currentState != TutorialState.Default && PlayerPrefs.GetInt(currentState.ToString(), 0) == 0)
        {
            Time.timeScale = 0.7f;
            currentTutorial = transform.Find(currentState.ToString()).gameObject;
            EnableDisableTutorialScreen(true);
            breakCorutine = false;

            while (!breakCorutine)
            {
                yield return null;
            }

            PlayerPrefs.SetInt(state.ToString(), 1);

            EnableDisableTutorialScreen(false);
            Time.timeScale = 1f;
        }
        yield return null;
    }
}
