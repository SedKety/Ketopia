using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class Consumable : Item
{
    public int healthValue;
    public int foodValue;
    public override void OnItemUse(GameObject objectToInteract)
    {
        PlayerStats.instance.food += foodValue;
        PlayerStats.instance.health += healthValue;
    }
}
