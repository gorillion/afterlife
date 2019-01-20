using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	public AudioClip[] audioClips;

	private AudioSource audioSource;
	[SerializeField]
	private SoundPlayer footStepsAudioSource;
	[SerializeField]
	private SoundPlayer jumpAudioSource;
	[SerializeField]
	private SoundPlayer impactAudioSource;

    void Start() {
		audioSource = GetComponent<AudioSource> ();
    }

    //void Update() {
        
    //}

	public void PlaySound () {

		int rand = Random.Range (0, audioClips.Length);
		audioSource.PlayOneShot (audioClips[rand]);

	}

	public void PlayFootSteps () {
		footStepsAudioSource.PlaySound ();
	}

	public void PlayJumpNoise () {
		jumpAudioSource.PlaySound ();
	}

	public void PlayImpactNoise () {
		impactAudioSource.PlaySound ();
	}

}
