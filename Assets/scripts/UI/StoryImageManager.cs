using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryImageManager : MonoBehaviour {

	private Image image;
	public Text text;

	void Start() {
		image = GetComponent<Image> ();
	}

	public void OnClick() {
		StartCoroutine (Fade ());
		Destroy (gameObject, 1f);
	}

	IEnumerator Fade() {
		for (float f = 1f; f >= 0; f -= 0.1f) {
			Color c = image.color;
			Color t = text.color;
			c.a = f;
			t.a = f;
			image.color = c;
			text.color = t;
			yield return new WaitForSeconds(0.05f);
		}
	}

}
