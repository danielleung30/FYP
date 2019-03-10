using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FJK : MonoBehaviour
{
    public ParticleSystemRenderer pr;
    //public GameObject Obj;
    //  public float fogtemp = 0.0f;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        pr.material.SetColor("_TintColor", new Color(1, 1, 1, 0));


    }


    public void Update()
    {
        pr.material.SetColor("_TintColor", new Color(1, 1, 1, slider.value));
        
    }
}