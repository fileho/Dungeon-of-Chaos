using System.Collections;
using UnityEngine;

/// <summary>
/// Specifies the tweening of indicators
/// </summary>
public abstract class IIndicator : MonoBehaviour {
	public float Duration { get; protected set; }
	protected SpriteRenderer sprite;

	public void Init (IndicatorConfiguration indicatorConfiguration)
	{
		InitSprites ();
		ApplyConfigurations (indicatorConfiguration);
	}

	protected virtual void InitSprites ()
	{
		sprite = transform.Find ("Primary").GetComponent<SpriteRenderer> ();
	}

	protected virtual void ApplyConfigurations (IndicatorConfiguration indicatorConfiguration)
	{
		Duration = indicatorConfiguration.duration;
	}

	public virtual void Use ()
	{
		StartCoroutine (ShowIndicator ());
	}

	/// <summary>
	/// Abstract Coroutine that controls the way the Indicator appears before the attack
	/// </summary>
	/// <returns></returns>
	protected abstract IEnumerator ShowIndicator ();

	protected virtual void CleanUp ()
	{
		Destroy (gameObject);
	}
}
