using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {
    public Vector3 face;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        currentTile();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void currentTile() {
        Vector3 pos = transform.position;
        LevelManager lm = LevelManager.Instance;

        int tileSize = (int)(lm.TileSize * 100);

        // Don't look at this disgusting mess.
        // For some reason our main developer decided to use a float for tile size and
        // 1 pixel is .01 in value. 
        int x = ((int)System.Math.Abs((lm.worldStart.x + pos.x) * 100) / tileSize);
        int y = ((int)System.Math.Abs((lm.worldStart.y - pos.y) * 100) / tileSize);

        Point current = new Point(x, y);
        Debug.Log("(" + lm.worldStart.x + "," + lm.worldStart.y + ")");
        //Debug.Log("(" + pos.x + "," + pos.y + ")");
        //Debug.Log("(" + current.X + "," + current.Y + ")");

        TileScript tile = lm.Tiles[current]; 
        if (tile.Type == "water") {
            Destroy(gameObject);
        } 
    }
}
