﻿using System.Collections;
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
    public GameObject[] TowerMenu { get; private set; }

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

    public void setTowerMenu(GameObject towerMenu, int playerNumber) {
        if(TowerMenu == null) {
            TowerMenu = new GameObject[4];
        }
        TowerMenu[playerNumber - 1] = towerMenu;
    }
}
