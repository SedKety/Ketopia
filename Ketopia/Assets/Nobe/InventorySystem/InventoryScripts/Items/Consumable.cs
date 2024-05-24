using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class Consumable : Item
{
    public float healthValue;
    public float foodValue;
    public override void OnItemUse()
    {
        PlayerStats.instance.food += foodValue;
        PlayerStats.instance.health += healthValue;
    }
}
