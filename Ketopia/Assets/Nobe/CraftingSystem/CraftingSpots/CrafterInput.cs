using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterInput : MonoBehaviour
{
    public PhysicalItemScript currentItem;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PhysicalItemScript>() != null & currentItem == null)
        {
            currentItem = other.gameObject.GetComponent<PhysicalItemScript>();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<PhysicalItemScript>() == currentItem)
        {
            currentItem = null;
        }
    }
}
