using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	public float invincibleTime = 20;
	Animator animator;
	public int randomDeaths = 3;
	Rigidbody rb;
	AudioSource audioSource;

	public AudioClip deathSound, specialSound;
	public bool invincible;
	public GameObject akuAkuMask;

	public GameObject specialAttack;


	private void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	public void TakeDamage()
	{
		
		int random = Random.Range(0, randomDeaths);
		Debug.Log(random);
		animator.SetFloat("random", random);
		animator.SetTrigger("death");
		rb.isKinematic = true;
		rb.detectCollisions = false;
		GetComponent<PlayerMovement>().enabled = false;
		audioSource.PlayOneShot(deathSound);
		Invoke("ReloadScene", 3f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Special"))
		{
			StartCoroutine(GettingSpecial());
			Destroy(other.gameObject);
		}
	}

	IEnumerator GettingSpecial()
	{
		LevelManager.instance.PlaySound(LevelManager.instance.specialSound);
		specialAttack.SetActive(true);
		invincible = true;
		akuAkuMask.SetActive(true);
		yield return new WaitForSeconds(invincibleTime);
		specialAttack.SetActive(false);
		invincible = false;
		akuAkuMask.SetActive(false);
		LevelManager.instance.PlaySound(LevelManager.instance.levelSong);
	}

	void ReloadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
