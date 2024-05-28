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

    [Header("Misc")]
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public Animator animator;
    //public Animator animator;
    private void Start()
    {
        canMove = true;
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponent<Animator>();
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
        animator.SetFloat("Movement", verticalInput);
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetButton("Jump") && onGround && canMove)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
            {
                float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

                if (slopeAngle < 80f)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    onGround = false;
                }
            }
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

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
    public void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Stairs"))
        {
            rb.velocity = new Vector3(0, verticalInput, 0);
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Stairs"))
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
