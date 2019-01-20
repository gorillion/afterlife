using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {

	public Vector2 newPlayerStartPos;

	public int newLevelNumber;
	public Vector2 newCameraBoundsMin;
	public Vector2 newCameraBoundsMax;

	public bool finalExit;

	private PlayerController player;
	private GameManager gameManager;
	private CameraFollow cameraFollow;

	private bool fade;
	private bool hasFaded;
	private float fadeTimer;

	void Start () {
		//player = FindObjectOfType<PlayerController> ();
		gameManager = FindObjectOfType<GameManager> ();
		cameraFollow = FindObjectOfType<CameraFollow> ();
	}

	void Update () {

		if (!hasFaded) {
			if (fade) {

				fadeTimer += Time.deltaTime;

				if (!finalExit) {

					if (fadeTimer >= 1f) {
						gameManager.FadeOutLevel ();
						gameManager.newPlayerPos = newPlayerStartPos;
						gameManager.newCameraBoundsMin = newCameraBoundsMin;
						gameManager.newCameraBoundsMax = newCameraBoundsMax;
						gameManager.currentLevel = newLevelNumber;
						hasFaded = true;
					}

				} else {

					SceneManager.LoadScene ("End");

				}
			}
		}

	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			if (!fade) {
				//fade player out
				player = col.gameObject.GetComponent<PlayerController> ();
				player.FadeOutPlayer ();
				fade = true;
				print ("fadeTriggered");
				//after player fade, fade level out
				//place player in new level and fade in
			}
		}
	}

	void PlacePlayerInNewLevel () {
		cameraFollow.UpdateCameraBounds (newCameraBoundsMin, newCameraBoundsMax);
	}
}
