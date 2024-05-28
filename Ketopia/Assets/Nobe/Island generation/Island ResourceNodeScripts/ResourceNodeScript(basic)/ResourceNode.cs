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

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
    protected Vector3 originalPosition;

   public virtual void Start()
    {
        originalPosition = transform.localPosition;
    }
    public virtual void IDamagable(float dmgDone, NodeType typeUsed, int toolStrength)
    {
        if (toolStrength >= nodeStrength)
        {
            if (typeUsed == nodeType || typeUsed == NodeType.everything)
            {
                StartCoroutine(Shake());
                nodeHp -= dmgDone;
                if (nodeHp <= 0)
                {
                    PlayerStats.instance.AddExp(expUponDestroy);
                    if(droppableItems != null)
                    {
                        StopAllCoroutines();
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
