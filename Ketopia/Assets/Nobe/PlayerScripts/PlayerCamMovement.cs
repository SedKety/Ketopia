using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamMovement : MonoBehaviour
{
    [SerializeField] Transform cameraHolder;
    private float verticalLookRotation;
    public GameObject mainMenu;
    public float mouseSensitivity;
    public bool canILook;
    public float rotationSpeed;

    public bool shouldFollow;
    public Transform player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        canILook = true;
        cameraHolder = Camera.main.gameObject.transform;
    }

    void Update()
    {
        if (canILook)
        {
            Cursor.lockState = CursorLockMode.Locked;

            transform.Rotate(Input.GetAxis("Mouse X") * mouseSensitivity * Vector3.up * Time.deltaTime);
            verticalLookRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
            cameraHolder.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
        }
        else if (canILook == false & shouldFollow == false)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (shouldFollow)
        {
            RotateAndMoveTowardsTarget(player);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void RotateAndMoveTowardsTarget(Transform target)
    {
        Quaternion targetRotation = target.rotation;
        cameraHolder.rotation = Quaternion.Lerp(cameraHolder.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        cameraHolder.position = AirshipManager.instance.camHolderAirship.position;
    }
}