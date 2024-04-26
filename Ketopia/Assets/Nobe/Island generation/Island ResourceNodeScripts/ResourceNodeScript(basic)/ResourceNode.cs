using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NodeType
{
    stone,
    wood,
    plant,
}
public abstract class ResourceNode : MonoBehaviour, IDamagable
{
    public int nodeHp;
    public NodeType nodeType;
    public GameObject[] droppableItems;
    public abstract void IDamagable(int dmgDone, GameObject weaponUsed);
}
