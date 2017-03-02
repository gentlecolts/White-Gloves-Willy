using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class PlayerHealthManager : MonoBehaviour {
	public GameObject healthHeart;
	public float health=4;
	public float spacing=3;

	Stack<GameObject> hearts=new Stack<GameObject>();

	private PlayerState player;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();

		//clear existing
		foreach (Transform child in transform) {
			GameObject.DestroyImmediate(child.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
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
	}
}
