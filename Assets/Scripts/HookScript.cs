using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {

	private bool isFiring;
	private Vector3 spriteSize;
	private Vector3 pivotPoint;
	// Use this for initialization
	void Start () {
		isFiring = false;
	}
		
	void OnTriggerEnter2D(Collider2D coll) {
	}

	public void setup(Vector3 worldPosition, Transform parent) {
		spriteSize = this.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		this.transform.position = new Vector3 (worldPosition.x - spriteSize.x, worldPosition.y + spriteSize.y - LevelManager.Instance.TileSize/2, worldPosition.z);
		pivotPoint = transform.position - spriteSize / 2;
		transform.SetParent (parent);
	}
		
	public void LaunchHook() {
	}
	// Update is called once per frame
	void Update () {
		transform.RotateAround(pivotPoint, new Vector3(0,0,1), 20 * Time.deltaTime);
	}
}
