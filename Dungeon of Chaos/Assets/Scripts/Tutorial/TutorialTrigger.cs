using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tries to shows given tutorial when player enters area
/// </summary>
public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private TutorialState tutorialState;
    [SerializeField]
    private Vector2 size;
    // It can activate any object as sideeffect to make tutorials more flexible
    [SerializeField]
    private GameObject sideEffects;

    private TutorialManager tutorialManager;

    private int triggered;

    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        if (sideEffects)
            sideEffects.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, size * 2);
    }

    private void Update()
    {
        if (triggered >= 2)
            return;
        // Show
        if (triggered == 0 && IsInside() && !tutorialManager.AlreadyUsed(tutorialState))
        {
            if (sideEffects)
                sideEffects.SetActive(true);
            tutorialManager.Show(tutorialState);
            ++triggered;
            return;
        }
        // Hide
        if (triggered == 1 && !IsInside())
        {
            if (sideEffects)
                sideEffects.SetActive(false);
            tutorialManager.Hide();
            ++triggered;
        }
    }

    private bool IsInside()
    {
        Vector2 pos = Character.instance.transform.position;
        return Mathf.Abs(pos.x - transform.position.x) < size.x && Mathf.Abs(pos.y - transform.position.y) < size.y;
    }
}
