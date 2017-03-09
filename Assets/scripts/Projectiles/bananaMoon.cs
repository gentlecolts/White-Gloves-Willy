using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMoon : MonoBehaviour {

    public float despawntime;
    private float kill;

    void Awake()
    {
        kill = despawntime + (float)0.001;
    }
	
	// Update is called once per frame
	void Update () {
        bool need = gameObject.GetComponent<Rigidbody2D>().IsAwake();
        if (need)
        {
            kill -= Time.deltaTime;
            if (kill <= .1)
            {
                Destroy(this.gameObject);
            }
        }
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Enemy"&&kill<=despawntime)
        {
           GameObject nemey = col.gameObject;
            nemey.GetComponent<EnemyMovement>().Die();

        }
    }
}
