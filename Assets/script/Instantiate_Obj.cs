using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Instantiate_Obj : MonoBehaviour
{
    public GameObject Instantiate_Position;
    public GameObject SpawnMarker;
    public GameObject TextContainer;
    public bool done = false;
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
                String data = System.IO.File.ReadAllText(Application.persistentDataPath + "ObjectData.txt");
                RootObject[] markers = JsonHelper.getJsonArray<RootObject>(data);

                instantObj(markers);

                Destroy(SpawnMarker);
                done = true;
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);
                    System.IO.File.WriteAllText(Application.persistentDataPath + "ObjectData.txt", jsonResult);
                    String data =  System.IO.File.ReadAllText(Application.persistentDataPath + "ObjectData.txt");
                    RootObject[] markers = JsonHelper.getJsonArray<RootObject>(data);
                    Debug.Log(markers.Length);

                    instantObj(markers);

                    Destroy(SpawnMarker);
                    done = true;
                }
            }
        }
    }

    Vector3 WGS84toWebMercator(float _lon, float _lat, float _height)
    {
        var gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
        float fixLat = gps.fixLat;
        float fixLon = gps.fixLon;
       // Debug.Log("fixLat:" + fixLat);
       // Debug.Log("fixLon:" + fixLon);
        int mapScale = gps.mapScale;

        float initX = fixLon * 20037508.34f / (180 * mapScale);
        float initZ = (float)(System.Math.Log(System.Math.Tan((90 + fixLat) * System.Math.PI / 360)) / (System.Math.PI / 180));
        initZ = initZ * 20037508.34f / (180 * mapScale);

        double x = (_lon * 20037508.34 / 180) / mapScale - initX;
        double z = (Math.Log(Math.Tan((90 + _lat) * Math.PI / 360)) / (Math.PI / 180));
        z = (z * 20037508.34 / 180) / mapScale - initZ;
        float y = _height / mapScale;
        return new Vector3((float)x, y, (float)z);
    }



    void instantObj(RootObject[] markers) {

        foreach (var marker in markers)
        {
            //SpawnMarker.GetComponent<TextMesh>().text = marker.TITLE;
            SpawnMarker.GetComponentInChildren<TextMesh>().text = "";
            SpawnMarker.GetComponentInChildren<TextMesh>().name = marker.TITLE;
            SpawnMarker.name = marker.TITLE;
            SpawnMarker.tag = marker.TYPE;
            TextContainer.GetComponentInChildren<Text>().text = marker.DESCRIPTION;

            float longitude = float.Parse(marker.LONGITUDE);
            float latitude = float.Parse(marker.LATITUDE);
            Debug.Log(longitude + "//" + latitude);
            //Vector3 pos = new Vector3(600,200,500);                        
            Instantiate(SpawnMarker, WGS84toWebMercator(longitude, latitude, 150), Instantiate_Position.transform.rotation);
            // Debug.Log(marker.TITLE);
            // Debug.Log(marker.LATITUDE);
            // Debug.Log(marker.LONGITUDE);
        }


    }


}
