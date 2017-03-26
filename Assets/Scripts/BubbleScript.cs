using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour
{
	[SerializeField]
	public float ProjectileSpeed = 5.0f;

	public Point GridPosition { get; private set; }

	void start(){}

	public void update() {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag.Equals("BubbleWall")) {
//			float worldStartX = LevelManager.Instance.worldStart.x;
//			float tileSize = LevelManager.Instance.TileSize;
//			print ("collision " +  transform.position);
//			Vector2 directionalChange = new Vector2 (-1, -1);
//			if (transform.position.x == worldStartX - tileSize / 2
//				|| transform.position.x == worldStartX - tileSize / 2 + (tileSize * LevelManager.Instance.MapX)) {
//				print ("Redirecting along x axis");
//				directionalChange = new Vector2 (-1, 1);
//			} else {
//				directionalChange = new Vector2 (1, -1);
//			}
//
//			this.GetComponent<Rigidbody2D> ().velocity = Vector2.Scale (this.GetComponent<Rigidbody2D> ().velocity, directionalChange);
		}
	}

	public void setup(Point gridPosition, Vector3 worldPosition, Transform parent) {
		this.GridPosition = gridPosition;
		this.transform.position = worldPosition;
		transform.SetParent (parent);
		this.GetComponent<Rigidbody2D>().velocity = generateRandomUnitVector() * ProjectileSpeed;
	}

	private Vector2 generateRandomUnitVector() {
		float angle = Random.Range (0, 2 * Mathf.PI);
		// Calculate the x and y component using good ol' math.

		return new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
	}

}