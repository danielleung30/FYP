using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerEvents : MonoBehaviour

{
    #region Events
    public static UnityAction OnTriggerUp = null;
    public static UnityAction OnTriggerDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> onControllerSource = null;
    #endregion

    #region Anchors
    public GameObject m_LeftAnchors;
    public GameObject m_RightAnchors;
    public GameObject m_HeadAnchors;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> m_ControllerSets = null;
    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;
    #endregion


    public void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        m_ControllerSets = CreateControllerSet();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }


    // Update is called once per frame
    void Update()
    {
        //Check for active input
        if (!m_InputActive)
            return;

        //Check if a controller exists
        CheckForController();

        //Check for input source
        CheckInputSource();

        //Check for actual input
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        //Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)) 
        {
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        }

        //Left remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
        {
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        }

        //Headset if no remote
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)&&
            !OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
        {
            controllerCheck = OVRInput.Controller.Touchpad;
        }

        //Update
        m_Controller = UpdateSource(controllerCheck,m_Controller);
    }

    private void CheckInputSource() 
    {
        //Update
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    private void Input()
    {
        //Touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) { 

            if(OnTriggerDown != null) {
                OnTriggerDown();
            }
        }

        //Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if(OnTriggerUp != null) {
                OnTriggerUp();
            }
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check,OVRInput.Controller previous)
    {   //if value is the same ,return
        if (check == previous) return previous;
        //Get controller obj
        GameObject controllerObject = null;
        m_ControllerSets.TryGetValue(check, out controllerObject);

        //If no controller ,set to head
        if (controllerObject == null) controllerObject = m_HeadAnchors;

        //send out event
        if (onControllerSource != null) onControllerSource(check, controllerObject);

        return check;
    }

    private void PlayerFound() 
    {
        m_InputActive = true;
    }

    private void PlayerLost() 
    {
        m_InputActive = false;
    }

    private Dictionary<OVRInput.Controller,GameObject> CreateControllerSet()
    {

        Dictionary<OVRInput.Controller, GameObject> newSet = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote,m_LeftAnchors},
            {OVRInput.Controller.RTrackedRemote,m_RightAnchors},
            {OVRInput.Controller.Touchpad,m_HeadAnchors}
        };

        return newSet;

    }

}
