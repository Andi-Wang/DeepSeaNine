using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PirateShip : Movable {

	int movementFrequency { get; set; } //frames
	int movementTimer; //frames
	List<Vector3> directions;
	int movementCount;
	Point destination; // used to pass to pirate spawner

	[SerializeField]
	public GameObject pirateSpawnerPrefab;
	bool setupPirateSpawner;

	int health;

	// Use this for initialization
	void Start () {
		moveSprite(location);
		restrictedTileTypes = new string[]{};
		movementFrequency = 30;
		movementTimer = 0;
		setupPirateSpawner = false;
		health = 1000;
	}

	void Update() {
		// if the ship has arrived at a dock
		if (movementCount >= directions.Count) {
			if (!setupPirateSpawner) {
				PirateSpawner pirateSpawner = Instantiate (pirateSpawnerPrefab).GetComponent<PirateSpawner> ();
				pirateSpawner.setUpSpawner (destination, this);
				setupPirateSpawner = true;
			}

		// otherwise, drive to a dock
		} else {
			movementTimer++;
			if (movementTimer > movementFrequency) {
				movementTimer = 0;
				facing = directions [movementCount];
				moveInDirection (directions [movementCount]);
				movementCount++;
			}
		}
	}

	public void setupShip(Point start, Point end) {
		location = start;
		destination = end;
		facing = Vector3.right;
		moveSprite(location);
		directions = AI.aStar (start, end, "water");
		movementCount = 0;
	}
}
