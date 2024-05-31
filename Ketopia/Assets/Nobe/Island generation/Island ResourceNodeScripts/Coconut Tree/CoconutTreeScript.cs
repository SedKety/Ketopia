using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutTreeScript : BerryBush
{
    public GameObject coconut;
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
                    DropCoconuts();
                    PlayerStats.instance.AddExp(expUponDestroy);
                    var currentItem = droppableItems[Random.Range(0, droppableItems.Length)];
                    var item = currentItem.item;
                    var spawnedObject = Instantiate(item, dropPoint.position, Quaternion.identity);
                    spawnedObject.GetComponent<PhysicalItemScript>().quantity = Random.Range(currentItem.minDrop, currentItem.maxDrop + 1);
                    StartCoroutine(CollapseAndDie());
                }
            }
        }
    }

    private void DropCoconuts()
    {
        for (int i = 0; i < berries.Count; i++)
        {
            print("gudshit");
            if (berries[i] != null)
            {
                print("gudshit2");
                var spawnedCoconut = Instantiate(coconut, berries[i].transform.position, Quaternion.identity);
                spawnedCoconut.GetComponent<PhysicalItemScript>().quantity = 1;
                Destroy(berries[i]);
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
