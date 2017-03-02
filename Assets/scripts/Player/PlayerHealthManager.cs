using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerHealthManager : MonoBehaviour {
	public GameObject healthHeart;
	public const float startHealth=4;

	private PlayerState player;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
