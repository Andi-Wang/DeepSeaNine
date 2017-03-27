using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour {

	private bool isFiring;
	private bool isReturning;
	private Vector3 spriteSize;
	private Vector3 pivotPoint;
	private Vector3 dockedPoint;

	[SerializeField]
	public float RotateRate;

	// Use this for initialization
	void Start () {
		isFiring = false;
		isReturning = false;
	}
		
	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.CompareTag("HookTarget") && isFiring) {
			coll.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			coll.gameObject.transform.SetParent (transform);
			coll.gameObject.transform.localPosition = Vector2.zero;
			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity * -1;
			RetrieveHook ();
		} else if(coll.CompareTag("MapBoundary")) {
			RetrieveHook ();
		}
	}

	public void setup(int hookPos, Vector3 worldPosition, Transform parent) {
		
		spriteSize = this.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		Vector3 localHookPosition = Vector3.zero;
		// Check the diagonals of the hook tile for a water tile.
		if (hookPos == 0) {
			// top left.
			localHookPosition = new Vector3 (worldPosition.x - spriteSize.x, worldPosition.y + spriteSize.y, worldPosition.z);
		} else if (hookPos == 1) {
			// top right
			localHookPosition = new Vector3 (worldPosition.x + spriteSize.x, worldPosition.y + spriteSize.y, worldPosition.z);
		} else if (hookPos == 2) {
			// bot left
			localHookPosition = new Vector3 (worldPosition.x - spriteSize.x, worldPosition.y, worldPosition.z);
		} else if (hookPos == 3) {
			// bot right.
			localHookPosition = new Vector3 (worldPosition.x + spriteSize.x, worldPosition.y, worldPosition.z);

		}
			
		this.transform.localPosition = dockedPoint = localHookPosition;
		pivotPoint = transform.position - new Vector3 (0, spriteSize.y / 2, 0);
		transform.SetParent (parent);
	}
		
	public void LaunchHook() {
		if (!isFiring && !isReturning) {
			dockedPoint = transform.position;
			isFiring = true;
			print (transform.forward);
			GetComponent<BoxCollider2D> ().enabled = true;
			GetComponent<Rigidbody2D> ().velocity = transform.up * 10;
		}
	}

	public void RetrieveHook() {
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity * -1f;
		GetComponent<BoxCollider2D> ().enabled = false;
		isReturning = true;
	}

	public void MoveHookClockwise() {
		if (!isFiring) {
			transform.RotateAround (pivotPoint, new Vector3 (0, 0, -1), RotateRate * Time.deltaTime);
		}
	}

	public void MoveHookCounterClockwise() {
		if (!isFiring) {
			transform.RotateAround (pivotPoint, new Vector3 (0, 0, 1), RotateRate * Time.deltaTime);
		}
	}

	// Update is called once per frame
	// TODO: Change so it is only controllable by the controlling player.
	void Update () {
		
		if (isReturning) {
			// Calculate the distance.
			if (Vector2.Distance(dockedPoint, transform.position) < 0.5f) {
				GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
				transform.position = dockedPoint;
				// Destroy bubbles attached.
				int killCount = transform.childCount;
				foreach (Transform child in transform) {
					BubbleManager.Destroy(child.gameObject);
                    LevelManager.Instance.updateHealth();
				}
				print ("Killed " + killCount + " bubbles");
				isFiring = false;
				isReturning = false;
			}
		}
	}
}
