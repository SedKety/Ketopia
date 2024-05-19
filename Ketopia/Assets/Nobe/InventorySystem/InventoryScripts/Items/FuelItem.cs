using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Fuel")]
public class FuelItem : Item
{
    public int fuelAmount;
    public override void BurnItem(float _fuelAmount, int quantityMultiplier)
    {
        _fuelAmount = fuelAmount;
        base.BurnItem(fuelAmount, quantityMultiplier);
    }
}
