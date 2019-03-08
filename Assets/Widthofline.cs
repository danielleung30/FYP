using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widthofline : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth=0.1f;
        lineRenderer.endWidth = 0.1f;
}
}
