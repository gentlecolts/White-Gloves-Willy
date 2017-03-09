using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
public class PlayerState : MonoBehaviour {
	public GameObject display;
	public PlayerHealthManager health;
	//general movement
	public float speed=2,jumpSpeed=2;

	private Rigidbody2D body;
	private CapsuleCollider2D col;
	private Animator animator;
	private SpriteMeshInstance[] sprites;
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

	[Space(10)]
	public float invulnTime=1;
	private float invulnTimer=0;
	public void makeInvuln() {invulnTimer=invulnTime; }
	
	//msc vars
	[Space(10)]
	public Color cooldownColor;
	public Color damageColor;
	[HideInInspector]
	public bool doingSomething=false;//use this when any action is being done to prevent other actions
	[HideInInspector]
	public float xSpeed,ySpeed;

	private int groundIDMask;
	//private SpriteRenderer mat;
	private Color NormalColor;

	// Use this for initialization
	void Start () {
		animator = display.GetComponent<Animator> ();
		body=GetComponent<Rigidbody2D>();
		col=GetComponent<CapsuleCollider2D>();

		//mat=GetComponent<SpriteRenderer>();
		//=mat.color;

		groundIDMask=1<<LayerMask.NameToLayer("Terrain");

		health=GameObject.FindObjectOfType<PlayerHealthManager>();

		sprites = GetComponentsInChildren<SpriteMeshInstance> ();
		
	}

	bool enteredObject=false;
	bool checkOneWayHit() {
		RaycastHit2D[] r=new RaycastHit2D[1];;
		return col.Cast(Vector2.zero,r)>0;
	}

	void FixedUpdate() {
		if(body.velocity.y>0) {
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("OneWay"),true);
			enteredObject=checkOneWayHit();
			Debug.Log("going up! hit state: "+enteredObject);
		}else if(enteredObject){//entered an object while going up and didnt exit it before going back down
			enteredObject=checkOneWayHit();
			Debug.Log("not going up, but also in OneWay");
		}else {
			Debug.Log("now allowed to collide with OneWay");
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),LayerMask.NameToLayer("OneWay"),false);
		}
	}

	// Update is called once per frame
	void Update () {
		xSpeed=doingSomething?xSpeed:speed*Input.GetAxis("Horizontal");//ignore player input if it's in a movement state
		ySpeed=body.velocity.y;

		invulnTimer-=Time.deltaTime;

		onGround=OnGround();
		if (onGround) {
			animator.SetBool ("Jump", false);
		}
			//Debug.Log(OnGround().ToString());

		/*if(xSpeed!=0) {
			lastDir=(xSpeed>0)?1:-1;
		} // - Original ternary operator replaced to deal with extra animator variable assignments*/
		if (xSpeed > 0) {
			lastDir = 1;
			animator.SetBool ("FaceRight", true);
			animator.SetBool ("isWalking", true);
		} else if (xSpeed < 0) {
			lastDir = -1;
			animator.SetBool ("FaceRight", false);
			animator.SetBool ("isWalking", true);
		} else {
			animator.SetBool ("isWalking", false);
		}

		//check for jump
		if(onGround && !doingSomething && Input.GetButtonDown("Jump")) {
			animator.SetBool ("Jump", true);
			ySpeed+=jumpSpeed;
			onGround=false;
		}

		//Debug.Log(onGround+" "+doingSomething);
		//Debug.Log(dState);

		body.velocity=new Vector2(xSpeed,ySpeed);
	}

	public RaycastHit2D OnGround() {
		Vector2[] vectors= {//center check is probably redundant but is left in for now (terrain could have spikes in the future, for example)
			col.bounds.min,//bottom left
			new Vector2(col.bounds.max.x,col.bounds.min.y),//bottom right
			new Vector2(col.bounds.center.x,col.bounds.min.y)//bottom center
		};

		RaycastHit2D hit=new RaycastHit2D();
		foreach(Vector2 v in vectors) {
			hit=Physics2D.Raycast(v,Vector2.down,0.05f,~(1<<LayerMask.NameToLayer("Player")));
			
			if(hit && hit.transform.gameObject.tag!="Enemy") {return hit;}
		}
		return new RaycastHit2D();//missed
	}
	public void TakeDamage() {
		if(!doingSomething && invulnTimer<=0) {
			--health.health;
			invulnTimer=invulnTime;
			StartCoroutine(colorPulseRed ());
		}
	}

	IEnumerator colorPulseRed() {
		while (invulnTimer>0) {
			foreach (SpriteMeshInstance sprite in sprites) {
				sprite.color = Color.Lerp (Color.white, Color.red, Mathf.PingPong (Time.time*4, 1));
			}
			yield return null;
		}
		foreach (SpriteMeshInstance sprite in sprites) {
			sprite.color = Color.white;
		}
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
			TakeDamage();
		}else if(col.tag=="Bullet") {
			TakeDamage();
			Destroy(col.gameObject);
		}
	}
}
