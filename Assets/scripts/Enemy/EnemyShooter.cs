using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour {
	public bool shoots;
	public GameObject bullet;
	public float range,shootTime,bulletLife,bulletSpeed;

	private GameObject player;
	private float cooldown;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		cooldown-=Time.deltaTime;
		Vector2 dir=player.transform.position-transform.position;

		if(shoots && bullet  && cooldown<=0 && dir.sqrMagnitude<=range*range) {
			cooldown=shootTime;
			GameObject firedShot=Instantiate(bullet,transform.position,transform.rotation);
			firedShot.GetComponent<Rigidbody2D>().velocity=bulletSpeed*dir.normalized;
			Destroy(firedShot,bulletLife);
		}
	}
}
