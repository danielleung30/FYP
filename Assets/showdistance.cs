using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showdistance : MonoBehaviour
{
    public Slider slider;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString();
    }
}
