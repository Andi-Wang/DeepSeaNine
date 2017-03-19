using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : Singleton<GameManager> {

    public TowerBtn ClickedBtn { get; private set; }
    public TileScript CurrentTile { get; set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickTower(TowerBtn towerBtn) {

        this.ClickedBtn = towerBtn;

        //place the tile after it is picked
        CurrentTile.PlaceTower();
        //to hide menu
        LevelManager.Instance.Canvas.SetActive(false);

    }

    public void BuyTower() {
        this.ClickedBtn = null;
    }
}
