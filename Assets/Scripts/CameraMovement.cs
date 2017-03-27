using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    //[SerializeField]
    //private float cameraSpeed = 5;

    private float xMax;
    private float yMin;
    private float offset;

    private List<GameObject> players = new List<GameObject>();       //Public variable to store a reference to the player game object

    private float counter;
    private int activePlayer; 

    void Start() {
        activePlayer = 0;
        counter = 0;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate() {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = players[activePlayer].transform.position;
        //Debug.Log(players[activePlayer]);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0 - offset, xMax - offset), Mathf.Clamp(transform.position.y, yMin + offset, 0 + offset), -10);
    }
    void FixedUpdate() {
        counter += Time.deltaTime;
        if(counter >= 20) {
            counter = 0;
            activePlayer = (activePlayer + 1) % players.Count;
            //Debug.Log(activePlayer);
            //transform.Translate(players[activePlayer].transform.position * cameraSpeed * Time.deltaTime);
        }
    }
    public void SetLimits(Vector3 maxTile, float offset){
        Vector3 wp = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - wp.x;
        yMin = maxTile.y - wp.y;
        this.offset = offset;
    }
    public void addPlayer(GameObject player) {
        players.Add(player);
    }
}
