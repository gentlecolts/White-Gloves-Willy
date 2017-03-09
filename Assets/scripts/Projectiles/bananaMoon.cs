using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMoon : MonoBehaviour {

    public float despawntime;
    public bananaMove imDeadNowMove;
    private float kill;

    void Awake()
    {
        kill = despawntime + (float)3.5;
    }
	
	// Update is called once per frame
	void Update () {
        bool need = gameObject.GetComponent<Rigidbody2D>().IsAwake();
        if (need)
        {
            kill -= Time.deltaTime;
            Debug.Log(kill);
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
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("something");
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("i recognize that ou are doing it right");
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}
