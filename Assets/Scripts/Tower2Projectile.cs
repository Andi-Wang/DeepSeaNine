using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower2Projectile : MonoBehaviour {
    private float projectileSpeed = 20;

	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().velocity = transform.forward * projectileSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
