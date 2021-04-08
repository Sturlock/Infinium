using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    Rigidbody body, connectedBody, previousConnectedBody;
    [SerializeField, Range(-10f, 10f)]
    float maxGroundAngle = 0f;
    [SerializeField, Range(90f, 180f)]
    float maxClimbAngle = 140f;
    [SerializeField, Range(20f, 70f)]
    float maxStairsAngle = 50f;
    float minGroundDotProduct, minStairsDotProduct, minClimbDotProduct;

    Vector3 contactNormal, steepNormal, climbNormal;
    Vector3 connectionVelocity;
    
    int groundContactCount, steepContactCount, climbContactCount;

    void OnValidate()
    {
        minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);
        minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
        minClimbDotProduct = Mathf.Cos(maxClimbAngle * Mathf.Deg2Rad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearState()
    {
        groundContactCount = steepContactCount = climbContactCount = 0;
        contactNormal = steepNormal = climbNormal = Vector3.zero;
        connectionVelocity = Vector3.zero;
        previousConnectedBody = connectedBody;
        connectedBody = null;
    }
}
