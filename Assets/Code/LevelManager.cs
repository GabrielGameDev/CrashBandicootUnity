using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public TextMeshProUGUI fruitsText;
	public Animator fruitsAnimator;
	public AudioSource music;
	public AudioClip levelSong, specialSound;
	int fruits;

	private void Awake()
	{
		instance = this;
	}

	public void UpdateFruits()
	{
		fruits++;
		fruitsText.text = fruits.ToString();
		fruitsAnimator.SetBool("show", true);
		CancelInvoke();
		Invoke("DisableFruitsText", 2);
	}

	void DisableFruitsText()
	{
		fruitsAnimator.SetBool("show", false);
	}

	public void PlaySound(AudioClip clip)
	{
		music.clip = clip;
		music.Play();
	}

}
