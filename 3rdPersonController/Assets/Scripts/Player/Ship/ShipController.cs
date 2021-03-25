using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Transition transition;
    bool controlling;
    [Header ("Properties", order = 0)]
    public ThirdPersonCameraController tPCC;
    public List<GameObject> springs;
    public Rigidbody rb;
    public GameObject prop;
    public GameObject CM;
    [Header ("Ship Speeds", order = 1)]
    public float sailOneSpeed = 300f;
    public float sailTwoSpeed = 500f;
    public float travelSpeed = 800f;
    [SerializeField] float currentSpeed;
    Vector3 speed = Vector3.zero;
    [SerializeField] bool sailOne, sailTwo, sailTravel = false;
    [SerializeField] bool noSail = true;

    [Header ("Propultion Settings", order = 2)]
    public float turn = 7000f;
    public float disToGround = 17f;
    public float thrusterStrength = 500f;
    
    #region Local Getters
    public bool GetNoSail()
    {
        return noSail;
    }
    public bool GetSailOne()
    {
        return sailOne;
    }
    public bool GetSailTwo()
    {
        return sailTwo;
    }
    public bool GetSailTravel()
    {
        return sailTravel;
    }
    #endregion

    #region Local Setters
    public void SetNoSail(bool newNoSail)
    {
        noSail = newNoSail;
    }
    public void SetSailOne(bool newSailOne)
    {
        sailOne = newSailOne;
    }
    public void SetSailTwo(bool newSailTwo)
    {
        sailTwo = newSailTwo;
    }
    public void SetSailTravel(bool newSailTravel)
    {
        sailTravel = newSailTravel;
    }


    #endregion
    void Awake()
    {
        transition = GameObject.FindObjectOfType<Transition>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.centerOfMass = CM.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        controlling = transition.GetShip();
        if (controlling)
        {
            #region Gears
            tPCC.target = GameObject.FindGameObjectWithTag("Wheel").transform;
            //gearing the speed up
            if (Input.GetKeyDown(KeyCode.W) && noSail)
            {
                sailOne = true;
                noSail = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.W) && sailOne)
            {
                sailTwo = true;
                sailOne = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.W) && sailTwo)
            {
                sailTravel = true;
                sailTwo = false;
                return;
            }

            //gearing the speed down 
            if (Input.GetKeyDown(KeyCode.S) && sailOne)
            {
                noSail = true;
                sailOne = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.S) && sailTwo)
            {
                sailOne = true;
                sailTwo = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.S) && sailTravel)
            {
                sailTwo = true;
                sailTravel = false;
                return;
            }
            #endregion

            #region Camera Distance
            if (noSail)
                tPCC.dstFromTarget = 6f;
            if (sailOne)
                tPCC.dstFromTarget = 8f;
            if (sailTwo)
                tPCC.dstFromTarget = 15f;
            if (sailTravel)
                tPCC.dstFromTarget = 40f;
            #endregion
        }
        #region Gears Speeds
        //Making sure the ship doesnt float off
        if (noSail)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            currentSpeed = 0f;
        }
        //Setting Speed
        if (sailOne)
        {
            speed = Time.deltaTime * transform.TransformDirection(Vector3.forward) * sailOneSpeed;
            currentSpeed = sailOneSpeed;
        }
        if (sailTwo)
        {
            speed = Time.deltaTime * transform.TransformDirection(Vector3.forward) * sailTwoSpeed;
            currentSpeed = sailTwoSpeed;
        }
        if (sailTravel)
        {
            speed = Time.deltaTime * transform.TransformDirection(Vector3.forward) * travelSpeed;
            currentSpeed = travelSpeed;
        }
        #endregion
    }

    

    void FixedUpdate()
    {
        rb.AddForceAtPosition(speed, prop.transform.position);
        if (controlling) {rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * Input.GetAxis("Horizontal") * turn * 100f); }
        
        foreach (GameObject spring in springs)
        {
            //////////////// DO NOT CHANGE!!!!!!
            RaycastHit hit;
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit, disToGround))
            {
                rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(disToGround - hit.distance, 4) / disToGround * 90f, spring.transform.position);
            }
            //Debug.Log(hit.disToGround);
        }
        rb.AddForce(-Time.deltaTime * transform.TransformVector(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * (thrusterStrength * 10f));
    }
}
