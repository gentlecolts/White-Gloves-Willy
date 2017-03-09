using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceCheer : MonoBehaviour {

	public AudioSource Audience_Mad;
	public AudioSource Audience_Ok;
	public AudioSource Audience_Happy;

	private float happyLvl;

	public void UpdateHappiness(float happinessLvl) {
		happyLvl = happinessLvl;
	}

	public void AudienceNoise() {
		if (happyLvl < 0.33f) {
			Audience_Mad.Play ();
		} else if (happyLvl > 0.66f) {
			Audience_Happy.Play ();
		} else {
			Audience_Ok.Play ();
		}
	}
}
