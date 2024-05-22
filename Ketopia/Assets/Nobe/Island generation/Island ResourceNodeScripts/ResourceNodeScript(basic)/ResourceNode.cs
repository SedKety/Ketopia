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
    public int nodeStrength;
    public DroppableItems[] droppableItems;
    public Transform dropPoint;
    public int dissapearTimer;
    public virtual void IDamagable(int dmgDone, NodeType typeUsed, int toolStrength)
    {
        if(toolStrength >= nodeStrength)
        {
            if (typeUsed == nodeType || typeUsed == NodeType.everything)
            {
                nodeHp -= dmgDone;
                if (nodeHp <= 0)
                {
                    var currentItem = droppableItems[Random.Range(0, droppableItems.Length)];
                    var item = currentItem.item;
                    var spawnedObject = Instantiate(item, dropPoint.position, Quaternion.identity);
                    spawnedObject.GetComponent<PhysicalItemScript>().quantity = Random.Range(currentItem.minDrop, currentItem.maxDrop + 1);
                    StartCoroutine(CollapseAndDie());
                }
            }
        }
    }

    public IEnumerator CollapseAndDie()
    {
        var collider = GetComponent<MeshCollider>();
        collider.isTrigger = true;
        yield return new WaitForSeconds(dissapearTimer);
    }

    [System.Serializable]
    public struct DroppableItems
    {
        public GameObject item;
        public int maxDrop;
        public int minDrop;
    }
}
