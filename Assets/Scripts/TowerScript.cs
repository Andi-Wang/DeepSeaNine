using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour {

    public Transform currentEnemy;
    public float currentEnemyDistance;
    private float turnSpeed = 5.0f;
    private float projectileSpeed = 15f;
    private float counter = 0;

    [SerializeField]
    private GameObject tower_projectile;
 
    void Start() {
        currentEnemyDistance = 100f;
    }

    void Update() {
        if (currentEnemy) { // enemy alive and at sight: aim at him!
            Vector3 vectorToTarget = currentEnemy.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);

            counter += Time.deltaTime;
            if (counter >= 1) {
                counter = 0;
                GameObject bullet = Instantiate(tower_projectile, transform.position, transform.rotation) as GameObject;
                Vector3 directions = new Vector3(Mathf.Cos(transform.eulerAngles.z*Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z*Mathf.Deg2Rad), 0).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = directions * projectileSpeed;
            }
        }
        else {
            // no enemy or enemy dead: find the nearest
            // victim and assign it to currentEnemy
            currentEnemyDistance = 100f;
        }
    }
}
