using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownEnemyActive : MonoBehaviour {

	public Collider2D col;

	public void FreezeEnemies(){
		col.enabled = false;
	}

	public void UnfreezeEnemies(){
		col.enabled = true;
	}

}
