using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform currentPlayerObject;

	public Vector3[] cameraBounds;

	void Start() {
        
    }

    void Update() {

		transform.position = new Vector3 (Mathf.Clamp (currentPlayerObject.transform.position.x, cameraBounds[0].x, cameraBounds[1].x), 
			Mathf.Clamp (currentPlayerObject.transform.position.y, cameraBounds[0].y, cameraBounds[1].y), -10f);
    }

	public void UpdateCurrentPlayerObject (GameObject _currentPlayerObject) {
		currentPlayerObject = _currentPlayerObject.transform;
	}


}
