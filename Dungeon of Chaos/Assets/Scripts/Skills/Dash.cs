using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dash/Dash")]
public class Dash : ScriptableObject
{
    [SerializeField] protected float dashSpeed;
    [SerializeField] protected float staminaCost;

    protected bool dashing = false;
    protected bool stopDash = false;

    protected Rigidbody2D rb;
    protected Character character;
    protected TrailRenderer trail;

    public bool IsDashing()
    {
        return dashing;
    }

    public float Cost()
    {
        return staminaCost;
    }

    public void StartDash(Vector2 dir)
    {
        Init();
        character.StartCoroutine(DashAnimation(dir));
    }

    protected virtual void Init()
    {
        character = Character.instance;
        rb = character.GetComponent<Rigidbody2D>();
        trail = character.transform.Find("Trail").GetComponent<TrailRenderer>();
    }

    private IEnumerator DashAnimation(Vector2 dir)
    {
        trail.enabled = true;
        dashing = true;
        stopDash = false;

        rb.AddForce(dashSpeed * 100 * dir);
        rb.drag = 1;

        float duration = .4f;

        float t = 0;
        while (t < duration && !stopDash)
        {
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        rb.drag = 10;
        dashing = false;
        trail.enabled = false;
    }

    public void ResetDash()
    {
        dashing = false;
        stopDash = false;
    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (!dashing)
            return;

        stopDash = true;
    }
}
