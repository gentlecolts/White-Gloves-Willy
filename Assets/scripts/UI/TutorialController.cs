using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
	public Text textTut;
	public Vector3 stageLeft;
	public Vector3 stageRight;
	public GameObject Barrel;
	public GameObject Fedora;
	public GameObject RightBarrelSpawner;

	private string[] tutWords;
	private int currentText;

	// Use this for initialization
	void Start () {
		currentText = 0;
		tutWords = new string[10];

		tutWords [0] = "Help Willie the Magician save his disastrous magic show!" +
			" Use WASD/Arrow Keys to move around the stage. Space is jump. Press Return to continue.";
		tutWords [1] = "As a magician, you have abilities. Use Left Mouse to sent out a" +
		" sparkle that will swap positions between you and another object.";
		tutWords[2] = "Hats are your enemies! Keep them far away from you with your teleport, and " +
			"your dash, which you can use with Right Mouse Button.";
		tutWords [3] = "You may have noticed that if you dash or teleport into enemies, you can stun them, " +
		"and they stop moving for a time.";
		tutWords [4] = "Willie doesn't want to get his white gloves dirty, so he must use other means of " +
		"getting rid of the hats permanently. Use barrels or other hats to destroy them.";
		textTut.text = tutWords [currentText];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Next"))
			nextIsClicked ();
	}

	void nextIsClicked(){
		currentText += 1;
		textTut.text = tutWords [currentText];
		spawnAppropriateItems ();
	}

	void spawnAppropriateItems(){
		switch (currentText) {
		case 3:
		case 1:
			Instantiate (Barrel, stageLeft, transform.rotation);
			break;
		case 4:
		case 2: 
			Instantiate (Fedora, stageRight, transform.rotation);
			break;
		}
	}

}
