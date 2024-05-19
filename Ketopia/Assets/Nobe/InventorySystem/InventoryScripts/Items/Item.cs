using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    resource,
    consumable,
    fuel,
    holdable,
}
public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public GameObject physicalItem;
    public Sprite itemSprite;
    public float itemId;
    public ItemType itemType;
    public int maxQuantity;

    public virtual void OnItemUse()
    {

    }

    public virtual void BurnItem(float _fuelAmount, int quantityMultiplier)
    {
        AirshipManager.instance.currentFuel += _fuelAmount * quantityMultiplier;
        Debug.Log(_fuelAmount * quantityMultiplier);
    }
}
