using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    public float airshipMovementSpeed;
    public float airshipMovementSpeedMultiplier;
    public float airshipRotationSpeed;


    public bool airshipMovementEnabled;

    public Rigidbody rb;

    public static AirshipMovement _instance;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        rb = GetComponent<Rigidbody>();
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

            transform.Translate(0, movementValue, 0);
        }
    }
}
