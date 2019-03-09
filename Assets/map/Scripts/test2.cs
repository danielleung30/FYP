﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Xml;
using UnityEngine.Video;
public class test2 : MonoBehaviour
{
    public float testY;
    public double videotime;
    public VideoPlayer videoPlayer;
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private float fixLat;
    private float fixLon;
    private int mapScale;
    private float scaleFactor;
    public Transform gameobject;
    public GameObject map;
    public MapNav mapNav;
    Quaternion rotation;
    Vector3 startPos;
    List<Vector3> tartgetPos = new List<Vector3>();

    int PosCounter = 1;
    Vector3 pos;
    Vector3 oldpos;
    public float travelTime;
    float timer;
    float pp;
    Vector3 relativePos;
    Vector3 temppos;
    TextAsset textAsset;
    Boolean pause;
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }


    void Start()
    {
        map = GameObject.Find("Map");
        mapNav = map.GetComponent<MapNav>();
        videoPlayer = GetComponent<VideoPlayer>();
        // gmaeObj.transform.position = new Vector3(lonToX(114.253012),0,latToZ(22.325068));
        tartgetPos = xmlReader((TextAsset)Resources.Load("SBDIV_Test", typeof(TextAsset)));
        startPos = tartgetPos[0];
        pos = tartgetPos[PosCounter];
        timer = 0.0f;
        oldpos = startPos;
        pause = false;
    }
    void Update()
    {
        videotime = videoPlayer.time; 

        if (pause == false)
        {
            timer += Time.deltaTime;
            if (transform.position == pos)
            {
                //Debug.Log(transform.position);
                startPos = pos;
                PosCounter++;

                pos = tartgetPos[PosCounter];
                timer = 0.0f;
            }
            transform.position = Vector3.Lerp(startPos, pos, timer / travelTime);
            transform.rotation = Quaternion.LookRotation(pos - tartgetPos[PosCounter + 5]);
        }


    }








    public List<Vector3> xmlReader(TextAsset xmlFileName)
    {
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(xmlFileName.text);
        Debug.Log("ok");
        double lat;
        double lon = 0;
        List<Vector3> pos = new List<Vector3>();
        foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes)
        {
            foreach (XmlNode trkpt in node)
            {
                if (trkpt.Name == "trkpt")
                {
                    lat = double.Parse(trkpt.Attributes["lat"].Value);
                    lon = double.Parse(trkpt.Attributes["lon"].Value);
                    pos.Add(new Vector3(lonToX(lon), 0, latToZ(lat)));
                }
            }

        }

        return pos;

    }

    float latToZ(double _lat)
    {
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        fixLat = gps.fixLat;

        mapScale = gps.mapScale;
        initZ = (float)(System.Math.Log(System.Math.Tan((90 + fixLat) * System.Math.PI / 360)) / (System.Math.PI / 180));
        initZ = initZ * 20037508.34f / (180 * mapScale);

        double z = (Math.Log(Math.Tan((90 + _lat) * Math.PI / 360)) / (Math.PI / 180));
        z = (z * 20037508.34 / 180) / mapScale - initZ;

        return (float)z;
    }

    float lonToX(double _lon)
    {
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();

        fixLon = gps.fixLon;
        mapScale = gps.mapScale;
        initX = fixLon * 20037508.34f / (180 * mapScale);


        double x = (_lon * 20037508.34 / 180) / mapScale - initX;

        return (float)x;
    }
    public void playPause()
    {

        if (videoPlayer.isPlaying)
        {

            videoPlayer.Pause();
            pause = true;


        }
        else
        {

            videoPlayer.Play();
            pause = false;


        }
    }
    public void Fastfoward()
    {
        videoPlayer.Pause();
        timer = timer + 5;
        pos = tartgetPos[PosCounter + 5];
        videoPlayer.time = videotime + 5;
        transform.position = Vector3.Lerp(startPos, pos, timer / travelTime);
        
    }


    public void Rewind()
    {
        videoPlayer.Pause();
        timer = timer - 5;
        pos =tartgetPos[PosCounter-5];
        videoPlayer.time = videotime - 5;
        transform.position = Vector3.Lerp(startPos, pos, timer / travelTime);
       
    }
}



    
