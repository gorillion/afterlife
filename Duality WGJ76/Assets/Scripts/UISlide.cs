using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlide : MonoBehaviour {

	public GameObject panelToHide;

    void Start() {
        
    }

	void Update () {

	}

	public void HidePanel () {
		panelToHide.SetActive (false);
	}

	public void ShowPanel () {
		panelToHide.SetActive (true);
	}

}
