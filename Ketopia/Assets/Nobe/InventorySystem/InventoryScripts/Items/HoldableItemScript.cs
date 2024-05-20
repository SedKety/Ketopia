using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Holdable")]
public class HoldableItemScript : Item
{
    public NodeType typeToHarvest;
    public int dmg;
    public int hitRange;
    public int toolStrength;
}
