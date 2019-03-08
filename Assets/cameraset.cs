using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraset : MonoBehaviour
{
    public Camera myCamera;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myCamera.farClipPlane = slider.value;
    }
   // public void setcam(float num)
   // {
        //myCamera.farClipPlane = num;
   // }
}
