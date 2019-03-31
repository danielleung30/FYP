using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class speed : MonoBehaviour
{
    public VideoPlayer videoPlaye;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text= "Current Speed: "+videoPlaye.playbackSpeed.ToString()+" x";
    }
}
