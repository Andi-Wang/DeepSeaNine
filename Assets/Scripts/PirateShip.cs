﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PirateShip : Movable {

	int movementFrequency { get; set; } //frames
	int movementTimer; //frames
	List<Vector3> directions;
	int movementCount;

	[SerializeField]
	public GameObject pirateSpawnerPrefab;
	bool setupPirateSpawner;

	// Use this for initialization
	void Start () {
		moveSprite(location);
		restrictedTileTypes = new string[]{};
		movementFrequency = 30;
		movementTimer = 0;
		setupPirateSpawner = false;
	}

	void Update() {
		// if the ship has arrived at a dock
		if (movementCount >= directions.Count) {
			if (!setupPirateSpawner) {
				PirateSpawner pirateSpawner = Instantiate (pirateSpawnerPrefab).GetComponent<PirateSpawner> ();
				setupPirateSpawner = true;
			}

		// otherwise
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
		facing = Vector3.right;
		moveSprite(location);
		directions = AI.aStar (start, end, "water");
		movementCount = 0;
	}
}
