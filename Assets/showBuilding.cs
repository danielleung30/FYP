using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showBuilding : MonoBehaviour
{

    GameObject[] building;
    bool visible = true;
    public Text text;


    public void showObject() {

        if (building == null)
        {
            building = GameObject.FindGameObjectsWithTag(name);


        }

        visible = !visible;
        text.text = (building.ToString());

        foreach (GameObject obj in building) {

           
            obj.SetActive(visible);

        }


    }
}
