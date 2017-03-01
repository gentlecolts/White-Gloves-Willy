using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour {
	public float throwSpeed,throwCooldown;
	
	private GameObject player;
	private bool isThrown=false;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(isThrown) {
		}else {
			//aim in direction
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePos - (Vector2)player.transform.position;
			//direction.Normalize();
			float angleSgn=-Mathf.Sign(direction.x);
			transform.rotation=Quaternion.AngleAxis(angleSgn*Vector2.Angle(Vector2.up,direction),Vector3.forward);
		}
	}

	public void Shoot() {
	}
}
