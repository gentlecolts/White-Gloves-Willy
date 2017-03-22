using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawner : MonoBehaviour {
	public int maxEnemyCount=6;

	public float spawnRate=10;
	private float spawnTimer=0;
	
	public EnemyMovement[] enemyTypes;
	public Transform[] spawnRegions;

	
	public enum CurveType {Constant, Additive,Log};
	[Space(10)]
	public CurveType difficultyCurve;
	[Range(0,1)]
	[Tooltip("percent by which difficultyStepSeconds increases if Additive or Log is set")]
	public float timeChangePercent;
	[Tooltip("Initial time in seconds between spawn rate increases")]
	public float difficultyStepSeconds;
	
	private float difficultyTimer;
	private float initialRate;

	// Use this for initialization
	void Start () {
		foreach(Transform obj in spawnRegions) {
			obj.gameObject.SetActive(false);
		}

		Random.InitState((int)System.DateTime.Now.Ticks);

		initialRate=difficultyStepSeconds;
		difficultyTimer=difficultyStepSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		difficultyTimer-=Time.deltaTime;
		if(difficultyTimer<=0) {
			++maxEnemyCount;
			switch(difficultyCurve) {
			case CurveType.Additive:
				difficultyStepSeconds+=initialRate*timeChangePercent;
				break;
			case CurveType.Log:
				difficultyStepSeconds*=1+timeChangePercent;
				break;
			}

			difficultyTimer=difficultyStepSeconds;
		}

		spawnTimer-=Time.deltaTime;

		int enemyCount;
		if(spawnTimer<=0 && (enemyCount=GameObject.FindGameObjectsWithTag("Enemy").Length)<maxEnemyCount) {//if the timer says spawn more and we dont have enough enemies
			//Debug.Log("spawning more enemies");
			spawnTimer=spawnRate;
			enemyCount=maxEnemyCount-enemyCount;
			while(enemyCount>0) {
				Transform spawnner=spawnRegions[Random.Range(0,spawnRegions.Length)];
				Vector3 spawnPoint=spawnner.position
					+new Vector3(
						spawnner.localScale.x*Random.Range(-.5f,.5f),
						spawnner.localScale.y*Random.Range(-.5f,.5f),
						0
					);
				Instantiate(enemyTypes[Random.Range(0,enemyTypes.Length)],spawnPoint,Quaternion.identity);
				--enemyCount;
				//Debug.Log("made enemy at: "+spawnPoint);
			}
		}
	}
}
