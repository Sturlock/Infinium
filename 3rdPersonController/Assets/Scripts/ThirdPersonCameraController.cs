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

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    
    float yaw;
    float pitch;
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    void LateUpdate()
    {
        PlayerConversant playerConversant = GameObject.FindObjectOfType<PlayerConversant>();
        if(!playerConversant.IsConversing())
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivty;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivty;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

            currentRotation = Vector3.SmoothDamp(currentRotation,
                new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

            transform.eulerAngles = currentRotation;
            transform.position = target.position - transform.forward * dstFromTarget;
            if (Input.GetButton("Fire2"))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                return;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return;
            }
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        
    }
}
