using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class testCheckDis : MonoBehaviour
{
    public int distance;
    public GameObject player;
    public RectTransform _rectTransform;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        pos = _rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(pos);
        // Debug.Log(player.transform.position);
        // Debug.Log(Vector3.Distance(player.transform.position, pos));
        if (Vector3.Distance(player.transform.position, pos) < distance)
        {

            //transform.LookAt(player.transform);
            GetComponentInChildren<MeshRenderer>().enabled = true;
            _rectTransform.rotation = Quaternion.LookRotation(_rectTransform.position - player.transform.position);
        }
        else
        {

            //GetComponent<MeshRenderer>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;

        }
    }
}
