﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void hovered()
    {
        GetComponent<TextMesh>().text = name;
    }

    public void exitHovered()
    {
        GetComponent<TextMesh>().text = "";


    }
}
