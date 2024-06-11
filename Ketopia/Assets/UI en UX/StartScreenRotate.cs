using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenRotate : MonoBehaviour
{
    public Transform target; 
    public float distance ; 
    public float rotationSpeed; 
    public float height; 
    private float horizontalAngle;
    void Start()
    {
        if (!target)
        {
            Debug.LogError("Target not set for ContinuousHorizontalCameraOrbit script.");
            enabled = false;
            return;
        }
        horizontalAngle = transform.eulerAngles.y;
    }
    void LateUpdate()
    {
        if (target)
        {
            horizontalAngle += rotationSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position + new Vector3(0, height, 0);
            transform.position = position;
            transform.LookAt(target.position);
        }
    }
}

