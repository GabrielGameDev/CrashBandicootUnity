using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

	
	GameObject fruitPosObj;
	bool isMoving;
	Vector3 startMarker;	
	public float speed = 5;
	float startTime;
	float journeyLength;
	AudioSource audioSource;
	bool collected;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();

	}

	private void Update()
	{
		if (isMoving)
		{
			float distCovered = (Time.time - startTime) * speed;

			
			float fractionOfJourney = distCovered / journeyLength;			
			transform.position = Vector3.Lerp(startMarker, fruitPosObj.transform.position, fractionOfJourney);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Collect(other);
	}

	private void Collect(Collider other)
	{
		if (collected)
			return;

		if (other.gameObject.CompareTag("Player"))
		{
			collected = true;
			fruitPosObj = GameObject.FindGameObjectWithTag("FruitPos");
			audioSource.Play();
			startMarker = transform.position;
			startTime = Time.time;
			journeyLength = Vector3.Distance(startMarker, fruitPosObj.transform.position);
			isMoving = true;
			Destroy(gameObject, 1);
			LevelManager.instance.UpdateFruits();
			Collider[] colliders = GetComponents<Collider>();
			for (int i = 0; i < colliders.Length; i++)
			{
				colliders[i].enabled = false;
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Collect(collision.collider);
	}
}
