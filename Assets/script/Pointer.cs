using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    public Text GGtext;
    public float m_distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    private Transform m_currentOrigin = null;
    private GameObject m_CurrentObject = null;
    public void Awake()
    {

        PlayerEvents.onControllerSource += updateOrigin;
        PlayerEvents.OnTriggerDown += ProcessTriggerDown;
    }

    private void Start()
    {
        SetLineColor();
    }

    public void OnDestroy()
    {
        PlayerEvents.onControllerSource -= updateOrigin;
        PlayerEvents.OnTriggerDown -= ProcessTriggerDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = updatePointerStatus();
        GGtext.text = m_CurrentObject.name;
        if (OnPointerUpdate != null) {
            OnPointerUpdate(hitPoint, m_CurrentObject);
            }
    }

    private Vector3 UpdateLine() 
    {
        //Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        //Default end
        Vector3 endPosition = m_currentOrigin.position + (m_currentOrigin.forward * m_distance);

        //Check hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }
            
        //Set Position
        m_LineRenderer.SetPosition(0,m_currentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);



        return endPosition;
    }

    private void updateOrigin(OVRInput.Controller controller, GameObject controllerObject) 
    {
        //Set orgin of pointer

        m_currentOrigin = controllerObject.transform;

        //Is the laser visable?
        if(controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else {
            m_LineRenderer.enabled = true;
        }

    }

    private GameObject updatePointerStatus() 
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);


        // Check hit 
        if (hit.collider)
            return hit.collider.gameObject;

        //return
        return null;
    }

    private RaycastHit CreateRaycast(int layer) 
    {
        RaycastHit hit;

        Ray ray = new Ray(m_currentOrigin.position, m_currentOrigin.forward);
        Physics.Raycast(ray,out hit, m_distance,layer);
        
        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;
        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;
    }

    public void ProcessTriggerDown()
    {
        if (!m_CurrentObject)
            return;
        GGtext.text = m_CurrentObject.name;
        interactable interact = m_CurrentObject.GetComponent<interactable>();
        interact.Pressed(); 
    }

}
