using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {

	protected Point location;
	protected Vector3 facing;

	protected string[] restrictedTileTypes;

	//Get the next point in the given direction
	protected Point getNextPoint(Vector3 facing) {
		Point next = new Point(location.X + (int)facing.x, location.Y + (int)facing.y);
		return next;
	}

	//Move the player in the given direction if the new space is not on water (illegal); call moveSprite() to update the player's facing direction
	protected void moveInDirection(Vector3 facing) {
		if(facing != Vector3.zero) {
			Point next;
			if (facing == Vector3.up || facing == Vector3.down) {
				next = getNextPoint(-facing);
			}
			else {
				next = getNextPoint(facing);
			}                

			bool unrestrictedTile = true;
			string nextTileType = LevelManager.Instance.Tiles [next].Type;
			foreach (string restrictedTileType in restrictedTileTypes){
				if (nextTileType == restrictedTileType) {
					unrestrictedTile = false;
					break;
				}
			}

			if (unrestrictedTile) {
				location = next;
			}
			moveSprite(location);
		}
	}

	//Put the player on the given point and update facing direction
	protected void moveSprite(Point point) {
		transform.position = new Vector3(LevelManager.Instance.worldStart.x + LevelManager.Instance.TileSize * point.X, LevelManager.Instance.worldStart.y - LevelManager.Instance.TileSize * point.Y, 0);
		float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
