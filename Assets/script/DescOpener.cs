using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescOpener : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }

    public void EnableAll1()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnMarkers");
        foreach (GameObject spawn in spawns)
        {
            //bool isActive = spawn.activeSelf;
            spawn.SetActive(false);
        }
    }

    public void EnableAll2()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnMarkers");
        foreach (GameObject spawn in spawns)
        {
            //bool isActive = spawn.activeSelf;
            //spawn.SetActive(false);
            spawn.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
