using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public bool canMove;
    public float moveSpeed;
    public float sprintSpeed;
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
    public float ladderSpeed;

    public bool isSprinting;
    public float lastWPressTime;
    public float doubleTapTime;

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
        if (canMove)
        {
            MovePlayer();
        }
    }

    private void Update()
    {
        MyInput();
        SpeedControl();

        if (onGround)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        CheckSprinting();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        animator.SetFloat("Movement", verticalInput + horizontalInput);

        if (Input.GetButtonDown("Jump") && onGround && canMove)
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

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        Vector3 targetVelocity = moveDirection.normalized * currentSpeed;
        targetVelocity.y = rb.velocity.y;

        if (horizontalInput == 0 && verticalInput == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = targetVelocity;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > (isSprinting ? sprintSpeed : moveSpeed))
        {
            Vector3 limitedVel = flatVel.normalized * (isSprinting ? sprintSpeed : moveSpeed);
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Stairs"))
        {
            rb.velocity = new Vector3(0, verticalInput * ladderSpeed, 0);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Stairs"))
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ResourceNodeSpawner>() != null)
        {
            var spawner = other.GetComponent<ResourceNodeSpawner>();
            if (spawner.spawnedObjects.Count <= 0)
            {
                spawner.SpawnObjects();
            }
            else
            {
                foreach (var obj in spawner.spawnedObjects)
                {
                    if (obj)
                    {
                        obj.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = null;
        }

        if (other.GetComponent<ResourceNodeSpawner>() != null)
        {
            foreach (var obj in other.GetComponent<ResourceNodeSpawner>().spawnedObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    private void CheckSprinting()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Time.time - lastWPressTime < doubleTapTime)
            {
                isSprinting = true;
            }
            lastWPressTime = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isSprinting = false;
        }
    }
}