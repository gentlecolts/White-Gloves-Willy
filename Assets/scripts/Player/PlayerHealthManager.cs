using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
public class PlayerHealthManager : MonoBehaviour {
	public GameObject healthHeart;
	public float health=4;
	public float spacing=3;
	public GameObject gameOverMenu;

	Stack<GameObject> hearts=new Stack<GameObject>();

	private PlayerState player;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();

		//clear existing
		foreach (Transform child in transform) {
			DestroyImmediate(child.gameObject);
		}
	}

	bool isDie=false;
	void DieRoutine() {
		if(isDie) {//only run this once
			return;
		}
		isDie =true;

		//Stop the player
		GameObject player=GameObject.FindGameObjectWithTag("Player");
		foreach(MonoBehaviour mb in player.GetComponentsInChildren<MonoBehaviour>()) {
			//Debug.Log(mb.GetType());
			mb.enabled=false;//disable ALL of the player's scripts
		}

		//Display the game over menu
		//SceneManager.LoadScene("UIStructure");
		gameOverMenu.SetActive(true);

		//Play animation
		FindObjectOfType<CrookAnim>().activate();
	}
	
	// Update is called once per frame
	void Update () {
		health=(health<0)?0:health;//cannot be less than zero

		GameObject heartHolder;
		while(hearts.Count>health) {//need to take hearts away from
			heartHolder=hearts.Pop();
			DestroyImmediate(heartHolder);
		}

		while(hearts.Count<health) {//need to add health
			hearts.Push(Instantiate(healthHeart,transform));
		}

		heartHolder=null;
		Vector3 spaceVector=new Vector3(spacing,0,0);
		foreach(GameObject heart in hearts) {
			if(heartHolder) {
				heart.transform.localPosition=heartHolder.transform.localPosition+spaceVector;
			}else {
				heart.transform.localPosition=Vector3.zero;
			}
			heartHolder=heart;
		}

		if(health==0) {
			DieRoutine();
		}
	}
}
