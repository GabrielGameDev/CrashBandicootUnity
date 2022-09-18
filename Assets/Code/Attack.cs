using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : MonoBehaviour
{
	public UnityEvent OnAttack;
	public bool aplyForce;
	private void OnTriggerEnter(Collider other)
	{
		Damageable damageable = other.GetComponent<Damageable>();
		if(damageable != null)
		{
			Transform attackTransform = transform;
			if (!aplyForce)
				attackTransform = null;

			damageable.TakeDamage(attackTransform);
			OnAttack.Invoke();
		}
	}
}
