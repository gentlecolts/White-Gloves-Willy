using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMovement : MonoBehaviour {

    public float moveDown;
    public float killTime;

    private Vector3 CurrentPos;
    private Vector3 newPos;
    private float deSpawn;

    // Use this for initialization
    void Start()
    {
        CurrentPos = this.transform.position;
        newPos = CurrentPos;
        float newPosy = newPos.x - moveDown;
        newPos.x = newPosy;
        deSpawn = killTime;
    }

    // Update is called once per frame
    void Update()
    {
        deSpawn -= Time.deltaTime;
        CurrentPos = Vector3.MoveTowards(CurrentPos, newPos, Time.deltaTime * 7);
        this.transform.position = CurrentPos;
        if (deSpawn <= killTime/4)
        {
            goUp();
            Debug.Log(deSpawn);
        }
        if (deSpawn <= .1)
        {
            Destroy(this.gameObject);
        }
    }

    public void goUp()
    {
        CurrentPos = transform.position;
        newPos = CurrentPos;
        float newPosy = newPos.x + moveDown;
        newPos.x = newPosy;
    }
}
