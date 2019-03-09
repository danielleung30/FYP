using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Instantiate_Obj : MonoBehaviour
{
    public GameObject Instantiate_Position;
    public GameObject SpawnMarker;    

    void Start()
    {
        StartCoroutine(getUnityWebRequest());
    }
    
    void Update()
    {

    }

	IEnumerator getUnityWebRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://steveeasyjobs-fyp-sbdiv.herokuapp.com/api/markers"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {                    
                    string jsonResult =  System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);                        
                    Debug.Log(jsonResult);                      
                    RootObject[] markers = JsonHelper.getJsonArray<RootObject>(jsonResult);
                    Debug.Log(markers.Length);
                    foreach (var marker in markers){
                        //SpawnMarker.GetComponent<TextMesh>().text = marker.TITLE;
                        SpawnMarker.GetComponentInChildren<TextMesh>().text = marker.TITLE;
                        float longitude = float.Parse(marker.LONGITUDE);
                        float latitude = float.Parse(marker.LATITUDE);                       
                        Debug.Log(longitude+"//"+latitude);
                        //Vector3 pos = new Vector3(600,200,500);                        
                        Instantiate(SpawnMarker, WGS84toWebMercator(longitude, latitude, 150), Instantiate_Position.transform.rotation);                        
                        // Debug.Log(marker.TITLE);
                        // Debug.Log(marker.LATITUDE);
                        // Debug.Log(marker.LONGITUDE);
                    } Destroy(SpawnMarker);   
                                              
                }
            }
        }
    }    
    
    Vector3 WGS84toWebMercator(float _lon, float _lat, float _height)
    {
        var gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        float fixLat = gps.fixLat; 
        float fixLon = gps.fixLon;
        int mapScale = gps.mapScale;

        float initX = fixLon * 20037508.34f / (180*mapScale);
        float initZ = (float) (System.Math.Log(System.Math.Tan((90 + fixLat) * System.Math.PI / 360)) / (System.Math.PI / 180));
        initZ = initZ * 20037508.34f / (180*mapScale);

        double x = (_lon * 20037508.34 / 180) / mapScale - initX;
        double z = (Math.Log(Math.Tan((90 + _lat) * Math.PI / 360)) / (Math.PI / 180));
        z = (z * 20037508.34 / 180) / mapScale - initZ;
        float y = _height/mapScale;
        return new Vector3((float)x, y, (float)z);
    }

}
