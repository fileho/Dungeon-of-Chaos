using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class defining the execution of the Dash Attack
/// </summary>
public class DashAttack : MeleeAttack {
	protected override void PrepareWeapon ()
	{
		base.PrepareWeapon ();
		Weapon.EnableDisableCollider (true);
	}

	protected override void ResetWeapon ()
	{
		base.ResetWeapon ();
		Weapon.EnableDisableCollider (false);
	}

	// Ideal attack duration = 1
	protected override IEnumerator StartAttackAnimation ()
	{
		Vector2 ownerPos = owner.transform.position;
		Vector3 targetDirection = (GetTargetPosition () - ownerPos).normalized;

		IIndicator indicator = CreateIndicator ();
		if (indicator) {
			indicator.transform.localPosition = Vector3.zero;
			indicator.transform.up = targetDirection;
			indicator.Use ();
			yield return new WaitForSeconds (indicator.Duration);
		}
		PrepareWeapon ();

		float time = 0;
		int count = 0;
		while (time < 1) {
			++count;
			time += (Time.fixedDeltaTime / AttackAnimationDuration);
			ownerRB.MovePosition (owner.transform.position +
								 (targetDirection * (6f * range * Time.fixedDeltaTime / AttackAnimationDuration)));
			yield return null;
		}

		yield return new WaitForSeconds (0.3f);

		ResetWeapon ();
		isAttacking = false;
	}
}
