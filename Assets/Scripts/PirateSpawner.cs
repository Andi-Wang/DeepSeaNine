using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawner : MonoBehaviour {

	[SerializeField]
	public GameObject piratePrefab;

	private Point spawnPoint {get; set;}
	private float spawnFrequency { get; set; } // number of frames
	private float spawnTimer;
	private int piratesRemaining;


	// Use this for initialization
	void Start () {
		spawnPoint = new Point(22, 7); //TODO: get from ship
		spawnFrequency = 600;
		spawnTimer = 600;
		piratesRemaining = 6;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer++;
		if (spawnTimer > spawnFrequency && piratesRemaining > 0) {
			spawnTimer = 0;
			pirateSpawn();
			piratesRemaining--;
		}
	}

	// spawns pirate at spawn point (where ship is docked)
	private void pirateSpawn(){
		Pirate pirate = Instantiate (piratePrefab).GetComponent<Pirate> ();
		pirate.setupPirate (spawnPoint);
	}
}
