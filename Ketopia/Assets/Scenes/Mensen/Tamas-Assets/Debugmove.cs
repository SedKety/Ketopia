using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Debugmove : MonoBehaviour
{

    public Vector3 moveDir;
    public Vector3 rotDir;
    public Vector3 camRotDir;

    public float speed = 5f;
    public float hor;
    public float vertical;

    public float mouseX;
    public float mouseY;

    public float sensitivity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        hor = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        moveDir.z = vertical;
        moveDir.x = hor;

        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        camRotDir.x = -mouseY;
        rotDir.y = mouseX;

        transform.Translate(moveDir);
        transform.Rotate(rotDir);
        GetComponentInChildren<Camera>().transform.Rotate(camRotDir);
    }
}
