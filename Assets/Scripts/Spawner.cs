using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	private Point[] spawnPoints;
	private float spawnRate = 0; // every minute

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setSpawnPoints(){
		spawnPoints = new Point[]{ new Point (1, 1) };
	}
}
