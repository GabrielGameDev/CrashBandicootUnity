using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Damageable
{
    public ParticleSystem hitParticle;
	public GameObject fruits;

    public void OnDeath()
	{
		Instantiate(hitParticle, transform.position, transform.rotation);
		Instantiate(fruits, transform.position, transform.rotation);
		gameObject.SetActive(false);
	}
}
