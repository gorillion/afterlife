using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour {

	private Vector2 doorUnlockPos;
	private Vector2 doorLockedPos;
	private float positionLerp;
	private bool unlocked;
	private bool opened;

	void Start () {
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

	public void ButtonActivated () {
		unlocked = true;
	}

}
