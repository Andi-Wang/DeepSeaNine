using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPanel : MonoBehaviour {

    [SerializeField]
    private TowerBtn[] towers;
    private int selectedIdx = 0;
    private int newIdx = 0;

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
    }

    // Use this for initialization
    void Start () {
        towers[selectedIdx].highlighted(true);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("up"))
        {
            newIdx = menuSelection(towers, selectedIdx, "up");
        }
        if (Input.GetKeyDown("down"))
        {
            newIdx = menuSelection(towers, selectedIdx, "down");
        }

        if (newIdx != selectedIdx)
        {
            Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selectedIdx);
            towers[selectedIdx].highlighted(false);
            selectedIdx = newIdx;
            towers[selectedIdx].highlighted(true);
        }

        if (Input.GetKeyDown("space"))
        {
            handleSelection();
        }
    }

    void handleSelection()
    {
        GUI.FocusControl(towers[selectedIdx].name);

        switch (selectedIdx)
        {
            case 0:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selectedIdx);
                break;
            case 1:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selectedIdx);
                break;
            case 2:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selectedIdx);
                break;
            case 3:
                Debug.Log("Selected name: " + GUI.GetNameOfFocusedControl() + " / id: " + selectedIdx);
                break;
            default:
                Debug.Log("None of the above selected..");
                break;
        }
    }
}
