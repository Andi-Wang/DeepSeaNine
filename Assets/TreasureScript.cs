using UnityEngine;
using System.Collections;

public class TreasureScript : MonoBehaviour
{
	[SerializeField]
	public float ProjectileSpeed = 5.0f;

	[SerializeField]
	public int Value = 10;

	public Point GridPosition { get; private set; }

	void start(){}

	public void update() {

	}

	void OnTriggerEnter2D(Collider2D coll) {
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