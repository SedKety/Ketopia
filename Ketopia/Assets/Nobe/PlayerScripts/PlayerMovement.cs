using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public bool canMove;
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;

    [Header("Ground Check")]
    public bool onGround;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    //public Animator animator;
    private void Start()
    {
        canMove = true;
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        //animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if(canMove)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * moveSpeed * 1000f * Time.deltaTime, ForceMode.Force);
        }
    }
    private void Update()
    {
        MyInput();
        SpeedControl();

        if (onGround == true)
        {
            rb.drag = groundDrag;
        }

        if (onGround == false)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            rb.drag = 0;
        }
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //if (moveDirection != Vector3.zero)
        //{
        //    animator.SetBool("IsMoving", true);
        //}
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //}
        
        if (Input.GetButton("Jump") && onGround && canMove)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = null;
        }
    }
}
