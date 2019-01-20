using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private SoundPlayer soundPlayer;

	void Start () {
		soundPlayer = GetComponent<SoundPlayer> ();
	}

	public void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerController> ().KillPlayer ();
			soundPlayer.PlaySound ();
		}

	}

}
