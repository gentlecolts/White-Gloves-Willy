using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (EnemyMovement))]
public class PatrollerMovement : MonoBehaviour {
	public float moveSpeed;
	private int dir=1;

	private Rigidbody2D body;
	private EnemyMovement mover;
	private float startY;


	[Space(10)]
	public bool swoop;
	public float dipDistance;
	public float swoopDelay=1;
	public float swoopDuration;

	private float sTime;
	private bool swooping;
	private float swoopInitTime;
	private Vector3 lastSwoopPos;

	// Use this for initialization
	void Start () {
		body=GetComponent<Rigidbody2D>();
		sTime=swoopDelay;

		mover=GetComponent<EnemyMovement>();
		startY=transform.position.y;
	}

	Vector3 swoopFn(float t) {//t should be from 0 to 1
		return new Vector3(
			t*swoopDuration*moveSpeed,//[0,1]*t*(d/t)=[0,1]*d
			-dipDistance*t*4*(1-t),//-dipDistance * [parabola connecting points (0,0),(0.5,1),(1,0)]
			0
		);
	}
	
	// Update is called once per frame
	void Update () {
		if(mover.IsStunned) {
			swooping=false;
			sTime=0;
			return;
		}

		if(swoop && !swooping) {
			sTime-=Time.deltaTime;
		}

		if(!swoop || sTime>0) {//always do this if swoop is false
			body.velocity=new Vector3(dir,startY-transform.position.y,0).normalized*moveSpeed;
		}else {
			swooping=true;
			swoopInitTime=Time.time;
			lastSwoopPos=swoopFn(0);
			sTime=swoopDelay;
		}

		if(swooping) {
			float newtime=Time.time;
			if(newtime-swoopInitTime>=swoopDuration) {
				swooping=false;
				newtime=swoopInitTime+swoopDuration;
			}

			Vector3 newpos=swoopFn((newtime-swoopInitTime)/swoopDuration);
			Vector3 delta=newpos-lastSwoopPos;
			delta.x*=dir;
			transform.position+=delta;
			lastSwoopPos=newpos;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		doHitAction(col.collider);
	}
	void OnTriggerEnter2D(Collider2D col) {
		doHitAction(col);
	}

	void doHitAction(Collider2D col) {
		if(col.gameObject.tag!="Player") {
			dir=-dir;
		}
	}
}
