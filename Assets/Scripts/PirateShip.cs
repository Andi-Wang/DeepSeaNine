using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class PirateShip : Movable {

	int movementFrequency { get; set; } //frames
	int movementTimer; //frames

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
			moveShip ();
		}
	}

	public void setupShip(Point p){//, Point destination) {
		location = p;
		facing = Vector3.right;
		moveSprite(location);
	}

	private void moveShip (){
		moveInDirection (Vector3.down);
	}
}
