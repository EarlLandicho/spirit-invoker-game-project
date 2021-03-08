﻿#region

using UnityEngine;

#endregion

public class LightningCloudSpawn : MonoBehaviour
{
	[SerializeField] private float damage;
	[SerializeField] private Vector2 centerOffSet = new Vector2(0, 0);
	[SerializeField] private Vector2 size = new Vector2(0, 0);
	[SerializeField] private LayerMask layerMask;

	private void Start()
	{
		var yOffSet = .40f;
		var hit = Physics2D.Raycast(transform.position, Vector2.down, 500f, layerMask);
		transform.position = hit.point + new Vector2(0, yOffSet);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube((Vector2) transform.position + centerOffSet, size);
	}

	// Called in Animator
	public void DealDamage()
	{
		var enemies = Physics2D.OverlapBoxAll((Vector2) transform.position + centerOffSet, size, 0,
											  1 << LayerMask.NameToLayer("Enemy"));
		foreach (var enemy in enemies)
		{
			enemy.gameObject.GetComponent<IHealth>().TakeDamage(damage);
			enemy.gameObject.GetComponent<StatusEffect>().BecomeStunned();
		}
	}
}