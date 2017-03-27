using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PirateSpawner : MonoBehaviour {

    [SerializeField]
	public GameObject piratePrefab;

	private List<Point> spawnPoints;
    private Point location;
	private float spawnFrequency { get; set; } // number of frames
	private float spawnTimer;

	//public Dictionary<Point, PirateShip> ships;

	// Use this for initialization
	void Start () {
		spawnPoints = new List<Point> (){new Point(1,1) }; //LevelManager.Instance.WaterTiles;
		spawnFrequency = 600;
		spawnTimer = 600;
	}
	
	void Update () {
        TileScript currentTile = LevelManager.Instance.Tiles[location];
        if (currentTile.Type == "dock")
        {
            spawnTimer++;
            if (spawnTimer > spawnFrequency)
            {
                spawnTimer = 0;
                pirateSpawn();
            }
        }
	}

	// spawns ship in random location from list of valid spawn points
	private void pirateSpawn(){
		Point spawnPoint = spawnPoints [(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, spawnPoints.Count))];
		//PirateShip ship = Instantiate (pirateShipPrefab).GetComponent<PirateShip> ();
		//ship.setupShipAt (spawnPoint);
	}

}