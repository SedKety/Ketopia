using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandCanyon : ResourceNodeSpawner
{
    public GameObject coconutTree;
    public int coconutTreeCount;

    public Transform islandCenter;
    public LayerMask layerCheck;
    public void Start()
    {
        SpawnObjects();
    }
    public override void SpawnObjects()
    {
        if (spawnableGameobjects != null && spawnCollider != null)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                int retries = 0;
                Vector3 whereToSpawn;
                while (retries < maxRetries)
                {
                    whereToSpawn = SpawnPosition(spawnCollider.bounds);
                    if (Physics.Raycast(whereToSpawn, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerCheck))
                    {
                        if (hit.collider.transform == transform.parent)
                        {
                            GameObject spawnedObject = Instantiate(spawnableGameobjects[Random.Range(0, spawnableGameobjects.Length)], hit.point, Quaternion.identity);
                            spawnedObject.transform.parent = transform;
                            spawnedObjects.Add(spawnedObject);
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
        if (transform.childCount <= 10)
        {
            Destroy(transform.parent.gameObject);
        }
    }
   
}
