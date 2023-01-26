using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //commands          line
#pragma warning disable 96 // disabeling no clothes warninigs
#pragma warning disable 97
#pragma warning disable 98
#pragma warning disable 99

    [Header("Movement")]
    [SerializeField] private float moveSpeed; 

    [SerializeField] private float groundDrag; 

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    public LayerMask ground;
    bool grounded;

    [SerializeField] private Transform player;
    private float horizontalInput;
    private float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Step Handling")]
    [SerializeField] private GameObject stepRayUpper;
    [SerializeField] private GameObject stepRayLower;
    [SerializeField] private GameObject spawnItemLocation;
    [SerializeField] private float stepHight = 0.3f;
    [SerializeField] private float stepSmooth = 0.1f;

    [Header("Animation")]
    public List<Animator> animators = new List<Animator>();

    [Header("Sprite renderers")]
    public List<SpriteRenderer> renderers = new List<SpriteRenderer>();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation= true;
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHight, stepRayUpper.transform.position.z);

    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,ground);
        ControlInput();
        SpeedControl();
    }
    private void FixedUpdate()
    {
        MovePlayer();
        JumpDragHandle();
    }
    private void ControlInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jumping
        //Debug.Log($"jump key {jumpKey}, ready to jump {readyToJump}, grounded {grounded}");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
            rb.drag = 0f;
        }

        //flipping
        foreach (SpriteRenderer sprite in renderers)
        {
            if (!sprite.flipX && horizontalInput < 0)
            {
                sprite.flipX = true;
                spawnItemLocation.transform.position = new Vector3(transform.position.x -0.2f, transform.position.y, transform.position.z);
            }
            else if(sprite.flipX && horizontalInput >0) 
            {
                sprite.flipX = false;
                spawnItemLocation.transform.position = new Vector3(transform.position.x+0.2f, transform.position.y, transform.position.z);
            }
        }
    }
    private void MovePlayer()
    {
        SurfaceAlignment();
        StepClimb();

        if (grounded)
        {
            moveDirection = player.forward * verticalInput + player.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            //animation walking
            foreach(Animator anim in animators)
            {
                if (verticalInput * horizontalInput == 0) anim.SetFloat("Speed", Mathf.Abs(verticalInput + horizontalInput));
                if (verticalInput + horizontalInput == 0) anim.SetFloat("Speed", Mathf.Abs(verticalInput * horizontalInput));
            }
            
        }

        //in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f*airMultiplier, ForceMode.Force);
        }

    }
    private void StepClimb()
    {
        RaycastHit hitLower;
        if(Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.right),out hitLower, 0.1f) ||
            Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.left), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if(!Physics.Raycast(stepRayUpper.transform.position,transform.TransformDirection(Vector3.right),out hitUpper, 0.2f) ||
                !Physics.Raycast(stepRayUpper.transform.position,transform.TransformDirection(Vector3.left),out hitUpper,0.2f))
            {
                rb.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info;
        if (Physics.Raycast(ray, out info,ground))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.FromToRotation(Vector3.up,info.normal),0.1f);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVel.x,rb.velocity.y,limitVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        foreach(Animator anim in animators)
        {
            anim.SetBool("IsJumping", true);
        }
        Invoke("StopJumpAnim", 0.3f);
    }
    private void StopJumpAnim()
    {
        foreach (Animator anim in animators)
        {
            anim.SetBool("IsJumping", false);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
        rb.drag = groundDrag;
    }
    private void JumpDragHandle()
    {
        if (!grounded) rb.drag = 0;
        if (grounded) rb.drag = groundDrag;
    }
    public void Fixing()
    {
        foreach (Animator anim in animators)
        {
            anim.SetBool("IsFixing", true);
        }
    }
    public void DoneFixing()
    {
        foreach (Animator anim in animators)
        {
            anim.SetBool("IsFixing", false);
        }
    }
}
