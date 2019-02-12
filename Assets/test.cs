using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Xml;

public class test : MonoBehaviour {
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private float fixLat;
    private float fixLon;
    private int mapScale;
    private float scaleFactor;
    public Text text;
    public Transform gameobject;
    private VideoPlayer videoPlayer;
    Quaternion rotation;
    Boolean pause;
    Vector3   startPos;
        List<Vector3> tartgetPos= new List<Vector3>();
        

        int PosCounter = 1;
        Vector3   pos;
    Vector3 oldpos;
        public float travelTime;
        float timer;
    float pp;
    Vector3 relativePos;
    Vector3 temppos;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Start() {
        // gmaeObj.transform.position = new Vector3(lonToX(114.253012),0,latToZ(22.325068));
        pause = false;
        tartgetPos = xmlReader("Assets/SBDIV_Test.xml");
        startPos = tartgetPos[0];   
        pos = tartgetPos[PosCounter];
        timer = 0.0f;
        oldpos = startPos;
        
        }
        void Update() {


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
            transform.rotation = Quaternion.LookRotation(tartgetPos[PosCounter + 5]) * Quaternion.Euler(0.0f, -80, 0.0f);
        }


    }
     
        public List<Vector3> xmlReader(string xmlFileName){
                Debug.Log("Open xml");
                XmlDocument doc = new XmlDocument();
		doc.Load(xmlFileName);
                Debug.Log("ok");
                double lat;
                double lon=0;

                List<Vector3>pos = new List<Vector3>();
                foreach(XmlNode node in doc.DocumentElement.ChildNodes){
                        ;
                        
                        foreach(XmlNode trkpt in node){
                        Debug.Log(trkpt.Name);
                        if(trkpt.Name=="trkpt"){
                    

                        
                    
                        lat = double.Parse(trkpt.Attributes["lat"].Value) ;
                        Debug.Log(lat);
                        lon = double.Parse(trkpt.Attributes["lon"].Value);
                    
                        pos.Add(new Vector3(lonToX(lon),50.0f,latToZ(lat)));
                        
                }
                        }
                        
                }

                return pos;
             
        }

        float latToZ (double _lat){
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        fixLat = gps.fixLat;
        
        mapScale = gps.mapScale;
        initZ = (float)(System.Math.Log(System.Math.Tan((90 + fixLat) * System.Math.PI / 360)) / (System.Math.PI / 180));
        initZ = initZ * 20037508.34f / (180 * mapScale);

        double z = (Math.Log(Math.Tan((90 + _lat) * Math.PI / 360)) / (Math.PI / 180));
        z = (z * 20037508.34 / 180) / mapScale - initZ;

        return (float)z;
        }

        float lonToX (double _lon){
        gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        
        fixLon = gps.fixLon;
        mapScale = gps.mapScale;
        initX = fixLon * 20037508.34f / (180 * mapScale);
        

        double x = (_lon * 20037508.34 / 180) / mapScale - initX;

        return (float)x;
        }

        public void playPause()
    {

        if (videoPlayer.isPlaying) {

            videoPlayer.Pause();
            pause = true;
            text.text = "Play";

        }
        else {

            videoPlayer.Play();
            pause = false;
            text.text = "Stop";

        }


    }
    public void shoot(GameObject obj)
    {
        RaycastHit hit;
        Vector3 forward = obj.transform.TransformDirection(Vector3.forward) * 10;
        if (Physics.Raycast(obj.transform.position, (forward), out hit))
        {
            hit.collider.gameObject.SetActive(false);
        }
    }



}
