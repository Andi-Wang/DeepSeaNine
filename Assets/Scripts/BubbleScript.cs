using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour
{
	[SerializeField]
	public float ProjectileSpeed = 2.0f;

	public Point GridPosition { get; private set; }

	void start(){}

	public void update() {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "BubbleWall") {
			this.GetComponent<Rigidbody2D> ().velocity = this.GetComponent<Rigidbody2D> ().velocity * -1f;
			print ("collision");
		}
	}

	public void setup(Point gridPosition, Vector3 worldPosition, Transform parent) {
		this.GridPosition = gridPosition;
		this.transform.position = worldPosition;
		transform.SetParent (parent);
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 1f) * ProjectileSpeed;
	}

}