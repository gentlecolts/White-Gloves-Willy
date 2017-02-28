using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

	public GameObject countdown;
	public GameObject canvas;
	public bool countdownActive;


	void Start () {
		if (countdownActive) {
			StartCountDown ();
		}
	}
	
	void Update () {
		
	}

	void StartCountDown() {
		GameObject counter = Instantiate (countdown);
		counter.transform.SetParent (canvas.transform, false);
	}
}
