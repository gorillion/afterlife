using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip ambience;

	public static AudioManager Instance;

	void Awake () {
		if (Instance != null && Instance != this) {
			Destroy (this.gameObject);
		}
		else {
			Instance = this;
		}
	}

	void Start() {
		//audioSource.Play
		DontDestroyOnLoad (gameObject);
    }

    void Update() {
        
    }
}
