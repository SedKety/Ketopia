using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    [Header("Movement/Rotation")]
    public float airshipMovementSpeed;
    public float airshipMovementSpeedMultiplier;
    public float airshipRotationSpeed;
    public bool airshipMovementEnabled;
    public Vector3 airshipMovementDirection;
    [Header("Misc")]
    public Rigidbody rb;
    public static AirshipMovement instance;
    public int despawnTimer;
    public bool shouldDespawn;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
        airshipMovementEnabled = false;
    }
    public void Update()
    {
        if (AirshipManager.instance.currentFuel <= 0)
        {
            airshipMovementEnabled = false;
        }
    }
    public void FixedUpdate()
    {
        if (airshipMovementEnabled)
        {
            var rotationValue = Input.GetAxis("Horizontal");
            rotationValue = rotationValue * airshipRotationSpeed;
            gameObject.transform.Rotate(0, rotationValue, 0);

            var zAxis = Input.GetAxis("Vertical");
            zAxis = zAxis * airshipMovementSpeed * airshipMovementSpeedMultiplier;
            var yAxis = Input.GetAxis("AirshipHorizontal");
            yAxis *= 0.25f;
            //rb.velocity = new Vector3(0f, 0f, movementValue);
            airshipMovementDirection = new Vector3(0, yAxis, zAxis);
        }
        else if (!airshipMovementEnabled)
        {
            airshipMovementDirection = Vector3.zero;
        }
        transform.Translate(airshipMovementDirection);

    }
    public void EnableMovement()
    {
        airshipMovementEnabled = true;
        StartCoroutine(AirshipManager.instance.FuelConsumption());
    }
    public void DisableMovement()
    {
        airshipMovementEnabled = false;
        StopCoroutine(AirshipManager.instance.FuelConsumption());
    }
}
