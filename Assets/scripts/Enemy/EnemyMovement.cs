using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyMovement : MonoBehaviour {
	//public enum MovementType {Bobber,Follower };
	//public MovementType moveType;

	public float stunTime=2;
	public float dieTime=1;
	public Animator display;
	public AudioSource HatDieNoise;
	public AudioSource HatHitNoise;
    public ParticleSystem poof;

	private bool stunned=false;
	public bool IsStunned {
		get {return stunned;}
	}

	[Space(10)]
	public float bobHeight;
	public float bobRate;

	[Space(10)]
	public float followRange;
	public float followSpeed;
	
	private float timer=0,lastsin=0;
	private GameObject player;

	static int killCounter=0;
	public static int EnemiesKilled{
		get{return killCounter;}
	}
	public static void resetCounter() {killCounter=0; }

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
        poof.Play();
        poof.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		if(stunned) {
			return;
		}

		//switch(moveType) {
		//case MovementType.Bobber:
			timer+=Time.deltaTime;
			float newsin=Mathf.Sin(2*Mathf.PI*timer/bobRate);

			transform.position+=new Vector3(0,bobHeight*(newsin-lastsin),0);
			lastsin=newsin;
		//	break;
		//case MovementType.Follower:
			if(Vector3.Distance(player.transform.position,transform.position)<=followRange) {
				transform.position=Vector3.MoveTowards(transform.position,player.transform.position,followSpeed*Time.deltaTime);
			}
		//	break;
		//}
	}

	public void Stun() {Stun(stunTime,Vector2.zero);}
	public void Stun(float time) {Stun(time,Vector2.zero);}
	public void Stun(Vector2 force) {Stun(stunTime,force); }
	public void Stun(float time,Vector2 force) {
		StartCoroutine(StunRoutine(time,force));
	}

	IEnumerator StunRoutine(float time,Vector2 force){
		//disable the scripted movement
		stunned=true;

		//make noise
		HatHitNoise.Play();

		//make it collide properly
		Collider2D col=GetComponent<Collider2D>();
		col.isTrigger=false;
		gameObject.layer=LayerMask.NameToLayer("EnemyFallen");

		//turn physics on and add our force
		Rigidbody2D rb=GetComponent<Rigidbody2D>();
		rb.bodyType=RigidbodyType2D.Dynamic;
		rb.AddForce(force);
		
		//wait for a bit
		yield return new WaitForSeconds(time);

		//disable physics
		rb.bodyType=RigidbodyType2D.Kinematic;
		//back to being a trigger
		col.isTrigger=true;
		gameObject.layer=LayerMask.NameToLayer("Enemy");
		//reenable scripted movement
		stunned=false;
	}

	void OnCollisionEnter2D(Collision2D col) {
		if(stunned && col.gameObject.layer==LayerMask.NameToLayer("EnemyFallen")) {//two fallen enemies colliding with eachother should result in mutual destruction
			Die();
		}
	}
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.layer==LayerMask.NameToLayer("Enemy")) {//enemies cannot collide with enemies, but can collide with EnemyFallen
			Die();
		}
	}

	public void Die() {
		AudienceMeter.Instance.bigCheer();
		HatDieNoise.Play ();
        poof.Play();
        Destroy(gameObject, dieTime);
        display.SetTrigger ("Die");
		this.enabled=false;

		++killCounter;
	}
}
