using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMove : MonoBehaviour {

    public float moveDown;
    public float killTime;

    private Vector3 CurrentPos;
    private Vector3 newPos;
    private float deSpawn = 1000000000;

    // Use this for initialization
    void Start () {
        CurrentPos = this.transform.position;
        Debug.Log(CurrentPos);
        newPos = CurrentPos;
        float newPosy = newPos.y - moveDown;
        newPos.y = newPosy;
        Debug.Log(newPos);
        Debug.Log(Vector3.MoveTowards(CurrentPos, newPos,100));
    }
	
	// Update is called once per frame
	void Update () {
        deSpawn -= Time.deltaTime;
        CurrentPos = Vector3.MoveTowards(CurrentPos, newPos, Time.deltaTime*3);
        this.transform.position = CurrentPos;
        if(deSpawn<= .1)
        {
            Destroy(this.gameObject);
        }
        //Debug.Log(Vector3.MoveTowards(CurrentPos, newPos, 100));
        // Debug.Log("Moviing?");
    }
    
    public void goUp()
    {
        deSpawn = killTime;
        CurrentPos = transform.position;
        newPos = CurrentPos;
        float newPosy = newPos.y + moveDown;
        newPos.y = newPosy;
    }
}
