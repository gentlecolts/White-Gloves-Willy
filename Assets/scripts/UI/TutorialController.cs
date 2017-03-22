using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {
	public Text textTut;
	public Vector3 stageLeft;
	public Vector3 stageRight;
	public GameObject Barrel;
	public GameObject Fedora;
	public GameObject RightBarrelSpawner;
	public GameObject LeftBarrelSpawner;
	public GameObject EnemySpawner;
	public GameObject Moonprop;

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
		tutWords [2] = "Hats are your enemies! Keep them far away from you with your teleport, and " +
			"your dash, which you can use with Right Mouse Button.";
		tutWords [3] = "You may have noticed that if you dash or teleport into enemies, you can stun them, " +
		"and they stop moving for a time.";
		tutWords [4] = "Willie doesn't want to get his white gloves dirty, so he must use other means of " +
		"getting rid of the hats permanently. Use barrels or other hats to destroy them.";
		tutWords [5] = "I think you're beginning to get the hang of this! Now, hats come in multiple " +
		"varieties, so watch out. Each poses a different threat.";
		tutWords [6] = "There are also different kinds of props. Moons will kill enemies on contact, " +
		"so they're very handy in a pinch.";
		tutWords [7] = "Finally, there's the audience. This is still a show, so keeping the audience entertained is your " +
		"primary objective! Other than survival, of course. The bar at the bottom displays audience happiness.";
		tutWords [8] = "Audiences like excitement! Killing an enemy will increase their happiness. But audiences easily" +
		" get bored, so their happiness steadily drains over time. Don't let them get too unhappy, or else it will be" +
		" The End.";
		tutWords [9] = "That's all, folks. Hitting Return one more time will take you back to the main menu.";
		textTut.text = tutWords [currentText];

		AudienceMeter.Instance.drainPerSec=0;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Next"))
			nextIsClicked ();
	}

	void nextIsClicked(){
		if (currentText < 9) {
			currentText += 1;
			textTut.text = tutWords [currentText];
			spawnAppropriateItems ();
		} else {
			SceneManager.LoadScene ("UIStructure");
		}
	}

	void spawnAppropriateItems(){
		switch (currentText) {
		case 3:
		case 4:
		case 1:
			Instantiate (Barrel, stageLeft, transform.rotation);
			break;
		case 2: 
			Instantiate (Fedora, stageRight, transform.rotation);
			break;
		case 5:
			RightBarrelSpawner.SetActive (true);
			LeftBarrelSpawner.SetActive (true);
			EnemySpawner.SetActive (true);
			break;
		case 6: 
			Moonprop.SetActive (true);
			break;
		case 7:
			AudienceMeter.Instance.drainPerSec = 1;
			break;
		}
	}

}
