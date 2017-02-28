using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFunctions : MonoBehaviour {

	public GameObject StartImage;
	public GameObject Menu;

	// Use this for initialization
	void Start () {
		
	}


	public void StartImageClick() {
		Destroy (StartImage); //Fade out Coroutine later
		Menu.gameObject.SetActive(true);
	}


	public void LoadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}


	public void Quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit ();
		#endif
	}

}
