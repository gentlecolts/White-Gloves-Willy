using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSpawn : MonoBehaviour {

    public GameObject Moom;
    public float pubTim;

    private float timer;
    private GameObject Moomy;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (Moomy == null && timer <= .1)
        {
            Vector3 pos = transform.position;
            Moomy = Instantiate(Moom, pos, Quaternion.identity);
            timer = pubTim;
        }
    }
}
