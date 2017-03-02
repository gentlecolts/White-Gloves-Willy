using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelSpawn : MonoBehaviour {

    public int spawnNum;
    public GameObject objspawn;
    public float spawnDelay;

    private int increment;
    private float spawnTime;

    void Start()
    {
        increment = 0;
        spawnTime = spawnDelay;
    }
	void Update ()
    {
        spawnTime -= Time.deltaTime;
        if(spawnTime<=.1)
        {
            int controller = increment % 4;
            //Debug.Log(controller);
            if (controller == spawnNum)
            {
                Vector3 pos = this.transform.position;
                Instantiate(objspawn, pos, Quaternion.identity);
                spawnTime = spawnDelay;
                increment++;
            }
            else
            {
                spawnTime = spawnDelay;
                increment++;
            }
        }
	}
}
