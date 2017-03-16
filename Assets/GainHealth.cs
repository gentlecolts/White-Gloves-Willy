using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainHealth : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			PlayerHealthManager.Instance.health++;
			Destroy (gameObject);
		}

	}
}
