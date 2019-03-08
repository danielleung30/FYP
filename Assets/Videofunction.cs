using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Videofunction : MonoBehaviour
{
    public double videotime;
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        videotime = videoPlayer.time;
    }



    public void Fastfoward()
    {
        videoPlayer.time = videoPlayer.time + 5;

    }


    public void Rewind()
    {
        videoPlayer.time = videoPlayer.time - 5;

    }



}