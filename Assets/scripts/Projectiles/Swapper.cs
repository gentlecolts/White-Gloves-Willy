using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour {

	public float speed;

	// private bool hit;
	private GameObject _playerTran;

	void Start()
	{
		//  hit = false;
		_playerTran = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
		//do stuff maybe?
	}

	void OnCollisionEnter2D(Collision2D col){
		testCollision(col.collider);
	}
	void OnTriggerEnter2D(Collider2D col){
		testCollision(col);
	}

	void testCollision(Collider2D col) {
        //Debug.Log(col.gameObject.name);
		GameObject mover = col.gameObject;
		Vector3 swap = mover.transform.position;
		Vector3 newPos = _playerTran.transform.position;
		_playerTran.transform.position = swap;
		mover.transform.position = newPos;
		mover.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        if (col.tag=="Enemy")
        {
            col.gameObject.GetComponent<EnemyMovement>().Stun();
        }
		Destroy(this.gameObject);
	}
    void OnDestroy()
    {
        _playerTran.GetComponent<PlayerShoot>().Sparkle();
    }
}
