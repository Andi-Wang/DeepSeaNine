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

    public Dictionary<Point, TileScript> Tiles { get; set; }
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

        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++) {
            char[] newTiles = mapData[y].ToCharArray();
			for (int x = 0; x < mapX; x++) {
				
				PlaceTile(newTiles[x].ToString(), x, y, worldStart);
			}
		}

        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;
        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize), TileSize/2);
        miniMap.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize), TileSize/2);
        //TowerPanel = GameObject.Find("TowerPanel");
        //TowerPanel.SetActive(false);
        TowerMenu = GameObject.Find("TowerPanel");
        TowerMenu.SetActive(false);
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

	public List<Point> getPointsByType(String type){
		List<Point> typePoints = new List<Point>();
		foreach (KeyValuePair<Point, TileScript> tile in Tiles){
			if (tile.Value.Type == type){
				typePoints.Add(tile.Key);
			}
		}
		return typePoints;
	}

	public List<Point> getPerimeterPoints(){
		List<Point> perimeterTiles = new List<Point> ();
		int maxX = 0; //because hack
		int maxY = 0;
		foreach (Point tile in Tiles.Keys) {
			if (tile.X == 0 || tile.Y == 0) {
				perimeterTiles.Add (tile);
			}
			if (tile.X > maxX){ maxX = tile.X;}
			if (tile.Y > maxY) {maxY = tile.Y;}
		}
		foreach (Point tile in Tiles.Keys) {
			if (tile.X == maxX || tile.Y == maxY) {
				perimeterTiles.Add (tile);
			}
		}
		return perimeterTiles;
	}
}
