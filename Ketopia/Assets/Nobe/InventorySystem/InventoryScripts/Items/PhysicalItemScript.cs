using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItemScript : MonoBehaviour, IInteractable
{
    public Item item;
    public int quantity;
    public void Start()
    {
        if (quantity <=  0)
        {
            Destroy(gameObject);
        }
    }
    public void IInteractable()
    {
        int leftOverItems = InventoryManager.instance.OnItemAdd(item, quantity);
        if (leftOverItems <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            quantity = leftOverItems;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = null;
        }
    }

}
