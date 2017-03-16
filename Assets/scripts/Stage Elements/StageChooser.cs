using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChooser : MonoBehaviour {

    public GameObject Stage1;
    public GameObject Stage2;
    public float pubTim;

    private float timer;
    private GameObject _Stage;
    private bool statement;
    // Use this for initialization
    void Start()
    {
        statement = true;
        timer = 15;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (_Stage == null && timer <= .1)
        {
            Vector3 pos = transform.position;
            if (statement)
            {
                _Stage = Instantiate(Stage1, pos, Quaternion.identity);
                timer = pubTim;
                statement = false;
            }
            else
            {
                _Stage = Instantiate(Stage2, pos, Quaternion.identity);
                timer = pubTim;
                statement = true;
            }
        }
    }
}
