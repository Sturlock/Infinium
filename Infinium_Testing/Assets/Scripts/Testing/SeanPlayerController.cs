using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeanPlayerController : MonoBehaviour
{
    public float baseSpeed = 5;
    private float sprintSpeed = 7.5f;
    private float moveSpeed;
    private float JumpHeight = 2;
    Vector2 pInput;

    public Camera mainCamera;

    [NonSerialized] public CharacterController pController;
    [NonSerialized] public Collider pCollider;
    //[NonSerialized] public Rigidbody pRigidBody;
    [NonSerialized] public Animator pAnimator;
    public Transform cameraPos;

    //HealthScript playerHealth;

    private Vector3 playerVelocity;
    public Vector3 hVelocity;
    public float forwardAxis;
    public float horizontalAngle;
    private float gravityValue = -20;
    [NonSerialized] public Vector3 Movement;
    Vector3 movementOffset = new Vector3();

    bool Jumping = false;
    public bool Grounded;
    public bool canMove = true;
    public static PlayerController PlayerInstance { get; private set; }

    float staminaMax = 10;
    public float staminaCurrent;
    float staminaRegenRate = 0.5f;
    float sprintStamDrain = 2;
    float blockStamDrain = 2;
    float blockDmgReduction = 0.2f;
    bool isSprinting = false;
    bool isTired = false;

    float lightAtkDmg = 5f;
    float heavyAtkDmg = 15f;
    float comboDmg = 1.2f;

    float timer = 5;
    float tiredTimer;

    public enum ComboState
    {
        isAttacking,
        isComboing
    }


    private void Awake()
    {
        //pRigidBody = GetComponent<Rigidbody>();
        pController = GetComponent<CharacterController>();
        pCollider = GetComponent<Collider>();
        pAnimator = GetComponent<Animator>();
        //playerHealth = GetComponent<HealthScript>();
        staminaCurrent = staminaMax;
        horizontalAngle = transform.localEulerAngles.y;
        tiredTimer = timer;

    }

    void Update()
    {
        if (canMove == false)
        {
            return;
        }

        MovementCode();
        JumpingCode();
        //CombatCode();

        if (!isSprinting && !isTired)
        {
            staminaCurrent = Mathf.Clamp(staminaCurrent + staminaRegenRate * Time.deltaTime, 0, staminaMax);
        }

        hVelocity = new Vector3(pController.velocity.x, 0, pController.velocity.z);
        if (isTired)
        {
            tiredCode();
        }
        else
        {
            tiredTimer = timer;
        }
    }

    private void MovementCode()
    {
        forwardAxis = Input.GetAxis("Vertical");
        Grounded = pController.isGrounded;

        pInput = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        if (staminaCurrent < (staminaMax / 5))
        {
            isTired = true;
        }

        if (Input.GetButton("Sprint") && staminaCurrent > 0.5f && pInput != Vector2.zero)
        {
            moveSpeed = sprintSpeed;
            Stamina(sprintStamDrain, true);
        }
        else
        {
            if (!isTired)
            {
                moveSpeed = baseSpeed;
            }
            isSprinting = false;
        }

        Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Movement = Movement * Time.deltaTime * moveSpeed;
        Movement = transform.TransformDirection(Movement);
        pController.Move(Movement);

        LookRotation();

        //float playerTurn = Input.GetAxis("Mouse X");
        //horizontalAngle = horizontalAngle + playerTurn;

        //if (horizontalAngle > 360)
        //{
        //    horizontalAngle -= 360.0f;
        //}
        //if (horizontalAngle < 0)
        //{
        //    horizontalAngle += 360.0f;
        //}

        //Vector3 currentDir = transform.localEulerAngles;
        //currentDir.y = horizontalAngle;
        //transform.localEulerAngles = currentDir;


        if (Grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        pController.Move(playerVelocity * Time.deltaTime);

    }

    private Vector2 LookRotation()
    {
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                ref turnSmoothVelocity, turnSmoothTime);
            Debug.Log(targetRotation);
        }

        return inputDir;
    }

    private void JumpingCode()
    {
        float jumpStamDrain = 1f;
        if (pController.isGrounded && Input.GetButtonDown("Jump") && staminaCurrent > 0.5f)
        {
            Stamina(jumpStamDrain, false);
            playerVelocity.y = Mathf.Sqrt(JumpHeight * -2 * gravityValue);

        }
    }

    public void Stamina(float drain, bool isDraining)
    {

        while (isDraining)
        {
            staminaCurrent -= drain * Time.deltaTime;

            isSprinting = true;
            return;
        }

        staminaCurrent -= drain;
        return;
    }

    //private void CombatCode()
    //{
    //    if (Input.GetButton("HeavyAtk")) 
    //    { 
        
    //    }
    

    //    if (Input.GetButton("LightAtk"))
    //    {

    //    }

    //    if (Input.GetButtonDown("Block"))
    //    {
    //        bool isBlocking = true;


    //        // make stamina drain be percentage of damage blocked
    //        if (GetComponent<HealthScript>().isDamaged == true && isBlocking == true)
    //        {
    //            Debug.Log("bub");
    //            Stamina(blockStamDrain, false);
    //        }
    //    }
        
    //}

    private void tiredCode()
    {
        moveSpeed = baseSpeed / 10;
        Mathf.Clamp(tiredTimer -= Time.deltaTime, 0, timer);
        if (tiredTimer <= 0)
        {
            moveSpeed = baseSpeed;
            isTired = false;
            staminaCurrent += staminaRegenRate;
        }
    }
   

    private void PlayNextcombo()
    {

    }

    private void ComboEnd()
    {
        //final attack gets multiplied by comboDmg
    }
}
