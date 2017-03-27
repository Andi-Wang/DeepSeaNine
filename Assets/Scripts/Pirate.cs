using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate : Movable {

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
	
	// Update is called once per frame
	void Update () {
		// if the pirate has arrived at the bubbleactor
		if (movementCount >= directions.Count) {
		
		// otherwise
		} else {
			movementTimer++;
			if (movementTimer > movementFrequency) {
				movementTimer = 0;
				//facing = directions [movementCount]; causes silly rotation for now
				moveInDirection (directions [movementCount]);
				movementCount++;
			}
		}
	}

	public void setupPirate(Point start) {
		location = start;
		facing = Vector3.right;
		moveSprite(location);
		directions = AI.aStar (start, new Point(22,17), "path");
		movementCount = 0;
	}
}
