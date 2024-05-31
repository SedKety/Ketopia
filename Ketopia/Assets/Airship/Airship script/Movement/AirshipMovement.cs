using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    [Header("Movement")]
    public float airshipMovementSpeed;
    public float airshipMovementSpeedMultiplier;
    public float airshipRotationSpeed;
    public bool airshipMovementEnabled;
    public float airshipUpHeight;
    public float airshipUpHeightMultiplier;
    public float groundDrag;

    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    public static AirshipMovement instance;
    public AirShipPlayerParentScript airshipPlayerParentScript;
    public Transform propellor;
    public float propellorRotationSpeed;
    public Transform steeringWheel;

    private const float LiftForce = 9.81f;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        airshipMovementEnabled = false;
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (airshipMovementEnabled)
        {
            rb.mass = 50;
            MyInput();
            SpeedControl();
            HandleRotation();

            Vector3 movementDirection = orientation.forward * verticalInput + orientation.right * 0;
            Vector3 forceToAdd = movementDirection.normalized * airshipMovementSpeed * airshipMovementSpeedMultiplier * 10000f * Time.deltaTime;
            rb.AddForce(forceToAdd, ForceMode.Force);

            rb.AddForce(Vector3.up * (LiftForce + airshipUpHeight * airshipUpHeightMultiplier * 1000), ForceMode.Force);

            propellor.Rotate(0, 0, verticalInput * propellorRotationSpeed * airshipMovementSpeedMultiplier);
            rb.drag = groundDrag;
        }
        else
        {
            rb.mass = 100000;
            rb.drag = 100000;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > airshipMovementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * airshipMovementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void HandleRotation()
    {
        transform.Rotate(0, horizontalInput * airshipRotationSpeed, 0);
        float rotation = horizontalInput * airshipRotationSpeed;
        steeringWheel.Rotate(0, 0, rotation * -100);
    }

    private void MyInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        airshipUpHeight = Input.GetAxis("AirshipHorizontal");
    }

    public void EnableMovement()
    {
        airshipMovementEnabled = true;
        AirshipManager.instance.SwitchState(AirshipState.enabled);
        airshipPlayerParentScript.DisableGravity();
    }

    public void DisableMovement()
    {
        airshipMovementEnabled = false;
        AirshipManager.instance.SwitchState(AirshipState.disabled);
        airshipPlayerParentScript.EnableGravity();
    }
}