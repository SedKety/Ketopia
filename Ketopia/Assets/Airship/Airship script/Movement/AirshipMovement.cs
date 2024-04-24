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

            var movementValue = Input.GetAxis("Vertical");
            movementValue = movementValue * airshipMovementSpeed * airshipMovementSpeedMultiplier;

            transform.Translate(0, 0, movementValue);
            //rb.velocity = new Vector3(0f, 0f, movementValue);
        }
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
