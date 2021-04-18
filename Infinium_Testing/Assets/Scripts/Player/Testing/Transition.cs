using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    
    [SerializeField] GameObject cam;

    [Header("Target Settings", order = 0)]
    [SerializeField] GameObject shipTarget;
    [SerializeField] GameObject dockTarget;
    [SerializeField] LayerMask playerLayer;

    [Header("Docking Settings", order = 1)]
    [SerializeField] float startTime;
    [SerializeField] float dockingSpeed;
    [SerializeField] float journeyLength;
    [SerializeField] Vector3 startMarker;
    [SerializeField] Vector3 endMartker;
    [SerializeField] Quaternion startRotation;
    [SerializeField] Quaternion endRotation;

    public bool docking;

    [Header("Ship Interaction", order = 2)]
    [SerializeField] ShipController SC;
    [SerializeField] bool noSail = false;
    [SerializeField] bool sailOne, sailTwo, sailTravel;

    public bool near;
    [SerializeField] bool controllingShip = false;
    [SerializeField] float radius = 4;
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shipTarget.transform.position, radius);
    }
    public bool GetShip()
    {
        return controllingShip;
    }
    public void isShip(bool newIsShip)
    {
        controllingShip = newIsShip;
    }
    void Start()
    {
        SC = FindObjectOfType<ShipController>();
        
        playerLayer = LayerMask.GetMask("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        noSail = SC.GetNoSail();
        sailOne = SC.GetSailOne();
        sailTwo = SC.GetSailTwo();
        sailTravel = SC.GetSailTravel();
        float distCoverd = (Time.time - startTime) * dockingSpeed;
        //float distCoverd = Vector3.Distance(SC.transform.position, startMarker);
        float fractionOfJourney = distCoverd / journeyLength;

        near = Physics.CheckSphere(shipTarget.transform.position, radius, playerLayer);
        if(!controllingShip && (near && Input.GetKeyDown(KeyCode.E)))
        {
            docking = false;
            controllingShip = true;
            return;
        }
        if (noSail && (controllingShip && Input.GetKeyDown(KeyCode.E)))
        {
            controllingShip = false;
            return;
        }
        
        Test ts = GameObject.FindObjectOfType<Test>();
        bool inRadius = ts.GetInRange();

        Debug.Log(inRadius);

        if (inRadius && (controllingShip && Input.GetKeyDown(KeyCode.E)))
        {
            DockDistance();
            DockRotation();
            docking = true;
            noSail = true;
            SC.SetNoSail(noSail);
            sailOne = false;
            SC.SetSailOne(sailOne);
            sailTwo = false;
            SC.SetSailTwo(sailTwo);
            sailTravel = false;
            SC.SetSailTravel(sailTravel);
            
            return;
        }
        if (docking)
        {
            Debug.Log(fractionOfJourney);
            SC.transform.position = Vector3.Lerp(startMarker, endMartker, fractionOfJourney);
            SC.transform.rotation = Quaternion.Slerp(startRotation, endRotation, fractionOfJourney);
            if (SC.gameObject.transform.position == endMartker)
            {
                controllingShip = false;
            }
        }
    }

    private void DockRotation()
    {
        startRotation = SC.transform.rotation;
        endRotation = dockTarget.transform.rotation;
        Debug.Log(endRotation);
        
    }

    private void DockDistance()
    {
        Vector3 plus = new Vector3(-8, 0, 0);
        startMarker = SC.transform.position;
        endMartker = dockTarget.transform.position + plus;
        journeyLength = Vector3.Distance(startMarker, endMartker);
    }
}
