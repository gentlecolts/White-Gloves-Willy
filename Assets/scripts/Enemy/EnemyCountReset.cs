using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCountReset : MonoBehaviour {
	void Start () {
		EnemyMovement.resetCounter();
		Destroy(gameObject);
	}
}
