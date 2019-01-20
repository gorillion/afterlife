using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform currentPlayerObject;

	public Vector3[] cameraBounds;

	void Start() {
        
    }

    void Update() {

		if (!currentPlayerObject) {
			currentPlayerObject = FindObjectOfType<PlayerController> ().transform;
		}

		transform.position = new Vector3 (Mathf.Clamp (currentPlayerObject.transform.position.x, cameraBounds[0].x, cameraBounds[1].x), 
			Mathf.Clamp (currentPlayerObject.transform.position.y, cameraBounds[0].y, cameraBounds[1].y), -10f);
    }

	public void UpdateCurrentPlayerObject (GameObject _currentPlayerObject) {
		currentPlayerObject = _currentPlayerObject.transform;
	}

	public void UpdateCameraBounds (Vector2 minBounds, Vector2 maxBounds) {
		cameraBounds[0] = new Vector3 (minBounds.x, minBounds.y, -10f);
		cameraBounds[1] = new Vector3 (maxBounds.x, maxBounds.y, -10f);
	}


}
