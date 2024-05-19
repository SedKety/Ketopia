using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
public enum NodeType
{
    stone,
    wood,
    plant,
    everything,
}
public abstract class ResourceNode : MonoBehaviour, IDamagable
{
    public int nodeHp;
    public NodeType nodeType;
    public DroppableItems[] droppableItems;
    public Transform dropPoint;
    public virtual void IDamagable(int dmgDone, NodeType typeUsed)
    {
        if(typeUsed == nodeType || typeUsed == NodeType.everything)
        {
            nodeHp -= dmgDone;
            if (nodeHp <= 0)
            {
                var currentItem = droppableItems[Random.Range(0, droppableItems.Length)];
                var item = currentItem.item;
                var spawnedObject = Instantiate(item, dropPoint.position, Quaternion.identity);
                spawnedObject.GetComponent<PhysicalItemScript>().quantity = Random.Range(currentItem.minDrop, currentItem.maxDrop);
                Destroy(gameObject);
            }
        }
    }

    [System.Serializable]
    public struct DroppableItems
    {
        public GameObject item;
        public int maxDrop;
        public int minDrop;
    }
}
