using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningVFX : VisualEffects
{
    private Vector3 target;
    private float duration;

    [SerializeField] private SoundSettings lightningSFX;
    [SerializeField] private GameObject lightningSparks;

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
        float sparkTime = 0.1f;
        Vector2 dir = (Vector2)(target - source.gameObject.transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        if (angle > 0 && angle < 180)
            angle = (360 - angle);
        transform.Rotate(0, 0, angle);
        while (time < duration)
        {
            SoundManager.instance.PlaySound(lightningSFX);
            sparkTime -= Time.deltaTime;
            if (sparkTime <= 0f)
            {
                float scale = Random.Range(0.5f, 5*(time/duration));
                Vector3 pos = new Vector3(this.transform.position.x + dir.x*scale, transform.position.y + dir.y*scale, 0);
                Instantiate(lightningSparks, pos, Quaternion.identity, this.transform);
                sparkTime = 0.1f;
            }

            time += Time.deltaTime;
            float t = time / duration;
            GetComponent<SpriteRenderer>().material.SetFloat("_ClipUvDown", 1 - t);
            yield return null;
        }
    }
}
