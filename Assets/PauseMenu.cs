using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject pausemenu;

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Cancel")) {
			ButtonClicked ();
		}
	}

	public void ButtonClicked() {
		pausemenu.SetActive (true);
		Time.timeScale = 0;
	}

	public void ContinueButton() {
		pausemenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartMenuButtons(string newScene) {
		Time.timeScale = 1;
		SceneManager.LoadScene (newScene);
	}
}
