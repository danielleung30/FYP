using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper 
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[System.Serializable]
public class RootObject
{
    public string _id;
    public string TITLE;
    public string LATITUDE;
    public string LONGITUDE;
    public string DESCRIPTION;
    public string VIDEO_ID;
}

