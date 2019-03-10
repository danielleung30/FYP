using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowActive : MonoBehaviour
{
    public GameObject mypanel;
    // Start is called before the first frame update
    void Start()
    {
        mypanel.SetActive(false);
    }

    // Update is called once per frame
    public void switchHelp(GameObject mypanel)
    {
        if (mypanel.activeSelf)
        {
            mypanel.SetActive(false);
        }
        else
        {
            mypanel.SetActive(true);
        }
    }
}
