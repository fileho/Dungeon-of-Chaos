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

    Animator animator;
    TutorialState currentState = TutorialState.Default;
    Coroutine coroutine;

    public List<Keys> keys;

    // @TODO: Add Playerprefs
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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


    IEnumerator ShowTutorial(TutorialState state)
    {
        currentState = state;
        while (currentState != TutorialState.Default)
        {
            GameObject currentTutorial = transform.Find(currentState.ToString()).gameObject;
            currentTutorial.SetActive(true);
            animator.Play(currentState.ToString());
            bool pressed = false;

            foreach (KeyCode key in keys[(int)currentState].KeyCodes)
                if (Input.GetKeyDown(key))
                    pressed = true;

            while (!pressed)
                yield return null;

            yield return new WaitForSeconds(2);
            currentTutorial.SetActive(false);
            switch (state)
            {
                case TutorialState.Movement:
                    currentState = TutorialState.Dash;
                    break;

                case TutorialState.Dash:
                    currentState = TutorialState.Attack;
                    break;

                case TutorialState.Attack:
                    currentState = TutorialState.Default;
                    break;

                default:
                    currentState = TutorialState.Default;
                    break;
            }
        }
        yield return null;
    }
}
