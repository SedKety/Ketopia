using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NodeType
{
    stone,
    wood,
    plant,
    everything,
}
public class ResourceNode : MonoBehaviour, IDamagable
{
    public float nodeHp;
    public NodeType nodeType;
    public int nodeStrength;
    public float expUponDestroy;
    public DroppableItems[] droppableItems;
    public Transform dropPoint;
    public int dissapearTimer;
    public virtual void IDamagable(float dmgDone, NodeType typeUsed, int toolStrength)
    {
        if (toolStrength >= nodeStrength)
        {
            if (typeUsed == nodeType || typeUsed == NodeType.everything)
            {
                nodeHp -= dmgDone;
                if (nodeHp <= 0)
                {
                    PlayerStats.instance.AddExp(expUponDestroy);
                    if(droppableItems != null)
                    {
                        var currentItem = droppableItems[Random.Range(0, droppableItems.Length)];
                        var item = currentItem.item;
                        var spawnedObject = Instantiate(item, dropPoint.position, Quaternion.identity);
                        spawnedObject.GetComponent<PhysicalItemScript>().quantity = Random.Range(currentItem.minDrop, currentItem.maxDrop + 1);
                        StartCoroutine(CollapseAndDie());
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public IEnumerator CollapseAndDie()
    {
        var collider = GetComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;
        gameObject.AddComponent<Rigidbody>();
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
