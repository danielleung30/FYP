using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    private string marker = "marker";

  public void Pressed()
    {
        //MeshRenderer renderer = GetComponent<MeshRenderer>();
        //bool flip = !renderer.enabled;
        //renderer.enabled = flip;
        TextMesh text = GetComponent<TextMesh>();
        if (text.text == "marker")
        {
            text.text = text.name;
        }
        else 
        {
            text.text = marker;
        }

    }
}
