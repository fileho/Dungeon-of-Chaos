using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (AttackManager))]
public class Enemy : Unit {
	private const float CHASE_HEAT = 3f;
	private const float ATTACK_HEAT = 3f;
	private const float RAYCAST_TIME_INTERVAL = 1f;

	private enum State {
		Idle,
		Chase,
		Attack,
	}

	[SerializeField]
	private ILoot loot;
	public LootModifiers lootModifiers;
	private AttackManager attackManager;
	private IAttack currentAttack;
	private State state;
	private Animator animator;
	private Rigidbody2D rb;

	[Header ("Enemy Ambient")]
	[SerializeField]
	private float maxDistance;
	[SerializeField]
	private SoundSettings ambientSFX;
	private float minFrequency = 3.5f;
	private float maxFrequency = 6.5f;
	private float frequency = 0f;
	private float time = 0f;



	private void OnEnable ()
	{
		unitHit += OnEnemyHit;
	}

	private void OnDisable ()
	{
		unitHit -= OnEnemyHit;
	}

	private void OnEnemyHit ()
	{
		lastAttackTime = Time.time;
	}

	protected override void Init ()
	{
		loot = Instantiate (loot).Init (this);
		lootModifiers = Instantiate (lootModifiers);
		Target = Character.instance;
		state = State.Idle;
		attackManager = GetComponent<AttackManager> ();
		animator = GetComponent<Animator> ();
		rb = transform.GetComponent<Rigidbody2D> ();
		minFrequency = SoundManager.instance.GetSoundLenght(ambientSFX) + 1;
		maxFrequency = minFrequency * 3;
	}

	private float lastLosTime = Mathf.NegativeInfinity;
	private float lastAttackTime = Mathf.NegativeInfinity;
	private float lastRayCastCheck = 0f;

	private bool IsTargetInChaseRange ()
	{
		Vector2 direction = GetTargetDirection ();

		bool playerHit = false;

		if (Time.time - lastRayCastCheck > RAYCAST_TIME_INTERVAL) {
			for (int i = 0; i < 10; i++) {
				Vector2 dir = Quaternion.AngleAxis ((-5 + (1 * i)), Vector3.forward) * direction;
				RaycastHit2D hit =
					Physics2D.Linecast (transform.position, (Vector2)transform.position + (dir * stats.ChaseDistance ()),
									   ~(1 << LayerMask.NameToLayer ("Enemy")));

				if (hit.collider && hit.collider.CompareTag ("Player")) {
					playerHit = true;
					lastLosTime = Time.time;
					break;
				}
			}
			lastRayCastCheck = Time.time;
		}
		return playerHit || (Time.time - lastLosTime < CHASE_HEAT) || (Time.time - lastAttackTime < ATTACK_HEAT);
	}

	private void FixedUpdate ()
	{
		animator.SetBool ("isMoving", rb.velocity.magnitude > 0.01f);
		SwitchEnemyStates ();
		if (ShouldPlaySound ())
			PlayAmbientSound ();
	}

	private bool IsAttacking ()
	{
		return currentAttack != null && currentAttack.IsAttacking ();
	}

	private void RotateWeapon ()
	{
		if (weapon != null) {
			weapon.RotateWeapon (GetTargetPosition ());
		}
	}

	private void ResetWeapon ()
	{
		if (weapon != null) {
			weapon.ResetWeapon ();
		}
	}

	private void FlipSprite ()
	{
		Vector2 dir = GetTargetDirection ();

		if (dir.x > 0.01f)
			transform.localScale = new Vector3 (-1, 1, 1);
		else if (dir.x < -0.01f)
			transform.localScale = Vector3.one;
	}

	private bool Attack ()
	{
		if (!IsTargetInChaseRange () || IsAttacking ())
			return true;
		currentAttack = attackManager.GetBestAvailableAttack ();
		FlipSprite ();
		RotateWeapon ();
		if (currentAttack != null) {
			state = State.Attack;
			currentAttack.Attack ();
			return true;
		}
		return false;
	}

	private void Move ()
	{
		movement.Move ();
	}

	private bool Chase ()
	{
		if (IsTargetInChaseRange ()) {
			state = State.Chase;

			if (GetTargetDistance () >= attackManager.GetMinimumAttackRange ()) {
				Move ();
			}
			FlipSprite ();
			RotateWeapon ();
			return true;
		}
		return false;
	}

	private void Idle ()
	{
		state = State.Idle;
		ResetWeapon ();
	}

	private void SwitchEnemyStates ()
	{
		if (dead)
			return;
		if (Attack ()) {
		} else if (Chase ()) {
		} else {
			Idle ();
		}
	}

	public bool IsAwake ()
	{
		return state == State.Attack || state == State.Chase;
	}

	protected override void CleanUp ()
	{
		loot.Drop ();
		Destroy (transform.parent.gameObject);
	}

	private void PlayAmbientSound ()
	{
		time = 0f;
		frequency = Random.Range (minFrequency, maxFrequency);

		float distance = Vector2.Distance (Character.instance.transform.position, transform.position);
		if (distance > maxDistance)
			return;

		ambientSFX.SetVolumeFromDistance (distance, maxDistance);
		SoundManager.instance.PlaySound (ambientSFX);
	}

	private bool ShouldPlaySound ()
	{
		time += Time.deltaTime;
		return time >= frequency;
	}

	protected override void Die ()
	{
		if (currentAttack != null)
			currentAttack.StopAttackIfDead ();
		base.Die ();
	}
}
