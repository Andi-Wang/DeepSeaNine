using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapLimits : MonoBehaviour {

    private float xMax;
    private float yMin;
    private float offset;

    private void Update() {

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0 - offset, xMax - offset), Mathf.Clamp(transform.position.y, yMin + offset, 0 + offset), -10);
       
    }

    public void SetLimits(Vector3 maxTile, float offset) {
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
        this.offset = offset;
    }
}
