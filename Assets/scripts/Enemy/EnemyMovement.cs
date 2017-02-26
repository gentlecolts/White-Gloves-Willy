using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public enum MovementType {Bobber,Follower };
	public MovementType moveType;

	[Space(10)]
	public float bobHeight;
	public float bobRate;

	[Space(10)]
	public float followRange;
	public float followSpeed;
	
	private float timer=0,lastsin=0;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		switch(moveType) {
		case MovementType.Bobber:
			timer+=Time.deltaTime;
			float newsin=Mathf.Sin(2*Mathf.PI*timer/bobRate);

			transform.position+=new Vector3(0,bobHeight*(newsin-lastsin),0);
			lastsin=newsin;
			break;
		case MovementType.Follower:
			if(Vector3.Distance(player.transform.position,transform.position)<=followRange) {
				transform.position=Vector3.MoveTowards(transform.position,player.transform.position,followSpeed*Time.deltaTime);
			}
			break;
		}
	}
}
