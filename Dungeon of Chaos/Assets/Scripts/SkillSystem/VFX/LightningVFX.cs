using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningVFX : VisualEffects
{
    private Vector3 target;
    private float duration;

    [SerializeField] private SoundSettings lightningSFX;

    public override void Init(float duration, Unit source, List<ISkillEffect> effects)
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.duration = duration;
        base.Init(duration, source, effects);
        StartCoroutine(ExecuteVFX());
    }

    private IEnumerator ExecuteVFX()
    {
        float time = 0f;
        Vector2 dir = (Vector2)(target - source.gameObject.transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        Debug.Log(angle);
        if (angle > 0 && angle < 180)
            angle = (360 - angle);
        transform.Rotate(0, 0, angle);
        Debug.Log(transform.rotation.eulerAngles);
        while (time < duration)
        {
            SoundManager.instance.PlaySound(lightningSFX);
            time += Time.deltaTime;
            float t = time / duration;
            GetComponent<SpriteRenderer>().material.SetFloat("_ClipUvDown", 1 - t);
            yield return null;
        }
    }
}
