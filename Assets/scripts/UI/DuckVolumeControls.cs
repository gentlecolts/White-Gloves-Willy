using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DuckVolumeControls : MonoBehaviour {

	public AudioMixer mixer;
	private float sfxvol;
	private bool ischecking;

	void Start() {
		ischecking = true;
		StartCoroutine (CheckDuckLvl ());
	}

	IEnumerator CheckDuckLvl(){
		while (ischecking) {
			mixer.GetFloat ("SFXVol", out sfxvol);
			float newsendlvl = -40f + (sfxvol / 2f);
			mixer.SetFloat ("DuckSendLvl", newsendlvl); 
			yield return new WaitForSeconds (1f);
		}
	}
}
