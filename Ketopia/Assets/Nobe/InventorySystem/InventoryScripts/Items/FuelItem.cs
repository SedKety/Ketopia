using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelItem : Item
{
    public int fuelAmount;
    public override void OnItemUse(GameObject objectToInteract)
    {
        objectToInteract.GetComponent<AirshipManager>().currentFuel += fuelAmount * quantity;
    }
}
