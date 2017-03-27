using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {
    private float projectileSpeed = 20;

    public Vector3 face;
	// Use this for initialization
	void Start () {
        
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
