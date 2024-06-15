using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItemScript : MonoBehaviour, IInteractable
{
    public Item item;
    public int quantity;
    public virtual void Start()
    {
        if (quantity <= 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void UpdateQuantity(int amountToRemove)
    {
        quantity -= amountToRemove;
        if (quantity <= 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void IInteractable()
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
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = other.transform;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Airship"))
        {
            transform.parent = null;
        }
    }
}
