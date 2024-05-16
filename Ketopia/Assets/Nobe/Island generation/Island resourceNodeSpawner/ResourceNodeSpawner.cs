using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNodeSpawner : MonoBehaviour
{
    public GameObject[] spawnableGameobjects;
    public Collider spawnCollider;
    public int spawnCount;
    public LayerMask spawnLayerMask;
    public int maxRetries; 

    void Start()
    {
        Invoke("SpawnObjects", 0.1f);
    }

    public virtual void SpawnObjects()
    {
        if (spawnableGameobjects != null && spawnCollider != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int retries = 0;
                Vector3 whereToSpawn;
                RaycastHit hit;
                while (retries < maxRetries)
                {
                    whereToSpawn = SpawnPosition(spawnCollider.bounds);
                    if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 50))
                    {
                        if(hit.collider.transform == transform.parent)
                        {
                            GameObject spawnedObject = Instantiate(spawnableGameobjects[Random.Range(0, spawnableGameobjects.Length)], hit.point, Quaternion.identity);
                            spawnedObject.transform.parent = transform;
                            break;
                        }
                    }
                    retries++;
                    if (retries >= maxRetries)
                    {
                        break;
                    }
                }
            }
        }
        if(transform.childCount <= 10) 
        {
            Destroy(transform.parent.gameObject);
        }
    }
    public virtual Vector3 SpawnPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
        float randomZ = Random.Range(bounds.center.z - bounds.extents.z, bounds.center.z + bounds.extents.z);

        return new Vector3(randomX, bounds.min.y, randomZ);
    }
}
