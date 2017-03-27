using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSpawner : Singleton<ShipSpawner> {

	[SerializeField]
	public GameObject pirateShipPrefab;

	private List<Point> spawnPoints;
	private List<Point> destinations;
	private float spawnFrequency { get; set; } // number of frames
	private float spawnTimer;

	private Dictionary<Point, bool> destinationClaimed; 

	public Dictionary<Point, PirateShip> ships;

	// Use this for initialization
	void Start () {
		spawnPoints = LevelManager.Instance.getPerimeterPoints(); //new List<Point> (){ new Point (0, 0) };
		destinations = LevelManager.Instance.getPointsByType ("dock");
		spawnFrequency = 600;
		spawnTimer = 600;
	}
	
	void Update () {

        if (Time.timeScale == 1)
        {
            spawnTimer++;
            if (spawnTimer > spawnFrequency)
            {
                spawnTimer = 0;
                shipSpawn();
            }
        }
	}

	// spawns ship in random location from list of valid spawn points
	private void shipSpawn(){
		int randomNumber = (int)System.Math.Floor ((float)UnityEngine.Random.Range (0, spawnPoints.Count));
		Point spawnPoint = spawnPoints [randomNumber];
	
		randomNumber = (int)System.Math.Floor ((float)UnityEngine.Random.Range (0, destinations.Count));
		Point destination = destinations[randomNumber];

		PirateShip ship = Instantiate (pirateShipPrefab).GetComponent<PirateShip> ();
		ship.setupShip (spawnPoint, destination);
	}

}