﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets._2D;

public class TileScript : MonoBehaviour {

	public GameObject HookPrefab;

    public Point GridPosition { get; private set; }
    public string Type { get; private set; }
    public bool IsTower { get; private set; }

    private bool isAmmo = false;
    private static int hookCount = 0;

    // Use this for initialization
    void Start() {
        IsTower = false;
	}
	
	// Update is called once per frame
	void Update() {
		
	}

    public void Setup(Point gridPos, Vector3 worldPos, int type, Transform parent) {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        this.Type = "";
        this.IsTower = false;

		if (type == 0) {

			this.Type = "water";
			LevelManager.Instance.WaterTiles.Add (gridPos);

		} else if (type == 1) {
     
			this.Type = "wall";

		} else if (type == 2) {

			this.Type = "dock";

		} else if (type == 3) {

			this.Type = "path";

		} else if (type == 4) {

			this.Type = "room";

		} else if (type == 5) {
			
			this.Type = "hook";
			// Create a new hook object for the tile.
			HookScript hs = Instantiate(HookPrefab).GetComponentInChildren<HookScript>();
			hs.setup (hookCount, worldPos, this.transform);
			hookCount++;
		} else if (type == 6) {
            this.Type = "goal";
        }
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

		if (this.Type == "water") {
			LevelManager.Instance.WaterTiles.Add(gridPos);
		}else if(this.Type == "room") {
            LevelManager.Instance.roomTiles.Add(gridPos);
        }

    }

   /* private void OnMouseEnter() {

        if (this.Type == "wall") {
            startColor = this.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.color = Color.red;
        }

    }*/

    /*private void OnMouseExit() {

        if (this.Type == "wall") {
            this.GetComponent<Renderer>().material.color = startColor;
        }

    }*/

    public void setCurrentTile(int playerNumber) {
        //if (this.Type == "wall") {
            //GameObject towerMenu = LevelManager.Instance.TowerMenu;
            //towerMenu.GetComponent<RectTransform>().transform.position = loc;
            //towerMenu.SetActive(true);
            //LevelManager.Instance.TowerPanel.SetActive(true);
        GameManager.Instance.CurrentTile[playerNumber - 1] = this;
        //}
    }

    /*private void OnMouseOver() {

        //display tower menu when user clicks wall tile.
       if (!EventSystem.current.IsPointerOverGameObject()) {
            if (this.Type == "wall") {
                if (Input.GetMouseButtonDown(0)) {
                    startColor = this.GetComponent<Renderer>().material.color;
                    this.GetComponent<Renderer>().material.color = Color.blue;
                    LevelManager.Instance.Canvas.SetActive(true);
                    //LevelManager.Instance.TowerPanel.SetActive(true);
                    GameManager.Instance.CurrentTile = this;
                    
                }
           }
        }
        /*if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {

            if (Input.GetMouseButtonDown(0))
            {

                PlaceTower();
            }

        }
    }*/

    public void PlaceTower(int playerNumber) {
        if (GameManager.Instance.ClickedBtn[playerNumber - 1] != null && !IsTower) {
            GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn[playerNumber - 1].TowerPrefab, transform.position, Quaternion.identity);
            tower.transform.SetParent(transform);
            PlayerManager.Instance.playerArray[playerNumber - 1].Gold -= GameManager.Instance.ClickedBtn[playerNumber - 1].Gold;
            GameManager.Instance.BuyTower(playerNumber);
            this.IsTower = true;
            LevelManager.Instance.towers.Add(tower);
        }
    }

    public void PlaceAmmo() {
        GameObject ammo = (GameObject)Instantiate(LevelManager.Instance.AmmoBox, transform.position, Quaternion.identity);
        //ammo.transform.SetParent(transform);
        this.isAmmo = true;
    }

    public void updateUsables(int playerIdx) {
        if (this.isAmmo) {
            PlayerManager.Instance.playerArray[playerIdx - 1].Ammo = PlayerManager.Instance.playerArray[playerIdx - 1].ClipSize * 10;
            PlayerManager.Instance.playerArray[playerIdx - 1].AmmoInClip = PlayerManager.Instance.playerArray[playerIdx - 1].ClipSize;
        }
    }
}
