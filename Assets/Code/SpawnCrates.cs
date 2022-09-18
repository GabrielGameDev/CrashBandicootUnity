using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCrates : MonoBehaviour
{

    public GameObject[] crates;
	public float spawnInterval = 0.1f;

	public void EnableCrates()
	{
		StartCoroutine(SpawningCrates());
	}

	IEnumerator SpawningCrates()
	{
		for (int i = 0; i < crates.Length; i++)
		{
			crates[i].SetActive(true);
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
