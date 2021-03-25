using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    
    [SerializeField] GameObject cam;
    [SerializeField] GameObject playerTarget;
    [SerializeField] GameObject shipTarget;
    [SerializeField] LayerMask playerLayer; 
    public bool near;
    [SerializeField] bool controllingShip = false;
    [SerializeField] float radius = 4;
    // Start is called before the first frame update
    void OnDrawGizmos()
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
        playerLayer = LayerMask.GetMask("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        ShipController SC = GameObject.FindObjectOfType<ShipController>();
        bool noSail = SC.GetNoSail();
        near = Physics.CheckSphere(shipTarget.transform.position, radius, playerLayer);
        if(!controllingShip && (near && Input.GetKeyDown(KeyCode.E)))
        {
            controllingShip = true;
            return;
        }
        if (noSail && (controllingShip && Input.GetKeyDown(KeyCode.E)))
        {
            controllingShip = false;
            return;
        }
    }
}
