using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFunctions : MonoBehaviour {


	public void LoadByIndex(string newScene)
	{
		SceneManager.LoadScene (newScene);
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
