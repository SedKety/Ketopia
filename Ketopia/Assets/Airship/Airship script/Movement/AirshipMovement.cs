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
    public Vector3 airshipMovementDirection;
    public float groundDrag;
    public float upForce;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Rigidbody rb;

    public static AirshipMovement instance;
    public AirShipPlayerParentScript airshipPlayerParentScript;
    public Transform propellor;
    public float propellorRotationSpeed;
    public Transform steeringWheel;
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
            airshipMovementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(airshipMovementDirection.normalized * airshipMovementSpeed * 1000f * Time.deltaTime, ForceMode.Force);
            rb.AddForce(0, airshipUpHeight * airshipUpHeightMultiplier * 1000, 0, ForceMode.Force);
            //rb.MovePosition(rb.position += new Vector3(0, airshipUpHeight * 0.25f, 0));
            propellor.transform.Rotate(0, 0, verticalInput * propellorRotationSpeed);
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
            rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
        }
    }
    void HandleRotation()
    {
        var rotation = Input.GetAxis("Horizontal") * airshipRotationSpeed;
        var rotationValue = Quaternion.Euler(0, rotation, 0);
        steeringWheel.Rotate(0, 0, rotation * -100);
        rb.MoveRotation(rb.rotation * rotationValue);
    }
    private void MyInput()
    {
        verticalInput = Input.GetAxis("Vertical");
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