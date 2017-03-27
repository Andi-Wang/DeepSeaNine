using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PirateShip : Movable {

	int movementFrequency { get; set; } //frames
	int movementTimer; //frames
	List<Vector3> directions;
	int movementCount;

	// Use this for initialization
	void Start () {
		moveSprite(location);
		restrictedTileTypes = new string[]{};
		movementFrequency = 30;
		movementTimer = 0;
	}

	void Update() {
		movementTimer++;
		if (movementTimer > movementFrequency) {
			movementTimer = 0;
			moveInDirection (directions [movementCount]);
			movementCount++;

			// if the ship has arrived at a dock
			if (movementCount >= directions.Count) {

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
