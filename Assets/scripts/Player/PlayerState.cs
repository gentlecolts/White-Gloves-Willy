using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
public class PlayerState : MonoBehaviour {
	//general movement
	public float speed=2,jumpSpeed=2;

	private Rigidbody2D body;
	private Collider2D boxcol;
	private bool onGround;
	private int lastDir=1;

	public bool isOnGround{
		get {return onGround;}
	}
	public Rigidbody2D Body{
		get{return body;}
	}
	public int LastDir {
		get {return lastDir; }
	}

	//counter mechanic
	[Space(10)]
	public Color counterColor;
	public int counterWindupFrames=5,counterActiveFrames=15,counterCooldownFrames=30;
	public float knockbackForce=20000;

	private int counterCounter;

	private enum CounterState {NEUTRAL,STARTUP,ACTIVE,COOLDOWN };
	private CounterState cState;
	
	//msc vars
	[Space(10)]
	public Color cooldownColor;
	public Color damageColor;
	[HideInInspector]
	public bool doingSomething=false;//use this when any action is being done to prevent other actions
	[HideInInspector]
	public float xSpeed,ySpeed;

	private int groundIDMask;
	private SpriteRenderer mat;
	private Color NormalColor;

	// Use this for initialization
	void Start () {
		body=GetComponent<Rigidbody2D>();
		boxcol=GetComponent<Collider2D>();

		cState=CounterState.NEUTRAL;

		mat=GetComponent<SpriteRenderer>();
		NormalColor=mat.color;

		groundIDMask=1<<LayerMask.NameToLayer("Terrain");
	}
	
	// Update is called once per frame
	void Update () {
		xSpeed=doingSomething?xSpeed:speed*Input.GetAxis("Horizontal");//ignore player input if it's in a movement state
		ySpeed=body.velocity.y;

		onGround=OnGround();
		//Debug.Log(OnGround().ToString());

		if(xSpeed!=0) {
			lastDir=(xSpeed>0)?1:-1;
		}

		//check for jump
		if(onGround && !doingSomething && Input.GetButtonDown("Jump")) {
			ySpeed+=jumpSpeed;
			onGround=false;
		}

		//Debug.Log(onGround+" "+doingSomething);
		//Debug.Log(dState);

		//check counter state
		switch(cState) {
		case CounterState.NEUTRAL:
			if(!doingSomething && Input.GetKeyDown("x")) {
				EnterCounterStartup();
			}
			break;
		case CounterState.STARTUP:
			if(--counterCounter==0){
				EnterCounter();
			}else {
				xSpeed=0;
			}
			break;
		case CounterState.ACTIVE:
			if(--counterCounter==0) {
				EnterCounterCooldown();
			}else {
				xSpeed=0;
			}
			break;
		case CounterState.COOLDOWN:
			//Debug.Log("cooling: "+onGround);
			if(--counterCounter==0) {//dont countdown unless on ground
				EnterCounterNeutral();
			}else {
				xSpeed=0;
			}
			break;
		}

		body.velocity=new Vector2(xSpeed,ySpeed);
	}

	public RaycastHit2D OnGround() {
		Vector2[] vectors= {//center check is probably redundant but is left in for now (terrain could have spikes in the future, for example)
			boxcol.bounds.min,//bottom left
			new Vector2(boxcol.bounds.max.x,boxcol.bounds.min.y),//bottom right
			new Vector2(boxcol.bounds.center.x,boxcol.bounds.min.y)//bottom center
		};

		RaycastHit2D hit=new RaycastHit2D();
		foreach(Vector2 v in vectors) {
			hit=Physics2D.Raycast(v,Vector2.down,0.05f,groundIDMask);
			if(hit) {return hit;}
		}
		return hit;
	}
	public void TakeDamage() {
	}

	//Counter state change code
	void EnterCounterNeutral() {
		cState=CounterState.NEUTRAL;//reset immediately
		doingSomething=false;
		body.constraints=RigidbodyConstraints2D.None;

		mat.color=NormalColor;
	}
	void EnterCounterStartup() {
		counterCounter=counterWindupFrames;

		cState=CounterState.STARTUP;
		doingSomething=true;

		//would start playing startup animation here?
	}
	void EnterCounter() {
		cState=CounterState.ACTIVE;
		counterCounter=counterActiveFrames;
		body.constraints=RigidbodyConstraints2D.FreezePositionY;

		mat.color=counterColor;

		//would enter Counter animation here
	}
	void EnterCounterCooldown() {
		cState=CounterState.COOLDOWN;
		counterCounter=counterCooldownFrames;

		body.constraints=RigidbodyConstraints2D.None;
		
		mat.color=cooldownColor;
	}


	//colliders, note that enemy is a trigger, not a collider
	void OnCollisionEnter2D(Collision2D col) {
	}
	void OnCollisionStay2D(Collision2D col) {
		//Debug.Log("triggered by "+col.collider.name);
	}

	void OnTriggerEnter2D(Collider2D col) {
		//Debug.Log("triggered by "+col.name);
		if(col.tag=="Enemy") {
			if(cState==CounterState.ACTIVE) {
				//Debug.Log("countered!");
			}else{//took damage
				TakeDamage();
			}
		}else if(col.tag=="Bullet") {
			//Debug.Log("bullet hit me!");
			if(cState==CounterState.ACTIVE) {
				col.gameObject.GetComponent<Rigidbody2D>().velocity*=-1;
				col.gameObject.tag="PlayerBullet";
			}else {//too kdamage
				TakeDamage();
				Destroy(col.gameObject);
			}
		}
	}
}
