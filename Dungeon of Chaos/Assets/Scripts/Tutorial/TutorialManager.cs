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

[System.SerializableAttribute]
public struct Keys
{
    public string name;
    public KeyCode[] KeyCodes;
}

public class TutorialManager : MonoBehaviour
{
    private GameObject bg;
    private Animator animator;
    private TutorialState currentState = TutorialState.Default;
    private Coroutine coroutine;
    private GameObject currentTutorial;

    public List<Keys> keys;

    private bool breakCorutine;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bg = transform.Find("BG").gameObject;
        //    Show(TutorialState.Movement);
    }

    public void Show(TutorialState state)
    {
        coroutine = StartCoroutine(ShowTutorial(state));
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

    IEnumerator ShowTutorial(TutorialState state)
    {
        currentState = state;
        if (currentState != TutorialState.Default && PlayerPrefs.GetInt(currentState.ToString(), 0) == 0)
        {
            Time.timeScale = 0.7f;
            currentTutorial = transform.Find(currentState.ToString()).gameObject;
            EnableDisableTutorialScreen(true);
            breakCorutine = false;

            KeyCode[] keycodes = keys[(int)currentState].KeyCodes;
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
