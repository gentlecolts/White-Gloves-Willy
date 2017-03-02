using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerDash : MonoBehaviour {
	private PlayerState PlayerState;

	public enum MouseButton {
		LeftClick=0,
		RightClick =1
	}
	public MouseButton dashButton;
	
	public int dashWindupFrames=2,dashMissFrames=30;//frame timing for dash
	public float dashDistance=1.5f,dashSpeed=10,dashBounceSpeed=5;//how fast is the dash

	public float pushForce=1;

	private int dashCounter;//count frames
	private float dashVel;//the actual speed AND directon of the dash
	private Vector3 dashStartPos;

	private enum DashState {NEUTRAL,STARTUP,DASH,COOLDOWN };
	private DashState dState;

	// Use this for initialization
	void Start () {
		PlayerState=GetComponent<PlayerState>();
		dState=DashState.NEUTRAL;
	}
	
	// Update is called once per frame
	void Update () {
		switch(dState) {
		case DashState.NEUTRAL:
			if(!PlayerState.doingSomething && Input.GetMouseButtonDown((int)dashButton)) {
				EnterDashStartup();
			}
			break;
		case DashState.STARTUP:
			if(--dashCounter==0){
				EnterDash();
			}
			break;
		case DashState.DASH:
			if(Vector3.Distance(dashStartPos,transform.position)>=dashDistance) {
				EnterDashCooldown();
			}else {
				PlayerState.xSpeed=dashVel;
			}
			break;
		case DashState.COOLDOWN:
			//Debug.Log("cooling: "+onGround);
			if(PlayerState.isOnGround && --dashCounter==0) {//dont countdown unless on ground
				EnterDashNeutral();
			}else {
				PlayerState.xSpeed=0;
			}
			break;
		}
	}

	//Dash state change code
	void EnterDashNeutral() {
		dState=DashState.NEUTRAL;//reset immediately
		PlayerState.doingSomething=false;
		PlayerState.Body.constraints&=~RigidbodyConstraints2D.FreezePositionY;
	}
	void EnterDashStartup() {
		dashCounter=dashWindupFrames;
		dashVel=PlayerState.LastDir*dashSpeed;

		dState=DashState.STARTUP;
		PlayerState.doingSomething=true;

		//would start playing startup animation here?
	}
	void EnterDash() {
		dState=DashState.DASH;
		dashStartPos=transform.position;
		PlayerState.Body.constraints|=RigidbodyConstraints2D.FreezePositionY;

		//would enter dash animation here
	}
	void EnterDashCooldown() {
		dState=DashState.COOLDOWN;
		dashCounter=dashMissFrames;

		PlayerState.Body.constraints&=~RigidbodyConstraints2D.FreezePositionY;
	}

	void OnCollisionEnter2D(Collision2D col) {
		TestWall(col);
	}
	void OnCollisionStay2D(Collision2D col) {//note that enemy is a trigger, not a collider
		TestWall(col);
	}

	void TestWall(Collision2D col) {
		if(PlayerState.OnGround().collider!=col.collider && dState==DashState.DASH) {//if we are dashing and collided with something that isnt the ground
			EnterDashCooldown();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.tag=="Enemy") {
			if(dState==DashState.DASH) {//dashed into someone
				EnterDashNeutral();
				PlayerState.Body.velocity=new Vector2(PlayerState.Body.velocity.x,dashBounceSpeed);

				//Vector2 force=(enemy.transform.position-transform.position).normalized*pushForce;
				//Vector2 force=pushForce*(Vector2.down+Vector2.right*PlayerState.LastDir).normalized
				col.GetComponent<EnemyMovement>().Stun(pushForce*(Vector2.down+Vector2.right*PlayerState.LastDir).normalized);
			}
		}
	}
}
