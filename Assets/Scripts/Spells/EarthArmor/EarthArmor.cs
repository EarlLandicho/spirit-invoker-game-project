﻿#region

using System.Collections;
using UnityEngine;

#endregion

public class EarthArmor : MonoBehaviour
{
	[Range(0, 1)] [SerializeField] private float percentDamageDecrease;
	[SerializeField] private float duration;
	[SerializeField] private GameObject armorShield;
	private StatusEffect playerStatusEffect;

	private void Awake()
	{
		playerStatusEffect = GameObject.Find("Player").GetComponent<StatusEffect>();
	}

	private void Start()
	{
		playerStatusEffect.BecomeArmored(percentDamageDecrease, duration);
		//Instantiate(armorShield, transform.position, transform.rotation);
		StartCoroutine("ArmorDuration");
	}

	private IEnumerator ArmorDuration()
	{
		yield return new WaitForSeconds(duration);
		Destroy(gameObject);
	}
}