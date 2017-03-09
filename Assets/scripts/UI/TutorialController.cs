using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {
	public Text textTut;

	private string[] tutWords;
	private int currentText;

	// Use this for initialization
	void Start () {
		currentText = 0;
		tutWords = new string[10];

		tutWords [0] = "Help Willie the Magician save his disastrous magic show!" +
			" Use WASD/Arrow Keys to move around the stage. Space is jump.";
		tutWords [1] = "As a magician, you have abilities. Use Left Mouse to sent out a" +
		" sparkle that will swap positions between you and another object.";
		textTut.text = tutWords [currentText];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void nextIsClicked(){
		currentText += 1;
		textTut.text = tutWords [currentText];
	}

}
