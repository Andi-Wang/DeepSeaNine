using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapLimits : MonoBehaviour {

    private float xMax;
    private float yMin;

    private void Update() {

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMax), Mathf.Clamp(transform.position.y, yMin, 0), -10);
       
    }

    public void SetLimits(Vector3 maxTile) {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
    }
}
