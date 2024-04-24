using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    public int airshipMovementSpeed;
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
}
