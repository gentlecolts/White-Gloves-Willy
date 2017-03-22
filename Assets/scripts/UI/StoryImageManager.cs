using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryImageManager : MonoBehaviour {

	public float readTime;
	public Image image1;
	public Image image2;
	public Image image3;

	private Text curr_text;
	private MenuFunctions quitScript;


	void Start() {
		curr_text = image1.GetComponentInChildren<Text>();
		quitScript = GetComponent<MenuFunctions> ();

		StartCoroutine (PlayCutscenes ());
	}


	void Update() {
		if (Input.anyKeyDown) {
			quitScript.LoadByIndex ("UIStructure");
		}
	}


	IEnumerator PlayCutscenes() {
		yield return new WaitForSeconds (readTime);

		StartCoroutine (Fade (image1, curr_text));
		yield return new WaitForSeconds (readTime + 0.5f);

		curr_text = image2.GetComponentInChildren<Text> ();
		StartCoroutine (Fade (image2, curr_text));
		yield return new WaitForSeconds (readTime + 0.5f);

		curr_text = image3.GetComponentInChildren<Text> ();
		StartCoroutine (Fade (image3, curr_text));
	}

	IEnumerator Fade(Image image, Text text) {
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = image.color;
			Color t = text.color;
			c.a = f;
			t.a = f;
			image.color = c;
			text.color = t;
			yield return new WaitForSeconds(0.05f);
		}
		image.gameObject.SetActive (false);
	}

}
