using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeChange : MonoBehaviour {

	public AudioMixer mixer;

	public Slider master;
	public Slider music;
	public Slider sfx;

	static float masterVolume=0;
	static float musicVolume=0;
	static float SFXVolume=0;

	void Start() {
		master.value = masterVolume;
		music.value = musicVolume;
		sfx.value = SFXVolume;
	}

	public void SetMasterVol (float masterVolLvl) {
		masterVolume = masterVolLvl;
		mixer.SetFloat ("MasterVol", masterVolLvl);
	}

	public void SetMusicVol (float musicVolLvl) {
		musicVolume = musicVolLvl;
		mixer.SetFloat ("MusicVol", musicVolLvl);
	}

	public void SetSFXVol (float SFXVolLvl) {
		SFXVolume = SFXVolLvl;
		mixer.SetFloat ("SFXVol", SFXVolLvl);
	}

	public void reset(Slider slider){
		slider.value = 0;
	}
}
