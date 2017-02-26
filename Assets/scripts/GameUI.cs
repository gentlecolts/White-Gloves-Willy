using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

	public GameObject countdown;
	public GameObject canvas;

	// Use this for initialization
	void Start () {
		StartCountDown ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartCountDown() {
		GameObject counter = Instantiate (countdown);
		counter.transform.SetParent (canvas.transform, false);
	}
}
