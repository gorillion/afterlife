using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDatabase : MonoBehaviour {
    
	public List<Level> database;

    void Start() {
		database = new List<Level> ();

		database.Add (new Level(new Vector2 (-6f, -4f), new Vector2(4.67f, 3.46f)));

    }

    void Update() {
        
    }
}
