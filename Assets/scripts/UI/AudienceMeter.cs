using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMeter : MonoBehaviour {
	const float happyMax=100;

	static AudienceMeter instance;
	public static AudienceMeter Instance {
		get {return instance; }
	}

	private float happyLevel;
	public float HappyLevel {
		get {return happyLevel; }
	}

	public Image audiencefill;
	public float delayUntilCheer;
	public AudioSource bigCheerNoise;
	public AudioSource smallCheerNoise;
	public AudioSource booNoise;

	[Range(0,happyMax)]
	public float startValue,unhappyLevel,smallCheerAmount,bigCheerAmount,drainPerSec;
	
	private bool isHappy=true, booing=false;

	void Start () {
		instance=this;
		happyLevel=startValue;
	}
	
	void Update () {
		happyLevel-=drainPerSec*Time.deltaTime;


		happyLevel=Mathf.Clamp(happyLevel,0,happyMax);
		audiencefill.fillAmount = happyLevel/happyMax;

		if (HappyLevel < unhappyLevel) {
			isHappy = false;
			if (!booing) {
				StartCoroutine (NotHappy ());
				booing = true;
			}
		}else {
			isHappy= true;
		}
	}

	//possibly have audience react to these
	public void smallCheer() {
		Debug.Log("small cheer!");
		if (isHappy) {
			StartCoroutine (MakeNoise (smallCheerNoise));
		}
		happyLevel+=smallCheerAmount;
	}
	public void bigCheer() {
		Debug.Log("big cheer!");
		if (isHappy) {
			StartCoroutine (MakeNoise (bigCheerNoise));
		}
		happyLevel+=bigCheerAmount;
	}

	IEnumerator MakeNoise(AudioSource noise) {
		yield return new WaitForSeconds (delayUntilCheer);
		noise.Play ();
	}

	IEnumerator NotHappy() {
		while (!isHappy) {
			booNoise.Play ();
			yield return new WaitForSeconds (8f);
		}
	}
}
