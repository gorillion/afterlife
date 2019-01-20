using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadFade : MonoBehaviour {

	public bool loadsLevel;
	public string levelToLoad;

	public Image fadeImage;
	public Color[] fadeAlphas;

	private float fadeLerp;
	private bool fadeIn;
	private bool fadeOut;

	void Start() {
		fadeIn = true;
    }

    void Update() {
        
		if (fadeIn) {
			fadeLerp += Time.deltaTime * 1.25f;
			fadeImage.color = Color.Lerp (fadeAlphas[0], fadeAlphas[1], Mathf.SmoothStep(0f, 1f, fadeLerp));

			if (fadeLerp >= 1f) {
				fadeIn = false;
				fadeLerp = 0f;
			}
		}

		if (fadeOut) {
			fadeLerp += Time.deltaTime * 1.25f;
			fadeImage.color = Color.Lerp (fadeAlphas[1], fadeAlphas[0], Mathf.SmoothStep (0f, 1f, fadeLerp));

			if (fadeLerp >= 1f) {
				fadeOut = false;
				SceneManager.LoadScene (levelToLoad);
			}
		}

    }

	public void StartFade () {
		fadeOut = true;
	}

}
