using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    public Point GridPosition { get; private set; }
    public string Type { get; private set; }
    public bool IsTower { get; private set; }

    private Color startColor;
    // Use this for initialization
    void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}

    public void Setup(Point gridPos, Vector3 worldPos, int type, Transform parent) {

        this.GridPosition = gridPos;
        transform.position = worldPos;
        this.Type = "";
        this.IsTower = false;

        if (type == 0){

            this.Type = "water";

        }else if (type == 1) {
     
            this.Type = "wall";

        }else if (type == 2) {

            this.Type = "dock";

        }else if (type == 3) {

            this.Type = "path";

        }else if (type == 4) {

            this.Type = "room";

        }
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
        startColor = this.GetComponent<Renderer>().material.color;
    }

   /* private void OnMouseEnter() {

        if (this.Type == "wall") {
            startColor = this.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.color = Color.red;
        }

    }*/

    private void OnMouseExit() {

        if (this.Type == "wall") {
            this.GetComponent<Renderer>().material.color = startColor;
        }

    }

    public void setCurrentTile() {
        //if (this.Type == "wall") {
            //GameObject towerMenu = LevelManager.Instance.TowerMenu;
            //towerMenu.GetComponent<RectTransform>().transform.position = loc;
            //towerMenu.SetActive(true);
            //LevelManager.Instance.TowerPanel.SetActive(true);
            GameManager.Instance.CurrentTile = this;
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

    public void PlaceTower() {
        if (GameManager.Instance.ClickedBtn != null) {
            GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
            tower.transform.SetParent(transform);
            GameManager.Instance.BuyTower();
            this.IsTower = true;
        }
    }
}
