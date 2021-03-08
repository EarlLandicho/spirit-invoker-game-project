﻿#region

using System.Collections;
using UnityEngine;

#endregion

public class HibernateDestroy : MonoBehaviour
{
	[SerializeField] private float duration;
	[SerializeField] private GameObject[] shards;
	[SerializeField] private int numberOfShards;
	[SerializeField] private float shardsLaunchForce;

	private void Awake()
	{
		StartCoroutine("HibernateDuration");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StopAllCoroutines();
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		for (var i = 0; i < numberOfShards; i++)
		{
			var randomShardNum = Random.Range(0, shards.Length);
			var randomForceAmount = Random.Range(shardsLaunchForce - 70, shardsLaunchForce + 70);
			Instantiate(shards[randomShardNum], transform.position, transform.rotation).GetComponent<Rigidbody2D>()
			   .AddForce((Quaternion.AngleAxis((Random.Range(0, 180)), Vector3.forward).normalized *
						  new Vector2(randomForceAmount, 0)));
		}
	}

	private IEnumerator HibernateDuration()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}

	public float GetDuration()
	{
		return duration;
	}
}