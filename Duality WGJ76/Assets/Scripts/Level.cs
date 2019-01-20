using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {

	public Vector2 levelStartPosition;
	public Vector2 cameraBounds;

	public Level (Vector2 _levelStartPos, Vector2 _cameraBounds) {
		levelStartPosition = _levelStartPos;
		cameraBounds = _cameraBounds;
	}

}
