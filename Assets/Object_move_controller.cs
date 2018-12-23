using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Xml;

public class Object_move_controller : MonoBehaviour {
    private float initX;
    private float initZ;
    private MapNav gps;
    private bool gpsFix;
    private float fixLat;
    private float fixLon;
    private int mapScale;
    private float scaleFactor;
    Vector3   startPos;
        List<Vector3> tartgetPos= new List<Vector3>();
        

        int PosCounter = 1;
        Vector3   pos;
        public float travelTime;
        float timer;

    

    void Start() {
       // gmaeObj.transform.position = new Vector3(lonToX(114.253012),0,latToZ(22.325068));
        tartgetPos = xmlReader("Assets/SBDIV_Test.xml");
        startPos = tartgetPos[0];   
        pos = tartgetPos[PosCounter];
        timer = 0.0f;

        }
        void Update() {
        timer += Time.deltaTime;
                if(transform.position==pos){
                        //Debug.Log(transform.position);
                        startPos = pos;
                        PosCounter++;
                        pos = tartgetPos[PosCounter];
                        timer = 0.0f;
                }
        transform.position = Vector3.Lerp(startPos,pos,timer/travelTime);
        

        
        }
     
        public List<Vector3> xmlReader(string xmlFileName){
                Debug.Log("Open xml");
                XmlDocument doc = new XmlDocument();
		doc.Load(xmlFileName);
                Debug.Log("ok");
                double lat;
                double lon;
        

                List<Vector3>pos = new List<Vector3>();
                foreach(XmlNode node in doc.DocumentElement.ChildNodes){
                        
                        
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

}
