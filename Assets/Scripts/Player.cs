using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController _controller;
    
    private float x;
    private float z;

    public float gravity = -9.82f;
    public float jumpHeight = 3f;
    public float walkSpeed = 12f;
    public float runningSpeed;
    public float currentSpeed;
    private Vector3 velocity;
    private Vector3 move;
    
    //for checking ground 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private bool isGrounded;
    
    //for crouching
    private bool isCrouching;
    public float crouchSpeed= 4f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,groundDistance);
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        GetInput();
    }

    private void FixedUpdate()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isCrouching = true;
        }
        
        move = transform.right * x + transform.forward * z;
        _controller.Move(move * currentSpeed * Time.deltaTime);
        
        //for crouching
        if (!isGrounded)
        {
            isCrouching = false;
        }
        
        
        //to create gravity
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }
    
    void GetInput()
    {
        //for movement
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        
        
        //for checking if plaer is running or crouching and set the current speed
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ;
        bool crouch = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        currentSpeed = crouch ? crouchSpeed : isRunning ? runningSpeed : walkSpeed;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {    //for add jump force to player 
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftControl) && isCrouching)
        {
            startCrouch();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            stopCrouch();
        }
        void startCrouch()
        {
            _controller.height = .5f;
        
        }

        void stopCrouch()
        {
            _controller.height = 2f;
        }
    }

}
