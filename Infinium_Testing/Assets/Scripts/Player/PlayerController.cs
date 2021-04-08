using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool controlling;
    [SerializeField] Transition transition;
    GameObject cam;
    //CharacterController cc;
    Rigidbody rb;
    
    [SerializeField] Vector2 input;
    [SerializeField] Vector3 movement;

    public float walkSpeed = 2;
    public float runSpeed = 6;
    float targetSpeed;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    
    Animator animator;
    Transform cameraT;
    [Range (0,1f)]
    public float disToGround;
    public LayerMask layer;

    RaycastHit hit;
    Ray ray;
    Vector3 footPosition;
    Vector3 forward;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hit.point, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ray.origin, 0.1f);
    }

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
    }
    void Update()
    {
        controlling = transition.GetShip();
        if (!controlling) 
        {
            cam.GetComponent<ThirdPersonCameraController>().target = GameObject.FindGameObjectWithTag("Target").transform;
            cam.GetComponent<ThirdPersonCameraController>().dstFromTarget = 4f;
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = LookRotation();

            bool running = Input.GetKey(KeyCode.LeftShift);
            targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

            float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }
    }

    private Vector2 LookRotation()
    {
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, 
                ref turnSmoothVelocity, turnSmoothTime);
            //Debug.Log(targetRotation);
        }

        return inputDir;
    }

    void FixedUpdate()
    {
        //Moving the Player around
        movement = Vector3.zero;
        movement = input * targetSpeed * Time.fixedDeltaTime ;
        rb.AddForce(movement);
        //cc.Move(input * Time.fixedDeltaTime * targetSpeed );

        Vector3 v = rb.velocity;
        v.x = Mathf.Clamp(rb.velocity.x, -6f, 6f);
        rb.velocity = v;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat("IKLeftFootWeight"));
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat("IKRightFootWeight"));
          
            //Left Foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up * 0.2f, Vector3.down * 0.4f);
            if (Physics.Raycast(ray, out hit, disToGround + 1f))
            {
                if (hit.transform.tag == "Walkable")
                {
                    footPosition = hit.point;
                    footPosition.y += disToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(forward, hit.normal));
                }
            }

            //Right Foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up * 0.2f, Vector3.down * 0.4f);
            if (Physics.Raycast(ray, out hit, disToGround + 1f))
            {
                if (hit.transform.tag == "Walkable")
                {
                    footPosition = hit.point;
                    footPosition.y += disToGround;
                    animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(forward, hit.normal));

                }
            }
        }
    }
}
