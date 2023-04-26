using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that defines the heal attack execution
/// </summary>
public class HealAttack : IAttack {
	protected float healAmount = 2;
	protected float healRadius = 10;

	protected override void ApplyConfigurations ()
	{
		base.ApplyConfigurations ();
		HealAttackConfiguration _attackConfiguration = attackConfiguration as HealAttackConfiguration;
		healAmount = _attackConfiguration.healAmount;
		healRadius = _attackConfiguration.healRadius;
	}

	protected override IEnumerator StartAttackAnimation ()
	{
		IIndicator indicator = CreateIndicator (transform);
		if (indicator) {
			indicator.transform.localPosition = Vector3.zero;
			indicator.Use ();
			yield return null;
		}

		Heal ();
		yield return new WaitForSeconds (indicator.Duration);
		isAttacking = false;
	}

	private void Heal ()
	{
		Collider2D [] colliders =
			Physics2D.OverlapCircleAll (owner.transform.position, healRadius, 1 << LayerMask.NameToLayer ("Enemy"));
		if (colliders.Length > 0) {
			for (int i = 0; i < colliders.Length; i++) {
				if (colliders [i].GetComponent<Unit> ()) {
					// Increase health
					colliders [i].GetComponent<Unit> ().stats.RegenerateHealth (healAmount);
				}
			}
		}
	}
}
