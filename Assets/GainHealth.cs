using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHealth : MonoBehaviour {

	public PlayerHealthManager health;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			health.health += 1;
		}
	}
}
