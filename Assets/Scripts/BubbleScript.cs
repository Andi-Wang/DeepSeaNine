using UnityEngine;
using System.Collections;

public class BubbleScript : MonoBehaviour
{

	public Point GridPosition { get; private set; }

	void start(){}

	void update() {}

	public void setup(Point gridPosition, Vector3 worldPosition, Transform parent) {
		this.GridPosition = gridPosition;
		this.transform.position = worldPosition;
		transform.SetParent (parent);
	}

}