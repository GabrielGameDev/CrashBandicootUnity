using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class DamageEvent : UnityEvent<Transform>
{
}

public class Damageable : MonoBehaviour
{
	public DamageEvent OnDamage;
	public float pushForce = 10;
	public AudioClip hurtSound, throwSound;
	Rigidbody rb;
	AudioSource audioSource;


	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	public void TakeDamage(Transform player)
	{		
		if(audioSource != null && hurtSound != null)
		{
			audioSource.PlayOneShot(hurtSound);
		}
		OnDamage.Invoke(player);
	}

	public void Throw(Transform player)
	{
		if(rb != null && player != null)
		{
			if(audioSource != null && throwSound != null)
			{
				audioSource.PlayOneShot(throwSound);
			}
			Vector3 distance = transform.position - player.position;
			distance.Normalize();
			distance.y = 0;
			Debug.Log(distance);
			rb.AddForce(distance * pushForce, ForceMode.Impulse);
		}
	}

	public void Death(float destroyTime)
	{
		Destroy(gameObject, destroyTime);
	}
}
