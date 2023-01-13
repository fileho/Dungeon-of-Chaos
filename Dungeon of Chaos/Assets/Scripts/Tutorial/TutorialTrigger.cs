using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField]
    private TutorialState tutorialState;

    [SerializeField]
    private Vector2 size;

    private TutorialManager tutorialManager;

    private int triggered;

    private void Start()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
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

        if (triggered == 0 && IsInside())
        {
            tutorialManager.Show(tutorialState);
            ++triggered;
            return;
        }

        if (triggered == 1 && !IsInside())
        {
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
