using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkDis : MonoBehaviour
{
    public int distance;
    public GameObject player;
    Component meshRender;
  
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
       
        pos = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, pos)<distance) {

            GetComponent<MeshRenderer>().enabled = true;
            transform.LookAt(player.transform);
            //transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);
        }
        else {

            GetComponent<MeshRenderer>().enabled = false;
           
        }
    }
}
