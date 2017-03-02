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
}
