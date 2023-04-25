using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Minimap
/// Works using second camera with different culling mask
/// </summary>
public class Minimap : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject minimap;
    private GameObject canvas;
    private GameObject globalLight;
    private GameObject gameUI;

    void Start()
    {
        mainCamera = Camera.main;
        minimap = transform.GetChild(0).gameObject;
        canvas = transform.GetChild(1).gameObject;
        gameUI = FindObjectOfType<InGameUIManager>().transform.parent.gameObject;
        var lights = FindObjectsOfType<Light2D>();

        globalLight = Array.Find(lights, light2D => light2D.lightType == Light2D.LightType.Global).gameObject;
        Assert.IsNotNull(globalLight, "globalLight != null");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && minimap.activeSelf)
        {
            // Escape can turn off the minimap
            ToggleMinimap();
        }
    }

    /// <summary>
    /// Turn the minimap on and off
    /// </summary>
    private void ToggleMinimap()
    {
        if (minimap.activeSelf)
        {
            Hide();
        }
        else
        {
            // Some overlap is already active
            if (Character.instance.IsInputBlocked())
                return;
            Show();
        }
    }

    private void Show()
    {
        minimap.SetActive(true);
        canvas.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        gameUI.SetActive(false);
        globalLight.SetActive(false);
        Character.instance.BlockInput();
    }

    private void Hide()
    {
        mainCamera.gameObject.SetActive(true);
        canvas.SetActive(false);
        minimap.SetActive(false);
        gameUI.SetActive(true);
        globalLight.SetActive(true);
        Character.instance.UnblockInput();
    }
}
