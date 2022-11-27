using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static UnityEngine.Rendering.DebugUI;


public enum TutorialState
{
    Movement = 0,
    Dash,
    Attack,
    Skills,
    EnemyAttack,
    CheckPoint,
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
        //PlayerPrefsManager.ClearData();
        currentState = state;
        //print(currentState.ToString() + "__" + PlayerPrefsManager.Tutorial[currentState.ToString()]);

        while (currentState != TutorialState.Default && !PlayerPrefsManager.Tutorial[currentState.ToString()])
        {
            currentTutorial = transform.Find(currentState.ToString()).gameObject;
            EnableDisableTutorialScreen(true);
            bool pressed = false;


            KeyCode[] keycodes = keys[(int)currentState].KeyCodes;
            while (!pressed)
            {
                if (keycodes.Length > 0)
                {
                    foreach (KeyCode key in keycodes)
                        if (Input.GetKeyDown(key))
                            pressed = true;
                }
                else
                {
                    if (Input.anyKeyDown)
                        pressed = true;
                }
                yield return null;
            }


            PlayerPrefsManager.Tutorial[currentState.ToString()] = true;
            //print(currentState.ToString() + "__" + PlayerPrefsManager.Tutorial[currentState.ToString()]);

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
                case TutorialState.Attack:
                    currentState = TutorialState.Skills;
                    break;
                default:
                    currentState = TutorialState.Default;
                    break;
            }
        }
        yield return null;
    }
}
