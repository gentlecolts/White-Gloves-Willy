using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PatrollerMovement : MonoBehaviour {
	public float moveSpeed;
	private int dir=1;
	private Rigidbody2D body;

	[Space(10)]
	public bool swoop;
	public float dipDistance;
	public float swoopFrequency=1;
	public float swoopDuration;

	private float sTime;
	private bool swooping;

	// Use this for initialization
	void Start () {
		body=GetComponent<Rigidbody2D>();
		sTime=swoopFrequency;
	}
	
	// Update is called once per frame
	void Update () {
		if(swoop) {
			sTime-=Time.deltaTime;
		}

		if(!swoop || sTime>0) {//always do this if swoop is false
			body.velocity=new Vector3(moveSpeed*dir,0,0);
		}else {
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		dir=-dir;
	}
}
