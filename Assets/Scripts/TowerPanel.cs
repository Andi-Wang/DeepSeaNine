using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPanel : MonoBehaviour {

    [SerializeField]
    private TowerBtn[] towers;
    private int selection = 0;
    private int newIdx = 0;


    public void menuSelection(int index) {
        selection = index;
    }

    /*
    int menuSelection(TowerBtn[] towers, int selectedItem, string direction)
    {

        if (direction == "up")
        {
            if (selectedItem == 0)
            {
                selectedItem = towers.Length - 1;
            }
            else
            {
                selectedItem -= 1;
            }
        }

        if (direction == "down")
        {
            if (selectedItem == towers.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem += 1;
            }
        }

        return selectedItem;
    }*/

    // Use this for initialization
    void Start () {
        towers[selection].highlighted(true);
    }
	
	// Update is called once per frame
	void Update () {
        /*
		if (Input.GetKeyDown("up"))
        {
            newIdx = menuSelection(towers, selection, "up");
        }
        if (Input.GetKeyDown("down"))
        {
            newIdx = menuSelection(towers, selection, "down");
        }

        if (newIdx != selection)
        {
            Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selection);
            towers[selection].highlighted(false);
            selection = newIdx;
            towers[selection].highlighted(true);
        }

        if (Input.GetKeyDown("space"))
        {
            handleSelection();
        }*/
    }

    public void handleSelection()
    {
        /*
        GUI.FocusControl(towers[selection].name);

        switch (selection)
        {
            case 0:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selection);
                GameManager.Instance.PickTower(towers[selection]);
                break;
            case 1:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selection);
                GameManager.Instance.PickTower(towers[selection]);
                break;
            case 2:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selection);
                break;
            case 3:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selection);
                break;
            default:
                Debug.Log("None of the above selected..");
                break;
        }
        */

        GameManager.Instance.PickTower(towers[selection]);
    }
}
