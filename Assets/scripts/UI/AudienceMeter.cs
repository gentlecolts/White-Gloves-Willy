using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMeter : MonoBehaviour {

	public float HappyLevel;
	public Image audiencefill;

	void Start () {
	}
	
	void Update () {
		audiencefill.fillAmount = HappyLevel;
		//HappyLevel should be a float between 0 and 1, 0 is empty and 1 is full.
		//It also probably won't be public in the final script.
		//Yeah.
	}
}
