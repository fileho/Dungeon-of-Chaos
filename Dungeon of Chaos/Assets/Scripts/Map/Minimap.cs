using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.Rendering.Universal;

public class Minimap : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject minimap;
    private GameObject globalLight;

    void Start()
    {
        mainCamera = Camera.main;
        minimap = transform.GetChild(0).gameObject;
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
    }

    private void ToggleMinimap()
    {
        if (minimap.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        minimap.SetActive(true);
        mainCamera.gameObject.SetActive(false);
        globalLight.SetActive(false);
    }

    private void Hide()
    {
        mainCamera.gameObject.SetActive(true);
        minimap.SetActive(false);
        globalLight.SetActive(true);
    }
}
