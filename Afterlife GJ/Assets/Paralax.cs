using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {

	public Transform playerPos;

    void Start() {
        
    }

    void Update() {
		transform.position = new Vector2 (playerPos.position.x / 2f, playerPos.position.y / 2f);
    }
}
