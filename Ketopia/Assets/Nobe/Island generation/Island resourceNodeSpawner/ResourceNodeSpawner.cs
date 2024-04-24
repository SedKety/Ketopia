using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNodeSpawner : MonoBehaviour
{
    public GameObject[] spawnableGameobjects;
    public Collider spawnCollider;
    public int spawnCount;
    public LayerMask spawnLayerMask;
    public int maxRetries = 10; // Maximum number of retries to avoid infinite loop

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (spawnableGameobjects != null && spawnCollider != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int retries = 0;
                Vector3 whereToSpawn;
                RaycastHit hit;
                do
                {
                    whereToSpawn = SpawnPosition(spawnCollider.bounds);
                    whereToSpawn.y += 20;
                    if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 20, spawnLayerMask))
                    {
                        GameObject spawnedObject = Instantiate(spawnableGameobjects[Random.Range(0, spawnableGameobjects.Length)], hit.point, Quaternion.identity);
                        spawnedObject.transform.parent = transform;
                        break;
                    }
                    retries++;
                } while (retries < maxRetries);

                if (retries >= maxRetries)
                {
                    break; 
                }
            }
        }
    }
    Vector3 SpawnPosition(Bounds bounds)
    {
        float randomX = Random.Range(bounds.center.x - bounds.extents.x, bounds.center.x + bounds.extents.x);
        float randomZ = Random.Range(bounds.center.z - bounds.extents.z, bounds.center.z + bounds.extents.z);

        return new Vector3(randomX, bounds.min.y, randomZ);
    }
}
