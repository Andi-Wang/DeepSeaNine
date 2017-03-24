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
            this.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        }
        else
        {
            this.GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");
        }
    }
}
