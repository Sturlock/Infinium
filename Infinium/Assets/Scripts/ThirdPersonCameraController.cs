using System;
using System.Collections;
using System.Collections.Generic;
using Infinium.Dialogue;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public bool lockCursor;

    public float mouseSensitivty = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    PlayerConversant playerConversant;
    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
    float yaw;
    float pitch;
    private void Start()
    {
        playerConversant = GameObject.FindObjectOfType<PlayerConversant>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void LateUpdate()
    {
        if(!playerConversant.IsConversing())
        {
            
            if (Input.GetButton("Fire2"))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                yaw += Input.GetAxis("Mouse X") * mouseSensitivty;
                pitch -= Input.GetAxis("Mouse Y") * mouseSensitivty;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                currentRotation = Vector3.SmoothDamp(currentRotation,
                    new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
                transform.eulerAngles = currentRotation;
                transform.position = target.position - transform.forward * dstFromTarget;
                return;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            transform.eulerAngles = currentRotation;
            transform.position = target.position - transform.forward * dstFromTarget;
        } 
    }
}
