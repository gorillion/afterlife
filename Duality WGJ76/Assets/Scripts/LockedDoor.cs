using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour {

	private GameManager gameManager;
	private SoundPlayer soundPlayer;

	private Vector2 doorUnlockPos;
	private Vector2 doorLockedPos;
	private float positionLerp;
	private bool unlocked;
	private bool opened;

    void Start() {
		gameManager = FindObjectOfType<GameManager> ();
		soundPlayer = GetComponent<SoundPlayer> ();
		doorLockedPos = transform.position;
		doorUnlockPos = new Vector2 (transform.position.x, transform.position.y + -1.05f);
    }

	void Update () {

		if (unlocked) {
			if (!opened) {
				positionLerp += Time.deltaTime;
				transform.position = Vector3.Lerp (doorLockedPos, doorUnlockPos, Mathf.SmoothStep (0f, 1f, positionLerp));

				if (positionLerp >= 1f) {
					opened = true;
				}
			}

		}

	}

	public void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag == "Player") {
			if (col.gameObject.GetComponent<PlayerController> ().playerType == PlayerController.PlayerTypes.Human) {
				if (gameManager.playerHasKey) {
					unlocked = true;
					gameManager.playerHasKey = false;
					soundPlayer.PlaySound ();
				}
			}
		}

	}

}
