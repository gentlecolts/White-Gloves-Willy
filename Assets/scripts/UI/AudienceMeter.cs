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

	[Range(0,happyMax)]
	public float startValue,smallCheerAmount,bigCheerAmount,drainPerSec;
	


	void Start () {
		instance=this;
		happyLevel=startValue;
	}
	
	void Update () {
		happyLevel-=drainPerSec*Time.deltaTime;


		happyLevel=Mathf.Clamp(happyLevel,0,happyMax);
		audiencefill.fillAmount = happyLevel/happyMax;
	}

	//possibly have audience react to these
	public void smallCheer() {
		Debug.Log("small cheer!");
		happyLevel+=smallCheerAmount;
	}
	public void bigCheer() {
		Debug.Log("big cheer!");
		happyLevel+=bigCheerAmount;
	}
}
