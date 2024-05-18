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

    public virtual void OnItemUse(GameObject objectToInteract)
    {

    }
}
