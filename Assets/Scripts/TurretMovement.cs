using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour {

    private Transform currentEnemy;
    
    float turnSpeed = 5.0f;
 
    void Update() {
        if (currentEnemy) { // enemy alive and at sight: aim at him!
            currentEnemy = GameObject.Find("center").GetComponent<Transform>();
            Vector3 vectorToTarget = currentEnemy.position - transform.position;
            Debug.Log("Hello");
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
        }
        else {
            // no enemy or enemy dead: find the nearest
            // victim and assign it to currentEnemy
        }
    }
}
