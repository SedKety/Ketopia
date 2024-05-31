using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour, IDamagable
{
    public float chestHp;
    public List<HeldItem> heldItems = new List<HeldItem>();
    public float disappearTimer;

    public void OnTriggerEnter(Collider other)
    {
        var physicalItemScript = other.GetComponent<PhysicalItemScript>();
        if (physicalItemScript != null)
        {
            var heldItem = new HeldItem
            {
                item = physicalItemScript.item.physicalItem,
                quantity = physicalItemScript.quantity
            };
            heldItems.Add(heldItem);
            Destroy(other.gameObject);
        }
    }

    public void IDamagable(float dmgDone, NodeType typeUsed, int toolStrength)
    {
        if (typeUsed != NodeType.everything)
        {
            chestHp -= dmgDone;
            if (chestHp <= 0)
            {
                DropItems();
                StartCoroutine(CollapseAndDie());
            }
        }
    }

    private void DropItems()
    {
        foreach (var heldItem in new List<HeldItem>(heldItems))
        {
            var itemToDrop = Instantiate(heldItem.item, transform.position, Quaternion.identity);
            var itemScript = itemToDrop.GetComponent<PhysicalItemScript>();
            if (itemScript != null)
            {
                itemScript.quantity = heldItem.quantity;
            }
        }
        heldItems.Clear();
    }

    public IEnumerator CollapseAndDie()
    {
        var collider = GetComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;

        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        yield return new WaitForSeconds(disappearTimer);
        Destroy(gameObject);
    }

    public struct HeldItem
    {
        public GameObject item;
        public int quantity;
    }
}
