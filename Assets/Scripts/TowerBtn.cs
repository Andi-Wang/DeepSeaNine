using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour {

    [SerializeField]
    private GameObject towerPrefab;

    public GameObject TowerPrefab {
        get {
            return towerPrefab;
        }
    }

    public void highlighted(bool selected)
    {
        if (selected)
        {
            
        }
        else
        {
            
        }
    }
}
