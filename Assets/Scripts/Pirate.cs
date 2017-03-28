using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Pirate : Movable {

	int movementFrequency { get; set; } //frames
	int movementTimer; //frames
	List<Vector3> directions;
	int movementCount;

	int health;

	// Use this for initialization
	void Start () {
		moveSprite(location);
		restrictedTileTypes = new string[]{};
		movementFrequency = 30;
		movementTimer = 0;
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		// if the pirate has arrived at the bubbleactor
		if (movementCount >= directions.Count) {
            LevelManager.Instance.damageHealth(10f);
            Destroy(gameObject);
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
		directions = AI.aStar (start, LevelManager.Instance.getPointsByType("goal") , "path");
		movementCount = 0;
	}

    public void damage(int d) {
        health -= d;
        if(health <= 0) {
            foreach(Player player in PlayerManager.Instance.playerArray) {
                player.Gold += 5;
            }
            Destroy(gameObject);
        }
    }

}
