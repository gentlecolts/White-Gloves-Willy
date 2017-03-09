using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeChange : MonoBehaviour {

	public AudioMixer mixer;

	public void SetMasterVol (float masterVolLvl) {
		mixer.SetFloat ("MasterVol", masterVolLvl);
	}

	public void SetMusicVol (float musicVolLvl) {
		mixer.SetFloat ("MusicVol", musicVolLvl);
	}

	public void SetSFXVol (float SFXVolLvl) {
		mixer.SetFloat ("SFXVol", SFXVolLvl);
	}

	public void reset(Slider slider){
		slider.value = 0;
	}
}
