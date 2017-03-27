using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject selectionBox;

    public GameObject TowerPrefab {
        get {
            return towerPrefab;
        }
    }

    public void highlighted(bool selected) {
        if (selected) {
            selectionBox.SetActive(true);
        }else {
            selectionBox.SetActive(false);
        }
    }
}
