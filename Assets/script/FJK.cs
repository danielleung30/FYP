using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FJK : MonoBehaviour
{
    public GameObject Obj;
    public float fogtemp = 0.0f;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer m = Obj.GetComponent<MeshRenderer>();
        m.material.SetColor("_TintColor", new Color(1, 1, 1, 0));

    }


    public void Update()
    {
        MeshRenderer m = Obj.GetComponent<MeshRenderer>();
        m.material.SetColor("_TintColor", new Color(1, 1, 1, slider.value)); 
    }
}