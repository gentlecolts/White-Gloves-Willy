using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMoon : MonoBehaviour {

    public float despawntime;
    private float kill;

    void Awake()
    {
        kill = despawntime;
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
    
    void OnTriggerEnter2D(Collision2D col)
    {
        Debug.Log("shits colliding");
        if (col.gameObject.tag == "Enemy")
        {
           GameObject nemey = col.gameObject;
            nemey.GetComponent<EnemyMovement>().Die();
            Debug.Log(col);

        }
    }
}
