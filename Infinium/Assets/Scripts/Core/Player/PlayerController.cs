using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Infinium.Saving;
using Infinium.Resources;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, ISaveable
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
    bool running;
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

    public float IKUP = .2f;
    public float IKDOWN = .4f;
    Stamina stamina;
    Health health;

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
        stamina = GetComponent<Stamina>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraT = Camera.main.transform;
        
    }
    void Update()
    {
        controlling = transition.GetShip();
        if (!controlling)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            rb.useGravity = true;

            
            cam.GetComponent<ThirdPersonCameraController>().target = GameObject.FindGameObjectWithTag("Target").transform;
            cam.GetComponent<ThirdPersonCameraController>().dstFromTarget = 4f;
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = LookRotation();

            running = Input.GetKey(KeyCode.LeftShift);
            print("Running is " + running);
            
            targetSpeed = ((running && stamina.GetStamina() > 0) ? runSpeed : walkSpeed) * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

            float animationSpeedPercent = ((running && stamina.GetStamina() > 0) ? 1 : .5f) * inputDir.magnitude;
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
            
        }
        else animator.SetFloat("speedPercent", 0f);
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
        if (running && targetSpeed == runSpeed)
        {
            stamina.UseStamina(running, 0.5f);
        }
        if (!running)
        {
            stamina.regen = true;
            StartCoroutine(stamina.RegenStamina());
        }

        running = Input.GetKey(KeyCode.LeftShift);

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
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up * IKUP, Vector3.down * IKDOWN);
            if (Physics.Raycast(ray, out hit, disToGround + 1f))
            {
                if (hit.transform.tag == "Walkable" || hit.transform.tag == "Ship")
                {
                    footPosition = hit.point;
                    footPosition.y += disToGround;
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(forward, hit.normal));
                    
                }
                Debug.Log(hit.transform.name);
            }

            //Right Foot
            ray = new Ray(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up * IKUP, Vector3.down * IKDOWN);
            if (Physics.Raycast(ray, out hit, disToGround + 1f))
            {
                if (hit.transform.tag == "Walkable" || hit.transform.tag == "Ship")
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

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        
        transform.position = position.ToVector();
        
    }
}
