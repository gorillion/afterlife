using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public CameraFollow cameraFollow;
	public GameObject playerPrefab;

	public GameObject[] levels;

	public GameObject paralaxBG;

	public static GameManager Instance;

	public Image fadeImage;
	public Color[] fadeAlphas;

	public bool playerHasKey;

	public int currentLevel;
	public Vector2 newPlayerPos;
	public Vector2 newCameraBoundsMin;
	public Vector2 newCameraBoundsMax;

	private float levelEndTimer;
	private bool fadeOut;
	private bool fadeIn;

	void Awake () {
		if (Instance != null && Instance != this) {
			Destroy (this.gameObject);
		}
		else {
			Instance = this;
		}
	}

	void Start() {
		DontDestroyOnLoad (gameObject);
		paralaxBG = FindObjectOfType<Paralax> ().gameObject;
		fadeImage = FindObjectOfType<Fade> ().GetComponent<Image> ();
    }

    void Update() {
        
		if (fadeOut) {

			levelEndTimer += Time.deltaTime;
			fadeImage.color = Color.Lerp (fadeAlphas[0], fadeAlphas[1], Mathf.SmoothStep (0f, 1f, levelEndTimer));

			if (levelEndTimer >= 1f) {
				fadeOut = false;
				SpawnPlayerInNewLevel ();
				fadeIn = true;
				levelEndTimer = 0f;
				print ("fade out ended");
			}

		}

		if (fadeIn) {

			levelEndTimer += Time.deltaTime;
			fadeImage.color = Color.Lerp (fadeAlphas[1], fadeAlphas[0], Mathf.SmoothStep (0f, 1f, levelEndTimer));

			if (levelEndTimer >= 1f) {
				fadeIn = false;
				levelEndTimer = 0f;
				print ("fade in ended");
			}

		}

		if (Input.GetKeyDown (KeyCode.R)) {
			ResetLevel ();
		}

		if (!paralaxBG) {
			paralaxBG = FindObjectOfType<Paralax> ().gameObject;
			fadeImage = FindObjectOfType<Fade> ().GetComponent<Image> ();
			cameraFollow = FindObjectOfType<CameraFollow> ();
			cameraFollow.UpdateCameraBounds (newCameraBoundsMin, newCameraBoundsMax);
			SpawnPlayerInNewLevel ();
		}

    }

	public void KeyPickedUp () {
		playerHasKey = true;
	}

	public void ResetKey () {
		playerHasKey = false;
	}

	public void FadeOutLevel () {
		fadeOut = true;
	}

	public void ResetLevel () {

		SceneManager.LoadScene ("Game");

		fadeOut = false;
		fadeIn = false;
		levelEndTimer = 0f;
		playerHasKey = false;

		//SpawnPlayerInNewLevel ();

/*		Instantiate (levels[currentLevel]);

		PlayerController[] playerObjects = FindObjectsOfType<PlayerController> ();

		for (int i = 0; i < playerObjects.Length; i++) {
			Destroy (playerObjects[i].gameObject);
		}

		GameObject newPlayerObj = Instantiate (playerPrefab, newPlayerPos, Quaternion.identity) as GameObject;
		cameraFollow.UpdateCurrentPlayerObject (newPlayerObj);
		cameraFollow.UpdateCameraBounds (newCameraBoundsMin, newCameraBoundsMax);
		paralaxBG.transform.position = newPlayerPos;
		ResetKey ();

	*/
	}

	public void SpawnPlayerInNewLevel () {

		PlayerController[] playerObjects = FindObjectsOfType<PlayerController> ();

		for (int i = 0; i < playerObjects.Length; i++) {
			Destroy (playerObjects[i].gameObject);
		}

		GameObject newPlayerObj = Instantiate (playerPrefab, newPlayerPos, Quaternion.identity) as GameObject;
		cameraFollow.UpdateCurrentPlayerObject (newPlayerObj);
		cameraFollow.UpdateCameraBounds (newCameraBoundsMin, newCameraBoundsMax);
		paralaxBG.transform.position = newPlayerPos;
		ResetKey ();
	}

}
