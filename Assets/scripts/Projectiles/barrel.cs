﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrel : MonoBehaviour {
    public float speed;

    private Vector2 speeder;
	// Use this for initialization
	
	void LateUpdate () {
        Rigidbody2D rolled = GetComponent<Rigidbody2D>();
        speeder = rolled.velocity;
        speeder.x = speed;
        rolled.velocity = speeder;
	}
}