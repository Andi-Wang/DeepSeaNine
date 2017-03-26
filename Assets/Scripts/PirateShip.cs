using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateShip : Movable {

	// Use this for initialization
	void Start () {
		moveSprite(location);
		restrictedTileTypes = new string[]{};
	}

	public void setupShipAt(Point p) {
		location = p;
		facing = Vector3.right;
		moveSprite(location);
	}
}
