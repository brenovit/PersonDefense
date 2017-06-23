using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

	public AudioClip uiSound;
	AudioSource audioSource;

	// Use this for initialization
	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void PlaySoundUI ()
	{
		audioSource.PlayOneShot (uiSound, 0.5f);
	}
}
