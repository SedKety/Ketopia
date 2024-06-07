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
                UIScript.instance.popup.gameObject.SetActive(false);
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
                        if (berries[i].GetComponent<Rigidbody>() == null)
                        {
                            berries[i].AddComponent<Rigidbody>();
                        }
                        berryScript.quantity = 1;
                        berryScript.item = berry;
                    }
                    StartCoroutine(CollapseAndDie());
                }
            }
        }
        else
        {
            if (nodeType == NodeType.wood)
            {
                if (nodeStrength == 1)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Wooden axe to destroy this!";
                }
                if (nodeStrength == 2)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Stone axe to destroy this!";
                }
                if (nodeStrength == 3)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Iron axe to destroy this!";
                }
                if (nodeStrength == 4)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Golden axe to destroy this!";
                }
                if (nodeStrength == 5)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Magnetite axe to destroy this!";
                }
            }
            else if (nodeType == NodeType.stone)
            {
                if (nodeStrength == 1)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Wooden pickaxe to destroy this!";
                }
                if (nodeStrength == 2)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Stone pickaxe to destroy this!";
                }
                if (nodeStrength == 3)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Iron pickaxe to destroy this!";
                }
                if (nodeStrength == 4)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Golden pickaxe to destroy this!";
                }
                if (nodeStrength == 5)
                {
                    UIScript.instance.popup.gameObject.SetActive(true);
                    UIScript.instance.popup.displayText.text = "You need atleast an Magnetite pickaxe to destroy this!";
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
