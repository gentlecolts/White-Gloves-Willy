using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bananaMove : MonoBehaviour {

    public float moveDown;

    private Vector3 CurrentPos;
    private Vector3 newPos;

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
        
        CurrentPos = Vector3.MoveTowards(CurrentPos, newPos, Time.deltaTime*3);
        this.transform.position = CurrentPos;
        //Debug.Log(Vector3.MoveTowards(CurrentPos, newPos, 100));
        // Debug.Log("Moviing?");
    }
}
