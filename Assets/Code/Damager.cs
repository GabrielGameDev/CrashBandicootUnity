using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		PlayerHealth player = other.GetComponent<PlayerHealth>();
		if (player != null)
		{
			player.TakeDamage();
		}
	}
}
