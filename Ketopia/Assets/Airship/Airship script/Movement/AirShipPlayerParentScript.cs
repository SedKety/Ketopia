using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPlayerParentScript : MonoBehaviour
{
    void Update()
    {
        if(transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                child.position = transform.TransformPoint(child.localPosition);
            }
        }
    }
}
