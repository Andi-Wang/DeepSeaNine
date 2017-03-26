using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager> {

	[SerializeField]
	private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    [SerializeField]
    private MiniMapLimits miniMap;

	// Variables for quickly accessing the size of the map.
	public int MapX;
	public int MapY;

    public Dictionary<Point, TileScript> Tiles { get; set; }

	// Valid water tiles.
	public List<Point> WaterTiles { get; set; }

    //public GameObject TowerPanel { get; private set; }
    public GameObject TowerMenu { get; private set; }

	public float TileSize{
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x; }
	}

    public Vector3 worldStart { get; private set; }



	// Use this for initialization
	void Start() {
		CreateLevel();
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	private void CreateLevel(){

        Tiles = new Dictionary<Point, TileScript>();
		this.WaterTiles = new List<Point> ();

        string[] mapData = ReadLevelText();

		MapX = mapData[0].ToCharArray().Length;
		MapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < MapY; y++) {
            char[] newTiles = mapData[y].ToCharArray();
			for (int x = 0; x < MapX; x++) {
				
				PlaceTile(newTiles[x].ToString(), x, y, worldStart);
			}
		}

        maxTile = Tiles[new Point(MapX - 1, MapY - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize), TileSize/2);
        miniMap.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize), TileSize/2);
        //TowerPanel = GameObject.Find("TowerPanel");
        //TowerPanel.SetActive(false);
        TowerMenu = GameObject.Find("TowerPanel");
        TowerMenu.SetActive(false);

		createBoundaryCollisionBoxes ();
    }

	private void PlaceTile(string tileType, int x, int y, Vector3 worldStart){

        int tileIndex = int.Parse(tileType);

		TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), tileIndex, map);

    }

    private string[] ReadLevelText(){
        TextAsset bindData = Resources.Load("Level5") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        return data.Split('-');
    }

	private void createBoundaryCollisionBoxes() {
		List<Vector2> edgePoints = new List<Vector2> ();
		edgePoints.Add (new Vector2 (worldStart.x, worldStart.y));
		edgePoints.Add (new Vector2 (worldStart.x + (TileSize * MapX), worldStart.y));
		edgePoints.Add (new Vector2 (worldStart.x + (TileSize * MapX), worldStart.y - (TileSize * (MapY - 1))));
		edgePoints.Add (new Vector2 (worldStart.x , worldStart.y - (TileSize * (MapY - 1))));
		edgePoints.Add (new Vector2 (worldStart.x, worldStart.y));

		print ("Creating collisionboxes");
		map.GetComponent<EdgeCollider2D>().points = edgePoints.ToArray ();

	}
}
