using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : ResourceNode, IDamagable
{
    public List<GameObject> berries;
    public Item berry;

    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            berries.Add(transform.GetChild(i).gameObject);
        }
    }
    public override void IDamagable(float dmgDone, NodeType typeUsed, int toolStrength)
    {
        if (toolStrength >= nodeStrength)
        {
            if (typeUsed == nodeType || typeUsed == NodeType.everything)
            {
                nodeHp -= dmgDone;
                if (nodeHp <= 0)
                {
                    PlayerStats.instance.AddExp(expUponDestroy);
                    var currentItem = droppableItems[Random.Range(0, droppableItems.Length)];
                    var item = currentItem.item;
                    var spawnedObject = Instantiate(item, dropPoint.position, Quaternion.identity);
                    spawnedObject.GetComponent<PhysicalItemScript>().quantity = Random.Range(currentItem.minDrop, currentItem.maxDrop + 1);
                    for (int i = 0; i < berries.Count; i++)
                    {
                        berries[i].transform.parent = transform.parent;
                        var berryScript = berries[i].AddComponent<PhysicalItemScript>();
                        berries[i].AddComponent<Rigidbody>();
                        berryScript.quantity = 1;
                        berryScript.item = berry;
                    }
                    StartCoroutine(CollapseAndDie());
                }
            }
        }
    }
}
