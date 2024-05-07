using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Vector3 startingPosition;
    public Vector3 endPosition;
    public void Start()
    {
        startingPosition = transform.position;
        endPosition = startingPosition;
        endPosition.y -= 2;
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = endPosition;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = startingPosition;
        }
    }
}
