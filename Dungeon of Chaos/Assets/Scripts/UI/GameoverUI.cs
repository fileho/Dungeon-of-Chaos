using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameoverUI : MonoBehaviour
{
    private Image background;
    private TMP_Text gameover;
    private TMP_Text tooltip;

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();

        gameover = transform.GetChild(0).GetComponent<TMP_Text>();
        tooltip = transform.GetChild(1).GetComponent<TMP_Text>();

        gameover.gameObject.SetActive(false);
        tooltip.gameObject.SetActive(false);

        HideCanvas();
    }

    public void ShowGameover()
    {
        StartCoroutine(Gameover());
    }

    private IEnumerator Gameover()
    {
        StartCoroutine(CanvasAnimation(2f, f => f));

        yield return new WaitForSeconds(0.6f);
        gameover.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        tooltip.gameObject.SetActive(true);
    }

    private void HideCanvas()
    {
        StartCoroutine(CanvasAnimation(0.8f, f => 1 - f));
    }

    private IEnumerator CanvasAnimation(float duration, Func<float, float> func)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            background.color = new Color(0, 0, 0, func(t));
            yield return null;
        }
    }
}
