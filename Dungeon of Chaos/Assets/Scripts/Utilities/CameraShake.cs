using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    // Amplitude of the shake. A larger value shakes the camera harder
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private float shakeDuration = 0f;
    private Vector3 originalPos;
    private float originalShakeAmount;

    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        originalShakeAmount = shakeAmount;
    }

    private void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            gameObject.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
            shakeAmount -= Time.deltaTime * decreaseFactor;
            if (shakeAmount <= 0) shakeAmount = 0;
        }
        else
        {
            shakeDuration = 0f;
            gameObject.transform.localPosition = originalPos;
        }
    }

    public void ShakeForDuration(float duration, float amount = 0.25f)
    {
        shakeAmount = amount;
        shakeDuration = duration;
    }
}