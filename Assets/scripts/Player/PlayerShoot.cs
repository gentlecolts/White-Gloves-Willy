using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
	public enum MouseButton {
		LeftClick=0,
		RightClick =1
	}
	public MouseButton dashButton;
	
    public GameObject switchPro;//shoots this
    private Swapper swamp;
    public float projectilelife = 5f;
	
    private GameObject switcher;

	// Use this for initialization
	void Start () {
		swamp=switchPro.GetComponent<Swapper>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton((int)dashButton)&&switcher==null){
			Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 obPos = transform.position;
			Vector2 direction = mousePos - obPos;
			direction.Normalize();
			switcher = Instantiate(switchPro, obPos, Quaternion.identity);
			Rigidbody2D proVel = switcher.GetComponent<Rigidbody2D>();
			proVel.velocity = direction * swamp.speed;
			Destroy(switcher, projectilelife);
		}
	}
}
