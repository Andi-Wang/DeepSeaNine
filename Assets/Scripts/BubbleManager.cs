using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : Singleton<BubbleManager> {

	// The zIndex layer that Bubbles are going to reside on.
	private int zIndex = -9;

	[SerializeField]
	private Transform BubbleLayer;

	[SerializeField]
	public GameObject BubblePrefab;

	[SerializeField]
	public GameObject TreasurePrefab;

	[SerializeField]
	public int MaxBubbleCount = 10;

	[SerializeField]
	public int MaxTreasureCount = 10;

	[SerializeField]
	public float InitialBubbleDelay = 5f;

	[SerializeField]
	public float InitialTreasureDelay = 0f;

	[SerializeField]
	public float BubbleSpawnTime = 5f;

	[SerializeField]
	public float TreasureSpawnTime = 3f;

	public List<BubbleScript> Bubbles;
	public List<TreasureScript> Treasures;

	// Use this for initialization
	void Start () {
		// Fetch a list of possible spawn locations.
		Bubbles = new List<BubbleScript>();
		Treasures = new List<TreasureScript> ();
		InvokeRepeating ("spawnBubbleRepeating", InitialBubbleDelay, BubbleSpawnTime);
		InvokeRepeating ("spawnTreasureRepeating", InitialTreasureDelay, TreasureSpawnTime);
	}

	// Spawns in a bubble at a random location chosen from valid points.
	void spawnBubbleRepeating() {
		// Spawns a bubble that is randomly chosen from the list of valid bubble spawn points.
		if(Bubbles.Count < MaxBubbleCount) {
			List<Point> validSpawnPoints = LevelManager.Instance.WaterTiles;
			Point potentialPoint = default(Point);
			while (potentialPoint.Equals(default(Point))) {
				potentialPoint = validSpawnPoints [(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, validSpawnPoints.Count))];
			}
			spawnBubble(potentialPoint);
		}
	}

	// Spawns in a bubble at a specific location. Does not check if it is a valid tile, so god help you if you mess it up.
	void spawnBubble (Point location) {
		Vector3 worldStart = LevelManager.Instance.worldStart;
		float tileSize = LevelManager.Instance.TileSize;

		BubbleScript bs = Instantiate(BubblePrefab).GetComponent<BubbleScript>();
		bs.setup(location, new Vector3(worldStart.x + (tileSize * location.X), worldStart.y - (tileSize * location.Y), this.zIndex), BubbleLayer);
		Bubbles.Add (bs);
	}

	void spawnTreasureRepeating() {
		if (Treasures.Count < MaxTreasureCount) {
			List<Point> validSpawnPoints = LevelManager.Instance.WaterTiles;
			Point potentialPoint = default(Point);
			while (potentialPoint.Equals(default(Point))) {
				potentialPoint = validSpawnPoints [(int)System.Math.Floor ((float)UnityEngine.Random.Range (0, validSpawnPoints.Count))];
			}
			spawnTreasure(potentialPoint);
		}
	}

	void spawnTreasure(Point location) {
		Vector3 worldStart = LevelManager.Instance.worldStart;
		float tileSize = LevelManager.Instance.TileSize;
		TreasureScript ts = Instantiate(TreasurePrefab).GetComponent<TreasureScript>();
		ts.setup(location, new Vector3(worldStart.x + (tileSize * location.X), worldStart.y - (tileSize * location.Y), this.zIndex), BubbleLayer);
		Treasures.Add (ts);
	}
		
	// Update is called once per frame
	void Update () {
		
	}
}
