using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudienceMeter : MonoBehaviour {
	const float happyMax=100;

	public float cheerTimer;

	[Space(10)]
	public float heartLifeTime;
	public GameObject thrownHeart;
	public Transform heartSpawnRegion;
	private EnemySpawner[] spawnRegions;
	private GameObject player;

	static AudienceMeter instance;
	public static AudienceMeter Instance {
		get {return instance; }
	}

	private float happyLevel;
	public float HappyLevel {
		get {return happyLevel; }
	}

	[Space(10)]
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

		spawnRegions=FindObjectsOfType<EnemySpawner>();

		player=GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(CheerLoop());

		if(heartSpawnRegion) {
			heartSpawnRegion.gameObject.SetActive(false);
		}
	}
	
	IEnumerator CheerLoop() {
		while(true) {
			yield return new WaitForSeconds(cheerTimer);
			if(happyLevel>happyMax/2) {
				if(heartSpawnRegion==null) {
					Debug.Log("Audience would have spawned something good, but heartSpawnRegion was null");
				}else {
					Debug.Log("Spawning happy item");

					Vector3 spawnPoint=heartSpawnRegion.position
						+new Vector3(
							heartSpawnRegion.localScale.x*Random.Range(-.5f,.5f),
							heartSpawnRegion.localScale.y*Random.Range(-.5f,.5f),
							0
						);

					Destroy(Instantiate(thrownHeart,spawnPoint,Quaternion.identity),heartLifeTime);
				}
			}else {
				if(spawnRegions.Length>0) {
					Debug.Log("Spawning unhappy item");
					spawnRegions[Random.Range(0,spawnRegions.Length)].maxEnemyCount++;
				}else {
					Debug.Log("Audience would have done something nasty, but no spawn regions");
				}
			}
		}
	}
	
	void Update () {
		happyLevel=Mathf.Clamp(happyLevel-drainPerSec*Time.deltaTime,0,happyMax);
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
