using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDisplay : MonoBehaviour {
	public Text killCountBox,timeBox;

	// Use this for initialization
	void OnEnable() {
		float timeSecs=Time.timeSinceLevelLoad;
		Debug.Log("player killed "+EnemyMovement.EnemiesKilled);
		killCountBox.text=EnemyMovement.EnemiesKilled.ToString();
		timeBox.text=Mathf.Floor(timeSecs/60)+":"+Mathf.RoundToInt(timeSecs%60);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
