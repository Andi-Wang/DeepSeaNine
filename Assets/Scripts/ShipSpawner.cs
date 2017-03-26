using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : Singleton<ShipSpawner> {

	[SerializeField]
	public GameObject pirateShipPrefab;

	private List<Point> spawnPoints;
	private float spawnFrequency { get; set; } // number of frames
	private float spawnTimer;

	public Dictionary<Point, PirateShip> ships;

	// Use this for initialization
	void Start () {
		spawnPoints = new List<Point> (){new Point(1,1) }; //LevelManager.Instance.WaterTiles;
		spawnFrequency = 600;
		spawnTimer = 600;
	}
	
	void Update () {
		spawnTimer++;
		if (spawnTimer > spawnFrequency) {
			spawnTimer = 0;
			validShipSpawn ();
		}
	}

	// spawns ship in random location from list of valid spawn points
	private void validShipSpawn(){
		spawnShip (spawnPoints [(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, spawnPoints.Count))]);
	}

	// spawn a ship at location. Warning: dDoes does not check if it is a valid tile
	private void spawnShip (Point location){
		PirateShip ship = Instantiate (pirateShipPrefab).GetComponent<PirateShip> ();
		ship.setupShipAt (location);
	}
}
