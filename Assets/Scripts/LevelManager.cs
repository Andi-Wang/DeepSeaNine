using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager> {

	[SerializeField]
	private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovement cameraMovement;

    [SerializeField]
    private Transform map;

    [SerializeField]
    private MiniMapLimits miniMap;

    [SerializeField]
    private GameObject ammoBox;

	// Variables for quickly accessing the size of the map.
	public int MapX;
	public int MapY;

    public Dictionary<Point, TileScript> Tiles { get; set; }

	// Valid water tiles.
	public List<Point> WaterTiles { get; set; }

    // Valid room tiles.
    public List<Point> roomTiles { get; set; }

    //public GameObject TowerPanel { get; private set; }
    public GameObject[] TowerMenu { get; private set; }

	public float TileSize{
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x; }
	}

    public GameObject AmmoBox { get { return ammoBox; } }

    public Vector3 worldStart { get; private set; }

    public GameObject HealthBar { get; set; }

    private float health = 200;

    private float counter;

    private GameObject endPanel;

    private float healthInc = 1;
    private float totalHealth = 200;

    public List<GameObject> towers;

    private int range = 3;
	// Use this for initialization
	void Start() {
		CreateLevel();
	}
	
	// Update is called once per frame
	void Update() {
        counter += Time.deltaTime;
        if (counter >= 1) {
            counter = 0;
            HealthBar.transform.FindChild("HealthBackgroundImage").FindChild("HealthBarImage").GetComponent<Image>().fillAmount -= 1/totalHealth;
            health -= healthInc;
            HealthBar.transform.FindChild("HealthNum").GetComponent<Text>().text = health.ToString();
            if(health == 0) {
                PauseGame.Instance.Pause();
                endPanel.SetActive(true);

            }
        }

        Pirate[] enemies = FindObjectsOfType(typeof(Pirate)) as Pirate[];

        foreach (Pirate enemy in enemies) {
            foreach (GameObject tower in towers) {
                TowerScript towerScript = tower.GetComponent<TowerScript>();
                float distanceX = Math.Abs(enemy.transform.position.x - tower.transform.position.x);
                float distanceY = Math.Abs(enemy.transform.position.y - tower.transform.position.y);
                float distance = distanceX + distanceY;
                if (distance <= range && distance < towerScript.currentEnemyDistance) {
                    towerScript.currentEnemy = enemy.transform;
                    towerScript.currentEnemyDistance = distance;
                }
            }
        }

    }

	private void CreateLevel(){
        Tiles = new Dictionary<Point, TileScript>();
        this.roomTiles = new List<Point>();
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
        cameraMovement.addPlayer(GameObject.Find("player1(Clone)"));
        cameraMovement.addPlayer(GameObject.Find("player2(Clone)"));
        miniMap.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize), TileSize/2);

		createBoundaryCollisionBoxes ();

        placeAmmoBoxes();

        counter = 0;

        HealthBar = PlayerManager.Instance.playerUICanvas.transform.FindChild("BubbleactorHealthPanel").gameObject;
        HealthBar.transform.FindChild("HealthNum").GetComponent<Text>().text = health.ToString();

        endPanel = GameObject.Find("endGameMenuPrefab");
        endPanel.SetActive(false);
        towers = new List<GameObject>();
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
		float left = worldStart.x - TileSize/2;
		float top = worldStart.y + TileSize / 2;
		float bottom = worldStart.y - (TileSize * MapY - TileSize / 2);
		edgePoints.Add (new Vector2 (left , top));
		edgePoints.Add (new Vector2 (left + (TileSize * MapX), top));
		edgePoints.Add (new Vector2 (left + (TileSize * MapX), bottom));
		edgePoints.Add (new Vector2 (left , bottom));
		edgePoints.Add (new Vector2 (left, top));

		print ("Creating collisionboxes");
		map.GetComponent<EdgeCollider2D>().points = edgePoints.ToArray ();

	}
    public void setTowerMenu(GameObject towerMenu, int playerNumber) {
        if(TowerMenu == null) {
            TowerMenu = new GameObject[4];
        }
        TowerMenu[playerNumber - 1] = towerMenu;
    }

    public void placeAmmoBoxes() {
        System.Random rand = new System.Random();
        int num = rand.Next(0, 100);
        int idx = num % (roomTiles.Count - 1);
        Tiles[roomTiles[idx]].PlaceAmmo();
        int idx2 = (num  + roomTiles.Count/2) % (roomTiles.Count - 1);
        Tiles[roomTiles[idx2]].PlaceAmmo();
    }

    public void updateHealth() {
        health += 10;
        if(health > totalHealth) {
            health = totalHealth;
        }
        HealthBar.transform.FindChild("HealthBackgroundImage").FindChild("HealthBarImage").GetComponent<Image>().fillAmount = (health/totalHealth);
        HealthBar.transform.FindChild("HealthNum").GetComponent<Text>().text = health.ToString();
    }

    public void damageHealth(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            HealthBar.transform.FindChild("HealthBackgroundImage").FindChild("HealthBarImage").GetComponent<Image>().fillAmount = (health / totalHealth);
            HealthBar.transform.FindChild("HealthNum").GetComponent<Text>().text = health.ToString();
            PauseGame.Instance.Pause();
            endPanel.SetActive(true);
        }
        else {
            HealthBar.transform.FindChild("HealthBackgroundImage").FindChild("HealthBarImage").GetComponent<Image>().fillAmount = (health / totalHealth);
            HealthBar.transform.FindChild("HealthNum").GetComponent<Text>().text = health.ToString();
        }
    }

    public List<Point> getPointsByType(String type) {
        List<Point> typePoints = new List<Point>();
        foreach (KeyValuePair<Point, TileScript> tile in Tiles) {
            if (tile.Value.Type == type) {
                typePoints.Add(tile.Key);
            }
        }
        return typePoints;
    }

    public List<Point> getPerimeterPoints() {
        List<Point> perimeterTiles = new List<Point>();
        int maxX = 0; //because hack
        int maxY = 0;
        foreach (Point tile in Tiles.Keys) {
            if (tile.X == 0 || tile.Y == 0) {
                perimeterTiles.Add(tile);
            }
            if (tile.X > maxX) { maxX = tile.X; }
            if (tile.Y > maxY) { maxY = tile.Y; }
        }
        foreach (Point tile in Tiles.Keys) {
            if (tile.X == maxX || tile.Y == maxY) {
                perimeterTiles.Add(tile);
            }
        }
        return perimeterTiles;
    }
}
