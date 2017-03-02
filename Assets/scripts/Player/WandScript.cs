using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour {
	public float throwSpeed,throwCooldown;
	public float wandRestRadius=16,wandSightRadius=100;
	public float lockonSpeed=100;
	
	private GameObject player,wandSprite;
	private bool isThrown=false;
	private int enemyMask;

	// Use this for initialization
	void Start () {
		player=GameObject.FindGameObjectWithTag("Player");
		wandSprite=transform.FindChild("wandSprite").gameObject;

		enemyMask=1<<LayerMask.NameToLayer("Enemy");
	}
	
	// Update is called once per frame
	void Update () {
		if(isThrown) {
		}else {
			//aim in direction
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 direction = mousePos - (Vector2)player.transform.position;
			//direction.Normalize();
			float angleSgn=-Mathf.Sign(direction.x);
			transform.rotation=Quaternion.AngleAxis(angleSgn*Vector2.Angle(Vector2.up,direction),Vector3.forward);


			RaycastHit2D hit;
			float hitD;
			Vector3 target;
			/*this particular chunk of code deserves some explaination, so here goes
			 * first, we do a raycast from the player's position in the aim direction (but only as far as wandSightRadius and only for objects on the Enemy layer)
			 * then, if there's a hit, we do a bit of math
			 *	so first, hit.distance is in global units, but the player model is scaled (so local transforms are too)
			 *	so we need compute hit.distance/player.transform.localScale.x (distance divided by the scale of the player's x, assuming scale is uniform)
			 *	we then store this value and compare it to 2*wandRestRadius, the reason for the multiplier is to prevent the wand from ever coming closer than wandRestRadius to the player
			 * if we've passed the hit test and the hit distance is far enough, then
			 *	set the y cord to the local hit distance (hitD) minus the rest radius (for spacing, this is why we multiplied by 2 in the condition)
			 * otherwise
			 *	simply set the wand to the rest radius
			 */
			if((hit=Physics2D.Raycast(player.transform.position,direction,wandSightRadius,enemyMask)) && (hitD=hit.distance/player.transform.localScale.x)>2*wandRestRadius){
				target=new Vector3(0,hitD-wandRestRadius,0);
			}else {
				target=new Vector3(0,wandRestRadius,0);
			}
			wandSprite.transform.localPosition=Vector3.MoveTowards(wandSprite.transform.localPosition,target,lockonSpeed*Time.deltaTime);
		}
	}

	public void Shoot() {
	}
}
