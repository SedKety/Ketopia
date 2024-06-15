using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInputScript : MonoBehaviour
{
    public FurnaceScript furnace;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PhysicalItemScript>() != null)
        {
            Ores item = other.gameObject.GetComponent<PhysicalItemScript>().item as Ores;
            FuelItem fuel = other.gameObject.GetComponent<PhysicalItemScript>().item as FuelItem;
            if (item != null)
            {
                furnace.currentTouchingItem = other.gameObject;
                furnace.SmeltItem();
            }
            if(fuel != null)
            {
                furnace.fuel += fuel.fuelAmount;
                furnace.StartFuel();
                Destroy(other.gameObject);
            }
            
        }
    }
}
