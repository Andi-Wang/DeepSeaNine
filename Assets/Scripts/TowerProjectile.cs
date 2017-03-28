using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour {
    public Vector3 face;

    [SerializeField]
    private int damage;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        currentTile();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            //Destroy(other.gameObject);
            Debug.Log(damage);
            other.gameObject.GetComponent<Pirate>().damage(damage);
            Destroy(gameObject);
        }
    }

    void currentTile() {
        Vector3 pos = transform.position;
        LevelManager lm = LevelManager.Instance;

        float tileSize = lm.TileSize;

        // Don't look at this disgusting mess.
        // For some reason our main developer decided to use a float for tile size and
        // 1 pixel is .01 in value. 
        int x = (int)(System.Math.Abs(pos.x - (lm.worldStart.x - tileSize / 2)) / tileSize);
        int y = (int)(System.Math.Abs(pos.y - (lm.worldStart.y + tileSize / 2)) / tileSize);

        Point current = new Point(x, y);
        //Debug.Log("(" + lm.worldStart.x + "," + lm.worldStart.y + ")");
        //Debug.Log("(" + pos.x + "," + pos.y + ")");
        //Debug.Log("(" + current.X + "," + current.Y + ")");

        TileScript tile = lm.Tiles[current]; 
        if (tile.Type == "water") {
            Destroy(gameObject);
        } 
    }
}
