using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : Singleton<BubbleManager> {

	// The zIndex layer that Bubbles are going to reside on.
	private int zIndex = -11;

	[SerializeField]
	private Transform BubbleLayer;

	[SerializeField]
	public GameObject BubblePrefab;

	[SerializeField]
	public float initialDelay = 15f;

	[SerializeField]
	public float spawnTime = 5f;

	public Dictionary<Point, BubbleScript> Bubbles;

	// Use this for initialization
	void Start () {
		// Fetch a list of possible spawn locations.
		Bubbles = new Dictionary<Point, BubbleScript>();
		InvokeRepeating ("spawn", initialDelay, spawnTime);
	}

	// Spawns in a bubble at a random location chosen from valid points.
	void spawn() {
		// Spawns a bubble that is randomly chosen from the list of valid bubble spawn points.
		List<Point> validSpawnPoints = LevelManager.Instance.WaterTiles;
		Point potentialPoint = default(Point);
		while (potentialPoint.Equals(default(Point)) || Bubbles.ContainsKey (potentialPoint)) {
			potentialPoint = validSpawnPoints [(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, validSpawnPoints.Count))];
		}
		spawnBubble(potentialPoint);
	}

	// Spawns in a bubble at a specific location. Does not check if it is a valid tile, so god help you if you mess it up.
	void spawnBubble (Point location) {
		Vector3 worldStart = LevelManager.Instance.worldStart;
		float tileSize = LevelManager.Instance.TileSize;

		BubbleScript bs = Instantiate(BubblePrefab).GetComponent<BubbleScript>();
		bs.setup(location, new Vector3(worldStart.x + (tileSize * location.X), worldStart.y - (tileSize * location.Y), this.zIndex), BubbleLayer);
		Bubbles.Add (location, bs);
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
