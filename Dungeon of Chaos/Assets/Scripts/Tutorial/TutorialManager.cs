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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bg = transform.Find("BG").gameObject;
        Show(TutorialState.Movement);
    }

    public void Show(TutorialState state)
    {
        coroutine = StartCoroutine(ShowTutorial(state));
    }

    public void Hide()
    {
        currentState = TutorialState.Default;
        StopCoroutine(coroutine);
        EnableDisableTutorialScreen(false);
    }

    void EnableDisableTutorialScreen(bool state)
    {
        bg.SetActive(state);
        currentTutorial.SetActive(state);

        if (state)
            animator.Play(currentState.ToString());
        else
            animator.StopPlayback();
    }

    IEnumerator ShowTutorial(TutorialState state)
    {
        currentState = state;

        while (currentState != TutorialState.Default && PlayerPrefs.GetInt(currentState.ToString(), 0) == 0)
        {
            Character.instance.BlockInput();
            currentTutorial = transform.Find(currentState.ToString()).gameObject;
            EnableDisableTutorialScreen(true);
            bool pressed = false;

            KeyCode[] keycodes = keys[(int)currentState].KeyCodes;
            while (!pressed)
            {
                if (keycodes.Length > 0)
                {
                    if (keycodes.Any(key => Input.GetKeyDown(key)))
                        break;
                }
                else
                {
                    if (Input.anyKeyDown)
                        pressed = true;
                }
                yield return null;
            }

            Character.instance.UnblockInput();
            Character.instance.UseSkills();
            PlayerPrefs.SetInt(currentState.ToString(), 1);
            EnableDisableTutorialScreen(false);
            yield return new WaitForSeconds(0.5f);

            switch (currentState)
            {
            case TutorialState.Movement:
                currentState = TutorialState.Dash;
                break;
            case TutorialState.Dash:
                currentState = TutorialState.Attack;
                break;
            default:
                currentState = TutorialState.Default;
                break;
            }
        }
        yield return null;
    }
}
