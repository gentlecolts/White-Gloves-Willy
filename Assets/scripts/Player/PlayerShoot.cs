using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerShoot : MonoBehaviour {
	public enum MouseButton {
		LeftClick=0,
		RightClick =1
	}
	public MouseButton dashButton;

    public ParticleSystem spark;
    public GameObject switchPro;//shoots this
    private Swapper swamp;
    public float projectilelife = 5f;
	public AudioSource shootNoise;
	
    private GameObject switcher;

	// Use this for initialization
	void Start () {
        spark.Play();
		swamp=switchPro.GetComponent<Swapper>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton((int)dashButton)&&switcher==null){
			shootNoise.Play();
            spark.Stop();
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
    public void Sparkle()
    {
        spark.Play();
    }
}
