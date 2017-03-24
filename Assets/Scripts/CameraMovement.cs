using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float cameraSpeed = 0;

    private float xMax;
    private float yMin;
    private float offset;

    // Update is called once per frame
    private void Update() {

        GetInput();
	}

    private void GetInput(){

        if (Input.GetKey(KeyCode.W)){
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)){
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0 - offset, xMax - offset), Mathf.Clamp(transform.position.y, yMin + offset, 0 + offset),-10);
    }

    public void SetLimits(Vector3 maxTile, float offset){
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
        this.offset = offset;
    }
}
