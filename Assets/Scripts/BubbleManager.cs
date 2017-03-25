using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour {

	public float initialDelay = 15f;
	public float spawnTime = 5f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("spawn", initialDelay, spawnTime);
	}
	// Spawns in a bubble in the surrounding.
	void spawn() {
	
	}

	// Update is called once per frame
	void Update () {
		
	}
}
