using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class layerenable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowObject()
    {
        GetComponent<Camera>().cullingMask |= (1 << 10);
    }


    public void valueChanged(Toggle t)
    {
        if (t.isOn)
        {
            GetComponent<Camera>().cullingMask |= (1 << 10);
        }
        else
        {
            GetComponent<Camera>().cullingMask = ~(1 << 10);
        }
    }
    /*// Render everything *except* layer 14
camera.cullingMask = ~(1 << 14);
 
// Switch off layer 14, leave others as-is
camera.cullingMask = ~(1 << 14);
 
// Switch on layer 14, leave others as-is
camera.cullingMask |= (1 << 14);
 */

}