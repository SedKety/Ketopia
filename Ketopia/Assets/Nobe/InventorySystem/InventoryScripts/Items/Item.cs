using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    resource,
    consumable,
    container,
    fuel,
}
public abstract class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public GameObject physicalItem;
    public Texture itemSprite;
    public float itemId;
    public int quantity;
    public int maxQuantity;
    public ItemType itemType;

    public virtual void OnItemUse(GameObject objectToInteract)
    {

    }
}
