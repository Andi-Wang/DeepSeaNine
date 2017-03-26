using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager> {

    public TowerBtn[] ClickedBtn { get; private set; }
    public TileScript[] CurrentTile { get; set; }

    // Use this for initialization
    void Start () {
        ClickedBtn = new TowerBtn[4];
        CurrentTile = new TileScript[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickTower(TowerBtn towerBtn, int playerNumber) {

        this.ClickedBtn[playerNumber - 1] = towerBtn;

        //place the tile after it is picked
        CurrentTile[playerNumber - 1].PlaceTower(playerNumber);
        //to hide menu
        //LevelManager.Instance.TowerMenu.SetActive(false);

    }

    public void BuyTower(int playerNumber) {
        this.ClickedBtn[playerNumber - 1] = null;
    }
}
