using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dash/Dash")]
public class Dash : ScriptableObject
{
    protected bool dashing = false;
    protected bool stopDash = false;

    protected Unit owner;
    protected List<ISkillEffect> effects;
    protected float dashSpeed;

    protected Rigidbody2D rb;    
    protected TrailRenderer trail;

    [SerializeField] protected SoundSettings dashSFX;

    public bool IsDashing()
    {
        return dashing;
    }

    public Dash Init(float speed, List<ISkillEffect> effects, Color color, Unit unit)
    {
        owner = unit;
        this.effects = effects;
        dashSpeed = speed;
        ResetDash();

        rb = owner.GetComponent<Rigidbody2D>();
        trail = owner.transform.Find("Trail").GetComponent<TrailRenderer>();
        trail.startColor = color;
        trail.endColor = color;

        return this;
    }

    public virtual void Use(Vector2 dir)
    {
        owner.StartCoroutine(DashAnimation(dir));
    }

    private IEnumerator DashAnimation(Vector2 dir)
    {
        trail.enabled = true;
        dashing = true;
        stopDash = false;

        SoundManager.instance.PlaySound(dashSFX);

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
        
        if (col.gameObject.CompareTag("Player"))
        {
            foreach (var e in effects)
            {
                e.Use(owner, new List<Unit>() { Character.instance });
            }
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            foreach (var e in effects)
            {
                e.Use(owner, new List<Unit>() { col.gameObject.GetComponent<Enemy>() });
            }
        }
    }
}
