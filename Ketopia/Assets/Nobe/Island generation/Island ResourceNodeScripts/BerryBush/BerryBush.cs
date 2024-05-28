using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryBush : ResourceNode, IDamagable
{
    public List<GameObject> berries;
    public Item berry;

    public override void Start()
    {
        base.Start();
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
                StartCoroutine(Shake());
                nodeHp -= dmgDone;
                if (nodeHp <= 0)
                {
                    StopCoroutine(Shake());
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
    private IEnumerator Shake()
    {
        float elapsed = 0.0f;
        float shakeyAmount = 0.01f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeyAmount, shakeyAmount) * shakeMagnitude;
            float offsetz = Random.Range(-shakeyAmount, shakeyAmount) * shakeMagnitude;

            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y, originalPosition.z + offsetz);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
