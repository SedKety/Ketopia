using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisIsland : ResourceNodeSpawner
{
    public GameObject coconutTree;
    public int coconutTreeCount;

    public Transform islandCenter;
    public override void SpawnObjects()
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
        SpawnTrees();
    }
    public void SpawnTrees()
    {
        if (coconutTree != null && spawnCollider != null)
        {
            for (int i = 0; i < coconutTreeCount; i++)
            {
                int retries = 0;
                Vector3 whereToSpawn;
                RaycastHit hit;
                while (retries < maxRetries)
                {
                    whereToSpawn = SpawnPosition(spawnCollider.bounds);
                    if (Physics.Raycast(whereToSpawn, Vector3.down, out hit, 50))
                    {
                        if (hit.collider.transform == transform.parent)
                        {
                            GameObject spawnedObject = Instantiate(coconutTree, hit.point, Quaternion.identity);
                            spawnedObject.transform.parent = transform;
                            spawnedObject.transform.LookAt(islandCenter);
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
            spawnCollider.enabled = false;
        }
    }

}
