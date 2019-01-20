using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour {

	[SerializeField]
	private ButtonDoor attachedDoor;
	private Animator anim;
	private SoundPlayer soundPlayer;

	private bool activated;

	void Start() {
		anim = GetComponent<Animator> ();
		soundPlayer = GetComponent<SoundPlayer> ();

	}

    void Update() {
        
    }

	public void OnTriggerEnter2D (Collider2D col) {

		if (!activated) {
			if (col.gameObject.tag == "Player") {
				anim.SetBool ("Activated", true);
				attachedDoor.ButtonActivated ();
				activated = true;
				soundPlayer.PlaySound ();
			}
		}

	}

}
