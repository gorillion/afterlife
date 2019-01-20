using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	public GameObject particles;

	private GameManager gameManager;
	private SoundPlayer soundPlayer;
	private SpriteRenderer sr;

	private bool pickedUp;

    void Start() {
		gameManager = FindObjectOfType<GameManager> ();
		soundPlayer = GetComponent<SoundPlayer> ();
		sr = GetComponent<SpriteRenderer> ();
	}

	public void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.tag == "Player") {
			if (!pickedUp) {
				if (!gameManager.playerHasKey) {
					if (col.GetComponent<PlayerController> ().playerType == PlayerController.PlayerTypes.Human) {
						gameManager.KeyPickedUp ();
						Instantiate (particles, transform.position + new Vector3 (0f, 0.25f, 0f), Quaternion.identity);
						soundPlayer.PlaySound ();
						pickedUp = true;
						sr.color = new Color (0f, 0f, 0f, 0f);
						//Destroy (gameObject);
					}
				}
			}
		}
	}

}
