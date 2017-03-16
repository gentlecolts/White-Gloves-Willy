using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrookAnim : MonoBehaviour {
	public float grabSpeed;
	public GameObject crookEnd;
	public GameObject playerAnchor;
	public ParticleSystem poof;

	bool grabbing=false,grabbed=false;
	SpriteRenderer sprite;
	GameObject player;

	// Use this for initialization
	void Start () {
		sprite=GetComponent<SpriteRenderer>();
		sprite.enabled=false;

		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(grabbed) {
			transform.position+=new Vector3(Mathf.Sign(transform.position.x)*grabSpeed*Time.deltaTime,0,0);
			ParticleSystem poofer=Instantiate(poof,player.transform.position,Quaternion.identity);
			Destroy(poofer.gameObject,poofer.main.duration);
		}else if(grabbing) {
			transform.position=Vector2.MoveTowards(transform.position,player.transform.position,grabSpeed*Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if(!grabbed && col.tag=="Player") {
			grabbed=true;
			crookEnd.SetActive(true);
			player.transform.parent=playerAnchor.transform;

			Rigidbody2D playerRB=player.GetComponent<Rigidbody2D>();
			playerRB.isKinematic=true;
			playerRB.velocity=Vector2.zero;
			
			player.transform.localPosition=Vector3.zero;
		}
	}

	public void activate() {
		if(player.transform.position.x<=0) {//move the crook to the left side and flip it
			Vector3 tmp=transform.position;
			tmp.x=-tmp.x;
			transform.position=tmp;

			tmp=transform.localScale;
			tmp.x=-1;
			transform.localScale=tmp;
		}

		sprite.enabled=true;
		grabbing=true;
	}
}
