using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour {

	public Image[] slides;
	public float fillTime;
	public float fillIncrement;

	void Start () {
		StartCoroutine (CountDown ());
	}
	
	public IEnumerator CountDown() {
		foreach (Image slide in slides) {
			StartCoroutine(FillIncrease (slide));
			yield return new WaitUntil (() => slide.fillAmount >= 1f);
			Destroy (slide.transform.parent.gameObject);
		}
		Destroy (gameObject);
	}

	IEnumerator FillIncrease(Image fillImage) {
		while (fillImage.fillAmount < 1f) {
			yield return new WaitForSeconds (fillTime);
			fillImage.fillAmount += fillIncrement;
		}
	}
}
