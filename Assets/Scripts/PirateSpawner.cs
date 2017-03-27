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
	private PirateShip pirateShip;

	private bool setUp = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (setUp) {
			spawnTimer++;
			if (spawnTimer > spawnFrequency && piratesRemaining > 0) {
				spawnTimer = 0;
				pirateSpawn ();
				piratesRemaining--;

				// destroy ship when no pirates remain
				if (piratesRemaining <= 0) {
					Destroy (pirateShip.gameObject);
				}
			}
		}
	}

	// spawns pirate at spawn point (where ship is docked)
	private void pirateSpawn(){
		Pirate pirate = Instantiate (piratePrefab).GetComponent<Pirate> ();
		pirate.setupPirate (spawnPoint);
	}

	// sets up parameters so that spawner can start spawning
	// called from pirate ship
	public void setUpSpawner(Point p, PirateShip ship){
		spawnPoint = p;
		pirateShip = ship;
		spawnFrequency = 240;
		spawnTimer = 600;
		piratesRemaining = 4;
		setUp = true;
	}

}
