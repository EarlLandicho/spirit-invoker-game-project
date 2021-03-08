﻿#region

using System.Collections;
using UnityEngine;

#endregion

public class MudGolemProjectileLaunch : MonoBehaviour
{
	[SerializeField] private GameObject projectile;
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackDelay;
	private Animator animator;
	private float attackRangeRadius;
	private float attackSpeedTemp;
	private bool canAttack;
	private bool isAttacking;
	private MovementFlip movementFlip;
	private MudGolem mudGolem;
	private Vector3 target;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		movementFlip = GetComponent<MovementFlip>();
		mudGolem = GetComponent<MudGolem>();
	}

	private void Start()
	{
		attackSpeedTemp = attackSpeed;
		attackRangeRadius = transform.GetChild(0).GetComponent<CircleCollider2D>().radius;
	}

	private void Update()
	{
		if (attackSpeedTemp > 0)
		{
			if (!canAttack && !isAttacking)
			{
				attackSpeedTemp -= Time.deltaTime;
			}
		}
		else
		{
			canAttack = true;
			attackSpeedTemp = attackSpeed;
		}
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && canAttack)
		{
			AttackRandomEnemyInRange();
		}
	}

	// Called in Animator
	public void ThrowBoulderProjectile()
	{
		var projectileObject = Instantiate(projectile, transform.position, transform.rotation);
		projectileObject.GetComponent<MudGolemProjectile>().SetTarget(target);
		projectileObject.GetComponent<MudGolemProjectile>().SetMoxGolemRange(attackRangeRadius);
		mudGolem.SetCanMove(true);
		isAttacking = false;
	}

	private void AttackRandomEnemyInRange()
	{
		var enemies =
			Physics2D.OverlapCircleAll(transform.position, attackRangeRadius, 1 << LayerMask.NameToLayer("Enemy"));
		if (enemies.Length > 0)
		{
			var ranNum = Random.Range(0, enemies.Length);
			target = enemies[ranNum].gameObject.transform.position;
			StartCoroutine("DelayAttack");
		}
	}

	private IEnumerator DelayAttack()
	{
		canAttack = false;
		isAttacking = true;
		mudGolem.SetCanMove(false);
		yield return new WaitForSeconds(attackDelay);
		FaceCorrectDirection();
		animator.SetTrigger("attack");
	}

	private void FaceCorrectDirection()
	{
		if (target.x < transform.position.x)
		{
			movementFlip.FaceLeft();
		}
		else
		{
			movementFlip.FaceRight();
		}
	}
}