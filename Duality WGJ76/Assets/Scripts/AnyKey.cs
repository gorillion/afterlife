using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnyKey : MonoBehaviour {

	public GameManager gameManager;

	public bool destroyedGameManager;

	private float graceTimer;
	private bool canPressAnyKey;

    void Start() {
		gameManager = FindObjectOfType<GameManager> ();

	}

    void Update() {

		graceTimer += Time.deltaTime;

		if (graceTimer >= 1f)
			canPressAnyKey = true;

		if (canPressAnyKey) {
			if (Input.anyKeyDown) {
				SceneManager.LoadScene ("Menu");
			}
		}

		if (gameManager) {
			Destroy (FindObjectOfType<GameManager> ().gameObject);
		}

    }
}
