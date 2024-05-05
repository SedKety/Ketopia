using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirShipPlayerParentScript : MonoBehaviour
{

    public void EnableGravity()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<PlayerManager>() == null)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }
    }
    public void DisableGravity()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PlayerManager>() == null)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }
}
