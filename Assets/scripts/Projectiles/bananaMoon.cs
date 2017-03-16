using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMoon : MonoBehaviour
{

    public float despawntime;
    public AudioSource CrashNoise;
    public GameObject imDeadNowMove;
    private float kill;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(15, 11, true);
        kill = despawntime + (float)3.5;
    }

    // Update is called once per frame
    void Update()
    {
        
        bool need = gameObject.GetComponent<Rigidbody2D>().IsAwake();
        if (need)
        {
            kill -= Time.deltaTime;
            if (kill <= .1)
            {
                imDeadNowMove.GetComponent<bananaMove>().goUp();
                Destroy(this.gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Enemy" && kill <= despawntime)
        {
            CrashNoise.Play();
            GameObject nemey = col.gameObject;
            nemey.GetComponent<EnemyMovement>().Stun();
            nemey.GetComponent<EnemyMovement>().Die();

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "Player")
        {
            //CrashNoise.Play ();
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            Physics2D.IgnoreLayerCollision(15, 11, false);
            
        }

    }
}
